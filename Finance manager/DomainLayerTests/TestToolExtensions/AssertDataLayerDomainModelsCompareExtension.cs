using DomainLayer.Models;

namespace DomainLayerTests.TestHelpers;

public static class AssertDataLayerDomainModelsCompareExtension
{
    public static void Compare(this Assert assert, DataLayer.Models.Account dbAccount, AccountModel domainAccount)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbAccount));
        ArgumentNullException.ThrowIfNull(nameof(domainAccount));

        Assert.IsTrue(Compare(dbAccount, domainAccount));
    }

    public static void Compare(this Assert assert, DataLayer.Models.Wallet dbWallet, WalletModel domainWallet)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbWallet));
        ArgumentNullException.ThrowIfNull(nameof(domainWallet));

        Assert.IsTrue(Compare(dbWallet, domainWallet));
    }

    public static void Compare(this Assert assert, DataLayer.Models.FinanceOperationType dbFinanceOperationType, FinanceOperationTypeModel domainFinanceOperationType)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbFinanceOperationType));
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperationType));

        Assert.IsTrue(Compare(dbFinanceOperationType, domainFinanceOperationType));
    }

    public static void Compare(this Assert assert, DataLayer.Models.FinanceOperation dbFinanceOperation, FinanceOperationModel domainFinanceOperation)
    {
        ArgumentNullException.ThrowIfNull(nameof(dbFinanceOperation));
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperation));

        Assert.IsTrue(Compare(dbFinanceOperation, domainFinanceOperation));
    }

    private static bool Compare(DataLayer.Models.Account dbAccount, AccountModel domainAccount)
    {
        if ((dbAccount.Id == domainAccount.Id)
       && (dbAccount.LastName == domainAccount.LastName)
       && (dbAccount.FirstName == domainAccount.FirstName)
       && (dbAccount.Email == domainAccount.Email)
       && (dbAccount.Password == domainAccount.Password)
       && CompareAccountWallets(dbAccount, domainAccount))
            return true;

        return false;
    }

    private static bool Compare(DataLayer.Models.Wallet dbWallet, WalletModel domainWallet)
    {
        if ((dbWallet.Id == domainWallet.Id)
        && (dbWallet.Balance == domainWallet.Balance)
        && (dbWallet.AccountId == domainWallet.AccountId)
        && (dbWallet.Name == domainWallet.Name)
        && CompareWalletOperationTypes(dbWallet, domainWallet)
        && CompareFinanceWalletOperationTypes(dbWallet, domainWallet))
            return true;

        return false;
    }

    private static bool Compare(DataLayer.Models.FinanceOperationType dbFinanceOperationType, FinanceOperationTypeModel domainFinanceOperationType)
    {
        if ((dbFinanceOperationType.Id == domainFinanceOperationType.Id)
        && (dbFinanceOperationType.Name == domainFinanceOperationType.Name)
        && (dbFinanceOperationType.EntryType == domainFinanceOperationType.EntryType)
        && (dbFinanceOperationType.Description == domainFinanceOperationType.Description)
        && (dbFinanceOperationType.WalletId == domainFinanceOperationType.WalletId)
        && ((dbFinanceOperationType.Wallet != null && dbFinanceOperationType.Wallet.Name == domainFinanceOperationType.WalletName)
            || (dbFinanceOperationType.Wallet == null && domainFinanceOperationType.WalletName == string.Empty)))
            return true;

        return false;
    }

    private static bool Compare(DataLayer.Models.FinanceOperation dbFinanceOperation, FinanceOperationModel domainFinanceOperation)
    {
        if ((dbFinanceOperation.Id == domainFinanceOperation.Id)
        && (dbFinanceOperation.TypeId == domainFinanceOperation.Type.Id)
        && (dbFinanceOperation.Date == domainFinanceOperation.Date)
        && (dbFinanceOperation.Amount == domainFinanceOperation.Amount)
        && ((dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Income && domainFinanceOperation.GetType() == typeof(IncomeModel))
            || (dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Exponse && domainFinanceOperation.GetType() == typeof(ExpenseModel))))
            return true;

        return false;
    }

    private static bool Compare(DataLayer.Models.FinanceOperation dbFinanceOperation, IncomeModel income)
    {
        return Compare(dbFinanceOperation, (FinanceOperationModel)income) && dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Income;
    }

    private static bool Compare(DataLayer.Models.FinanceOperation dbFinanceOperation, ExpenseModel Expense)
    {
        return Compare(dbFinanceOperation, (FinanceOperationModel)Expense) && dbFinanceOperation.Type.EntryType == DataLayer.Models.EntryType.Exponse;
    }

    private static bool CompareAccountWallets(DataLayer.Models.Account dbAccount, AccountModel domainAccount)
    {
        if (dbAccount.Wallets.Count != domainAccount.Wallets.Count)
            return false;

        for (int i = 0; i < domainAccount.Wallets.Count; i++)
        {
            if (!Compare(dbAccount.Wallets[i], domainAccount.Wallets[i]))
                return false;
        }

        return true;
    }

    private static bool CompareWalletOperationTypes(DataLayer.Models.Wallet dbWallet, WalletModel domainWallet)
    {
        if (dbWallet.FinanceOperationTypes.Count != domainWallet.FinanceOperationTypes.Count)
            return false;

        for (int i = 0; i < domainWallet.FinanceOperationTypes.Count; i++)
        {
            if (!Compare(dbWallet.FinanceOperationTypes[i], domainWallet.FinanceOperationTypes[i]))
                return false;
        }

        return true;
    }

    private static bool CompareFinanceWalletOperationTypes(DataLayer.Models.Wallet dbWallet, WalletModel domainWallet)
    {
        if (dbWallet.GetFinanceOperations().Count != (domainWallet.Incomes.Count + domainWallet.Expenses.Count))
            return false;

        List<DataLayer.Models.FinanceOperation> dbFinanceOperations = dbWallet
                .GetFinanceOperations()
                .OrderBy(fo => fo.Type.EntryType)
                .ToList();

        for (int i = 0; i < domainWallet.Incomes.Count; i++)
        {
            if (!Compare(dbFinanceOperations[i], domainWallet.Incomes[i]))
                return false;
        }

        for (int i = domainWallet.Incomes.Count, j = 0; i < dbFinanceOperations.Count; i++, j++)
        {
            if (!Compare(dbFinanceOperations[i], domainWallet.Expenses[j]))
                return false;
        }

        return true;
    }
}
