using DataLayer.Models.Base;
using DataLayerTests.Data.Models.Base;

namespace DataLayerTests.Models.Base;

[TestClass]
public class HumanTests
{
    [TestMethod]
    [DynamicData(nameof(HumanTestDataProvider.EqualsSamePropertiesReturnsTrueTestData), typeof(HumanTestDataProvider))]
    public void Equals_SameProperties_ReturnsTrue(Human human1, Human human2)
    {
        Assert.AreEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumanTestDataProvider.EqualsDifferentPropertiesReturnsFalseTestData), typeof(HumanTestDataProvider))]
    public void Equals_DifferentProperities_ReturnsFalse(Human human1, Human human2)
    {
        Assert.AreNotEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumanTestDataProvider.EqualsSamePropertiesReturnsTrueTestData), typeof(HumanTestDataProvider))]
    public void GetHashCode_SameProperties_ReturnsSameHashCode(Human human1, Human human2)
    {
        var hashCode1 = human1.GetHashCode();
        var hashCode2 = human2.GetHashCode();

        Assert.AreEqual(hashCode1, hashCode2);
    }
}

