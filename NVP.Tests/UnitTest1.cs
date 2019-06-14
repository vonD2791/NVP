using Microsoft.VisualStudio.TestTools.UnitTesting;
using NVP.Repository;
using NVP.Services;
using NVP.Services.Interface;
using System;
using System.Collections.Generic;

namespace NVP.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UnitTest()
        {
            INetPresentValueService netPresentValueService = new NetPresentValueService(new UnitOfWork());
            decimal npv = netPresentValueService.ComputeNetPresentValue(new List<decimal>()
            {
                100000, 150000, 200000, 250000, 300000
            },
            250000, 10);

            Assert.AreEqual(decimal.Round(npv, 2), new decimal(472168.75));
        }
    }
}