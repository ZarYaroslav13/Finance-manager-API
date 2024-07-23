using DomainLayer.Models;
using DomainLayerTests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerTests.Models;

[TestClass]
public class FinanceOperationTests
{
    [TestMethod]
    [DynamicData(nameof(FinanceOperationDataProvider.EqualsData), typeof(FinanceOperationDataProvider))]
    public void FinanceOperation_Equals_Bool(FinanceOperation fot1, object fot2, bool expected)
    {
        bool result = fot1.Equals(fot2);

        Assert.AreEqual(expected, result);
    }
}
