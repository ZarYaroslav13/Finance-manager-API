using DomainLayer.Models;

namespace DomainLayerTests.Data;

public class PeriodDataProvider
{
    public static IEnumerable<object[]> EqualsData { get; } = new List<object[]>
    {
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            true
        },

        new object[]
        {
            new Period(){ StartDate = DateTime.MaxValue, EndDate = DateTime.MaxValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            false
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MinValue},
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            false
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MinValue},
            new Period(){ StartDate = DateTime.MaxValue, EndDate = DateTime.MaxValue},
            false
        },

        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            null,
            false
        },
        new object[]
        {
            new Period(){ StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue},
            new Wallet(),
            false
        }
    };
}
