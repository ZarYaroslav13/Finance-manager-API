using DomainLayer.Models;

namespace DomainLayerTests.Data.Models;

public class PeriodDataProvider
{
    public static IEnumerable<object[]> MethodEqualsResultTrueData { get; } = new List<object[]>
    {
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue}
        }
    };

    public static IEnumerable<object[]> MethodEqualsResultFalseData { get; } = new List<object[]>
    {
        new object[]
        {
            new Period(){ StartDate = DateTime.MaxValue, EndDate = DateTime.MaxValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue}
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MinValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue}
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MinValue},
            new Period(){ StartDate = DateTime.MaxValue, EndDate = DateTime.MaxValue}
        },

        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            null
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            new WalletModel()
        }
    };
}
