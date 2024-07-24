using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models;

[TestClass]
public class PeriodTests
{
    [TestMethod]
    [DynamicData(nameof(PeriodDataProvider.EqualsData), typeof(PeriodDataProvider))]
    public void Period_Equals_Bool(Period ac1, object ac2, bool expected)
    {
        bool result = ac1.Equals(ac2);

        Assert.AreEqual(expected, result);

        if (ac2 != null && ac1.GetType() == ac2.GetType())
        {
            Assert.AreEqual(ac1 == (Period)ac2, result);

            Assert.AreEqual(ac1 != (Period)ac2, !result);

            Assert.AreEqual(ac1 == ac1, true);

            Assert.AreEqual(ac1 == null, false);
        }
    }
}
