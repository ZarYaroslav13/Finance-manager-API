using ApplicationLayer.Models;
using DataLayer.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class IncomeDTOTests
{
    [TestMethod]
    public void IncomeDTO_Creation_Success()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Income };

        var incomeDTO = new IncomeDTO(type);

        Assert.AreEqual(type, incomeDTO.Type);
    }

    [TestMethod]
    public void IncomeDTO_Creation_WithNonIncomeType_ThrowsException()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Expense };

        Assert.ThrowsException<ArgumentException>(() => new IncomeDTO(type));
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithIncomeType_Success()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Income, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Income, Description = "new Type" };
        var incomeDTO = new IncomeDTO(initialType);

        incomeDTO.ChangeFinanceOperationType(newType);

        Assert.AreEqual(newType, incomeDTO.Type);
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithNonIncomeType_ThrowsException()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Income, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "new Type" };
        var incomeDTO = new IncomeDTO(initialType);

        Assert.ThrowsException<ArgumentException>(() => incomeDTO.ChangeFinanceOperationType(newType));
    }

    [TestMethod]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        var income1 = new IncomeDTO(new() { EntryType = EntryType.Income });
        var income2 = new IncomeDTO(new() { EntryType = EntryType.Income });

        var hash1 = income1.GetHashCode();
        var hash2 = income2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
