using System.Collections.Generic;

namespace NVP.Requests
{
    public class NetPresentValueRequest
    {
        public decimal initialValue { get; set; }
        public decimal lowerBoundDiscountRate { get; set; }
        public decimal upperBoundDiscountRate { get; set; }
        public decimal increment { get; set; }
        public List<decimal> cashFlows { get; set; }
    }
}