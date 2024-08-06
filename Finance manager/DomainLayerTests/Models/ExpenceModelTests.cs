using DataLayer.Models;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerTests.Models;

[TestClass]
public class ExpenceModelTests
{
    [TestMethod]
    public void ExpenseModel_Creation_Success()
    {
        var type = new FinanceOperationTypeModel { EntryType = EntryType.Expense };

        var expenseModel = new ExpenseModel(type);

        Assert.AreEqual(type, expenseModel.Type);
    }

    [TestMethod]
    public void ExpenseModel_Creation_WithNonExpenseType_ThrowsException()
    {
        var type = new FinanceOperationTypeModel { EntryType = EntryType.Income };

        Assert.ThrowsException<ArgumentException>(() => new ExpenseModel(type));
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithExpenseType_Success()
    {
        var initialType = new FinanceOperationTypeModel { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeModel { EntryType = EntryType.Expense, Description = "new Type" };
        var expenseModel = new ExpenseModel(initialType);

        expenseModel.ChangeFinanceOperationType(newType);

        Assert.AreEqual(newType, expenseModel.Type);
    }

    [TestMethod]
    public void ChangeFinanceOperationType_WithNonExpenseType_ThrowsException()
    {
        var initialType = new FinanceOperationTypeModel { EntryType = EntryType.Expense, Description = "old Type" };
        var newType = new FinanceOperationTypeModel { EntryType = EntryType.Income, Description = "new Type" };
        var expenseModel = new ExpenseModel(initialType);

        Assert.ThrowsException<ArgumentException>(() => expenseModel.ChangeFinanceOperationType(newType));
    }
}
