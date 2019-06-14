using System;
using System.Collections.Generic;

namespace NVP.DTO
{
    public class NPVDTO
    {
        public Guid Id { get; set; }
        public decimal initialValue { get; set; }
        public decimal lowerBoundDiscountRate { get; set; }
        public decimal upperBoundDiscountRate { get; set; }
        public decimal increment { get; set; }
        public List<decimal> cashFlows { get; set; }
        public DateTime createdDate { get; set; }
    }
}