using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayerTests.TestHelpers;

public static class AssertDataLayerDomainModelsCompareExtension
{
    public static void AreEqual(this Assert assert, Account dbAccount, AccountModel domainAccount)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbAccount));
        ArgumentNullException.ThrowIfNull(nameof(domainAccount));

        Assert.IsTrue(AreEqual(dbAccount, domainAccount));
    }

    public static void AreEqual(this Assert assert, Wallet dbWallet, WalletModel domainWallet)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbWallet));
        ArgumentNullException.ThrowIfNull(nameof(domainWallet));

        Assert.IsTrue(AreEqual(dbWallet, domainWallet));
    }

    public static void AreEqual(this Assert assert, FinanceOperationType dbFinanceOperationType, FinanceOperationTypeModel domainFinanceOperationType)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbFinanceOperationType));
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperationType));

        Assert.IsTrue(AreEqual(dbFinanceOperationType, domainFinanceOperationType));
    }

    public static void AreEqual(this Assert assert, FinanceOperation dbFinanceOperation, FinanceOperationModel domainFinanceOperation)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbFinanceOperation));
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperation));

        Assert.IsTrue(AreEqual(dbFinanceOperation, domainFinanceOperation));
    }

    private static bool AreEqual(Account dbAccount, AccountModel domainAccount)
    {
        return (dbAccount.Id == domainAccount.Id)
       && (dbAccount.LastName == domainAccount.LastName)
       && (dbAccount.FirstName == domainAccount.FirstName)
       && (dbAccount.Email == domainAccount.Email)
       && (dbAccount.Password == domainAccount.Password)
       && AreEqualAccountWallets(dbAccount, domainAccount);
    }

    private static bool AreEqual(Wallet dbWallet, WalletModel domainWallet)
    {
        return (dbWallet.Id == domainWallet.Id)
        && (dbWallet.Balance == domainWallet.Balance)
        && (dbWallet.AccountId == domainWallet.AccountId)
        && (dbWallet.Name == domainWallet.Name)
        && AreEqualWalletOperationTypes(dbWallet, domainWallet)
        && AreEqualFinanceWalletOperationTypes(dbWallet, domainWallet);
    }

    private static bool AreEqual(FinanceOperationType dbFinanceOperationType, FinanceOperationTypeModel domainFinanceOperationType)
    {
        return (dbFinanceOperationType.Id == domainFinanceOperationType.Id)
        && (dbFinanceOperationType.Name == domainFinanceOperationType.Name)
        && (dbFinanceOperationType.EntryType == domainFinanceOperationType.EntryType)
        && (dbFinanceOperationType.Description == domainFinanceOperationType.Description)
        && (dbFinanceOperationType.WalletId == domainFinanceOperationType.WalletId)
        && ((dbFinanceOperationType.Wallet != null && dbFinanceOperationType.Wallet.Name == domainFinanceOperationType.WalletName)
            || (dbFinanceOperationType.Wallet == null && domainFinanceOperationType.WalletName == string.Empty));
    }

    private static bool AreEqual(FinanceOperation dbFinanceOperation, FinanceOperationModel domainFinanceOperation)
    {
        return (dbFinanceOperation.Id == domainFinanceOperation.Id)
        && (dbFinanceOperation.TypeId == domainFinanceOperation.Type.Id)
        && (dbFinanceOperation.Date == domainFinanceOperation.Date)
        && (dbFinanceOperation.Amount == domainFinanceOperation.Amount)
        && ((dbFinanceOperation.Type.EntryType == EntryType.Income && domainFinanceOperation.GetType() == typeof(IncomeModel))
            || (dbFinanceOperation.Type.EntryType == EntryType.Expense && domainFinanceOperation.GetType() == typeof(ExpenseModel)));
    }

    private static bool AreEqual(FinanceOperation dbFinanceOperation, IncomeModel income)
    {
        return AreEqual(dbFinanceOperation, (FinanceOperationModel)income) && dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Income;
    }

    private static bool AreEqual(FinanceOperation dbFinanceOperation, ExpenseModel Expense)
    {
        return AreEqual(dbFinanceOperation, (FinanceOperationModel)Expense) && dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Expense;
    }

    private static bool AreEqualAccountWallets(Account dbAccount, AccountModel domainAccount)
    {
        if (dbAccount.Wallets.Count != domainAccount.Wallets.Count)
            return false;

        for (int i = 0; i < domainAccount.Wallets.Count; i++)
        {
            if (!AreEqual(dbAccount.Wallets[i], domainAccount.Wallets[i]))
                return false;
        }

        return true;
    }

    private static bool AreEqualWalletOperationTypes(Wallet dbWallet, WalletModel domainWallet)
    {
        if (dbWallet.FinanceOperationTypes.Count != domainWallet.FinanceOperationTypes.Count)
            return false;

        for (int i = 0; i < domainWallet.FinanceOperationTypes.Count; i++)
        {
            if (!AreEqual(dbWallet.FinanceOperationTypes[i], domainWallet.FinanceOperationTypes[i]))
                return false;
        }

        return true;
    }

    private static bool AreEqualFinanceWalletOperationTypes(Wallet dbWallet, WalletModel domainWallet)
    {
        if (dbWallet.GetFinanceOperations().Count != (domainWallet.Incomes.Count + domainWallet.Expenses.Count))
            return false;

        List<FinanceOperation> dbFinanceOperations = dbWallet
                .GetFinanceOperations()
                .OrderBy(fo => fo.Type.EntryType)
                .ToList();

        for (int i = 0; i < domainWallet.Incomes.Count; i++)
        {
            if (!AreEqual(dbFinanceOperations[i], domainWallet.Incomes[i]))
                return false;
        }

        for (int i = domainWallet.Incomes.Count, j = 0; i < dbFinanceOperations.Count; i++, j++)
        {
            if (!AreEqual(dbFinanceOperations[i], domainWallet.Expenses[j]))
                return false;
        }

        return true;
    }
}
