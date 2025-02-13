using FinanceManager.ApiService.Models;
using Infrastructure.Models;

namespace ApplicationLayerTests.Models;

[TestClass]
public class ExpenceDTOTests
{
    [TestMethod]
    public void ExpenseDTO_Creation_Success()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Expense };

        var expenseDTO = new ExpenseDTO() { Type = type };

        Assert.AreEqual(type, expenseDTO.Type);
    }

    [TestMethod]
    public void ExpenseDTO_Creation_WithNonExpenseType_ThrowsException()
    {
        var type = new FinanceOperationTypeDTO { EntryType = EntryType.Income };

        Assert.ThrowsException<ArgumentException>(() => new ExpenseDTO() { Type = type });
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithExpenseType_Success()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "new Type" };
        var expenseDTO = new ExpenseDTO() { Type = initialType };

        expenseDTO.ChangeFinanceOperationType(newType);

        Assert.AreEqual(newType, expenseDTO.Type);
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithNonExpenseType_ThrowsException()
    {
        var initialType = new FinanceOperationTypeDTO { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeDTO { EntryType = EntryType.Income, Description = "new Type" };
        var expenseDTO = new ExpenseDTO() { Type = initialType };

        Assert.ThrowsException<ArgumentException>(() => expenseDTO.ChangeFinanceOperationType(newType));
    }

    [TestMethod]
    public void GetHashCode_SameValues_ReturnsSameHashCode()
    {
        var expense1 = new ExpenseDTO() { Type = new() { EntryType = EntryType.Expense } };
        var expense2 = new ExpenseDTO() { Type = new() { EntryType = EntryType.Expense } };

        var hash1 = expense1.GetHashCode();
        var hash2 = expense2.GetHashCode();

        Assert.AreEqual(hash1, hash2);
    }
}
