using DomainLayer.Models.Base;
using DomainLayerTests.Data.Models.Base;

namespace DomainLayerTests.Models.Base;

[TestClass]
public class HumanModelTests
{
    [TestMethod]
    [DynamicData(nameof(HumanModelTestsDataProvider.EqualsSameValuesReturnsTrueTestData), typeof(HumanModelTestsDataProvider))]
    public void Equals_SameValues_ReturnsTrue(HumanModel human1, HumanModel human2)
    {
        Assert.AreEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumanModelTestsDataProvider.EqualsDifferentValuesReturnsFalseTestData), typeof(HumanModelTestsDataProvider))]
    public void Equals_DifferentValues_ReturnsFalse(HumanModel human1, object human2)
    {
        Assert.AreNotEqual(human1, human2);
    }

    [TestMethod]
    [DynamicData(nameof(HumanModelTestsDataProvider.EqualsSameValuesReturnsTrueTestData), typeof(HumanModelTestsDataProvider))]
    public void GetHashCode_SameValues_ReturnsSameHashCode(HumanModel human1, HumanModel human2)
    {
        var hash1 = human1.GetHashCode();
        var hash2 = human2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
