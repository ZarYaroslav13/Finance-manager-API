using DataLayer.Models;
using ApplicationLayer.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class ExpenceDTOTests
{
    [TestMethod]
    public void ExpenseDTO_Creation_Success()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Expense };

        var expenseDTO = new ExpenseDTO(type);

        Assert.AreEqual(type, expenseDTO.Type);
    }

    [TestMethod]
    public void ExpenseDTO_Creation_WithNonExpenseType_ThrowsException()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Income };

        Assert.ThrowsException<ArgumentException>(() => new ExpenseDTO(type));
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithExpenseType_Success()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "new Type" };
        var expenseDTO = new ExpenseDTO(initialType);

        expenseDTO.ChangeFinanceOperationType(newType);

        Assert.AreEqual(newType, expenseDTO.Type);
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithNonExpenseType_ThrowsException()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Income, Description = "new Type" };
        var expenseDTO = new ExpenseDTO(initialType);

        Assert.ThrowsException<ArgumentException>(() => expenseDTO.ChangeFinanceOperationType(newType));
    }
}
