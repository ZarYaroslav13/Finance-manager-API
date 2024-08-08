using DataLayer.Models;
using ApplicationLayer.Models;

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
}
