using NVP.DTO;
using NVP.Models;
using NVP.Repository.Interface;
using NVP.Requests;
using NVP.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NVP.Services
{
    public class NetPresentValueService : INetPresentValueService
    {
        private readonly IUnitOfWork unitOfWork;

        public NetPresentValueService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool AddToHistory(decimal initialValue, decimal lowerBoundDiscountRate, decimal upperBoundDiscountRate, decimal increment, List<decimal> cashFlows)
        {
            List<NPV_HISTORY_CASHFLOWS> lstCashFlows = new List<NPV_HISTORY_CASHFLOWS>();
            int counter = 1;
            foreach (decimal cashFlow in cashFlows)
            {
                lstCashFlows.Add(new NPV_HISTORY_CASHFLOWS()
                {
                    ID = Guid.NewGuid(),
                    CASH_FLOW = cashFlow,
                    ORDER = counter
                });
                counter++;
            }

            NPV_HISTORY npv_history = new NPV_HISTORY()
            {
                ID = Guid.NewGuid(),
                INITIAL_VALUE = initialValue,
                LOWER_BOUND_DISCOUNT_RATE = lowerBoundDiscountRate,
                UPPER_BOUND_DISCOUNT_RATE = upperBoundDiscountRate,
                INCREMENT = increment,
                CREATED_DATE = DateTime.Now,
                NPV_HISTORY_CASHFLOWS = lstCashFlows
            };

            unitOfWork.NPVHistory.Add(npv_history);
            return unitOfWork.SaveChanges() > 0;
        }

        public decimal ComputeNetPresentValue(List<decimal> cashFlows, decimal initialValue, decimal discountRate)
        {
            decimal newPresentValue = 0;
            discountRate = 1 + discountRate;
            int timePeriod = 0;
            foreach (decimal cashFlow in cashFlows)
            {
                int counter = 0;
                decimal runningDiscountRate = discountRate;
                while (counter < timePeriod)
                {
                    runningDiscountRate *= discountRate;
                    counter++;
                }
                newPresentValue +=  (cashFlow / runningDiscountRate);
                timePeriod++;
            }
            newPresentValue -= initialValue;
            return newPresentValue;
        }

        public IEnumerable<NPVDTO> GetNPVDTOHistory()
        {
            IEnumerable<NPVDTO> result = unitOfWork.NPVHistory.GetAll().Select(x => new NPVDTO
            {
                Id = x.ID,
                initialValue = x.INITIAL_VALUE,
                lowerBoundDiscountRate = x.LOWER_BOUND_DISCOUNT_RATE,
                upperBoundDiscountRate = x.UPPER_BOUND_DISCOUNT_RATE,
                increment = x.INCREMENT,
                createdDate = x.CREATED_DATE,
                cashFlows = x.NPV_HISTORY_CASHFLOWS.OrderBy(o => o.ORDER).Select(y => y.CASH_FLOW).ToList()
            });

            foreach (NPVDTO nPVDTO in result)
            {
                nPVDTO.nPVPerDiscountRateDTOs = ComputeNetPresentValues(new NetPresentValueRequest()
                {
                    initialValue = nPVDTO.initialValue,
                    lowerBoundDiscountRate = nPVDTO.lowerBoundDiscountRate,
                    upperBoundDiscountRate =nPVDTO.upperBoundDiscountRate,
                    increment = nPVDTO.increment,
                    cashFlows = nPVDTO.cashFlows
                }).ToList();

                yield return nPVDTO;
            }
        }
        public IEnumerable<NPVPerDiscountRateDTO> ComputeNetPresentValues(NetPresentValueRequest netPresentValueRequest)
        {
            while (netPresentValueRequest.lowerBoundDiscountRate <= netPresentValueRequest.upperBoundDiscountRate)
            {
                decimal npv = ComputeNetPresentValue(netPresentValueRequest.cashFlows, netPresentValueRequest.initialValue, netPresentValueRequest.lowerBoundDiscountRate);
                yield return new NPVPerDiscountRateDTO()
                {
                    DiscountRate = netPresentValueRequest.lowerBoundDiscountRate,
                    NPV = npv
                };
                netPresentValueRequest.lowerBoundDiscountRate += netPresentValueRequest.increment;
                if (netPresentValueRequest.increment == 0) break;
            }
        }
    }
}