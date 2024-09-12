using ApplicationLayer.Models.Base;
using ApplicationLayerTests.Data.Models;

namespace ApplicationLayerTests.Models.Base;

[TestClass]
public class HumanDTOTests
{
    [TestMethod]
    [DynamicData(nameof(HumantDTOTestDataProvider.MethodEqualsResultTrueData), typeof(HumantDTOTestDataProvider))]
    public void Equals_HumanDTOsAreEqual_True(HumanDTO human1, HumanDTO human2)
    {
        Assert.AreEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumantDTOTestDataProvider.MethodEqualsResultFalseData), typeof(HumantDTOTestDataProvider))]
    public void Equals_HumanDTOsAreNotEqual_False(HumanDTO human1, object human2)
    {
        Assert.AreNotEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumantDTOTestDataProvider.MethodEqualsResultTrueData), typeof(HumantDTOTestDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(HumanDTO human1, HumanDTO human2)
    {
        var hash1 = human1.GetHashCode();
        var hash2 = human2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
