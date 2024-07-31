using DomainLayer.Models;
using DomainLayerTests.Data.Models;

namespace DomainLayerTests.Models;

[TestClass]
public class PeriodTests
{
    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultTrueData), typeof(PeriodDataProvider))]
    public void Equals_PeriodModelsAreEqual_True(Period p1, object p2)
    {
        Assert.AreEqual(p1, p2);
    }

    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.MethodEqualsResultFalseData), typeof(PeriodDataProvider))]
    public void Equals_PeriodModelsAreEqual_False(Period p1, object p2)
    {
        Assert.AreNotEqual(p1, p2);
    }
}
