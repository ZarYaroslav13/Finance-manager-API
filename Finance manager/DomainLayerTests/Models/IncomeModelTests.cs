using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayerTests.Models;

[TestClass]
public class IncomeModelTests
{
    [TestMethod]
    public void IncomeModel_Creation_Success()
    {
        var type = new FinanceOperationTypeModel { EntryType = EntryType.Income };

        var incomeModel = new IncomeModel(type);

        Assert.AreEqual(type, incomeModel.Type);
    }

    [TestMethod]
    public void IncomeModel_Creation_WithNonIncomeType_ThrowsException()
    {
        var type = new FinanceOperationTypeModel { EntryType = EntryType.Expense };

        Assert.ThrowsException<ArgumentException>(() => new IncomeModel(type));
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithIncomeType_Success()
    {
        var initialType = new FinanceOperationTypeModel { EntryType = EntryType.Income, Description = "old Type" };
        var newType = new FinanceOperationTypeModel { EntryType = EntryType.Income, Description = "new Type" };
        var incomeModel = new IncomeModel(initialType);

        incomeModel.ChangeFinanceOperationType(newType);

        Assert.AreEqual(newType, incomeModel.Type);
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithNonIncomeType_ThrowsException()
    {
        var initialType = new FinanceOperationTypeModel { EntryType = EntryType.Income, Description = "old Type" };
        var newType = new FinanceOperationTypeModel { EntryType = EntryType.Expense, Description = "new Type" };
        var incomeModel = new IncomeModel(initialType);

        Assert.ThrowsException<ArgumentException>(() => incomeModel.ChangeFinanceOperationType(newType));
    }
}
