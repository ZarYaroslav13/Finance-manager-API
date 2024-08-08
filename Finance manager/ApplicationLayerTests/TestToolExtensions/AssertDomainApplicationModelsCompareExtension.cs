using DomainLayer.Models;
using ApplicationLayer.Models;

namespace ApplicationLayerTests.TestToolExtensions;

public static class AssertappApplicationModelsCompareExtension

{
    public static void AreEqual(this Assert assert, AccountModel domainAccount, AccountDTO appAccount)
    {
        ArgumentNullException.ThrowIfNull(nameof(appAccount));
        ArgumentNullException.ThrowIfNull(nameof(appAccount));

        Assert.IsTrue(AreEqual(domainAccount, appAccount));
    }

    public static void AreEqual(this Assert assert, WalletModel domainWallet, WalletDTO appWallet)
    {
        ArgumentNullException.ThrowIfNull(nameof(domainWallet));
        ArgumentNullException.ThrowIfNull(nameof(appWallet));

        Assert.IsTrue(AreEqual(domainWallet, appWallet));
    }

    public static void AreEqual(this Assert assert, FinanceOperationTypeModel domainFinanceOperationType, FinanceOperationTypeDTO appFinanceOperationType)
    {
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperationType));
        ArgumentNullException.ThrowIfNull(nameof(appFinanceOperationType));

        Assert.IsTrue(AreEqual(domainFinanceOperationType, appFinanceOperationType));
    }

    public static void AreEqual(this Assert assert, FinanceOperationModel domainFinanceOperation, FinanceOperationDTO appFinanceOperation)
    {
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceOperation));
        ArgumentNullException.ThrowIfNull(nameof(appFinanceOperation));

        Assert.IsTrue(AreEqual(domainFinanceOperation, appFinanceOperation));
    }

    public static void AreEqual(this Assert assert, FinanceReportModel domainFinanceReport, FinanceReportDTO appFinanceReport)
    {
        ArgumentNullException.ThrowIfNull(nameof(domainFinanceReport));
        ArgumentNullException.ThrowIfNull(nameof(appFinanceReport));

        Assert.IsTrue(AreEqual(domainFinanceReport, appFinanceReport));
    }

    private static bool AreEqual(AccountModel domainAccount, AccountDTO appAccount)
    {
        return (domainAccount.Id == appAccount.Id)
       && (domainAccount.LastName == appAccount.LastName)
       && (domainAccount.FirstName == appAccount.FirstName)
       && (domainAccount.Email == appAccount.Email)
       && (domainAccount.Password == appAccount.Password);
    }

    private static bool AreEqual(WalletModel domainWallet, WalletDTO appWallet)
    {
        return (domainWallet.Id == appWallet.Id)
        && (domainWallet.Balance == appWallet.Balance)
        && (domainWallet.AccountId == appWallet.AccountId)
        && (domainWallet.Name == appWallet.Name)
        && AreEqualWalletOperationTypes(domainWallet, appWallet)
        && AreEqualWalletFinanceOperations(domainWallet, appWallet);
    }

    private static bool AreEqual(FinanceOperationTypeModel domainFinanceOperationType, FinanceOperationTypeDTO appFinanceOperationType)
    {
        return (domainFinanceOperationType.Id == appFinanceOperationType.Id)
        && (domainFinanceOperationType.Name == appFinanceOperationType.Name)
        && (domainFinanceOperationType.EntryType == appFinanceOperationType.EntryType)
        && (domainFinanceOperationType.Description == appFinanceOperationType.Description)
        && (domainFinanceOperationType.WalletId == appFinanceOperationType.WalletId)
        && (domainFinanceOperationType.WalletName == appFinanceOperationType.WalletName);
    }

    private static bool AreEqual(FinanceOperationModel domainFinanceOperation, FinanceOperationDTO appFinanceOperation)
    {
        return (domainFinanceOperation.Id == appFinanceOperation.Id)
        && (AreEqual(domainFinanceOperation.Type, appFinanceOperation.Type))
        && (domainFinanceOperation.Date == appFinanceOperation.Date)
        && (domainFinanceOperation.Amount == appFinanceOperation.Amount);
    }

    private static bool AreEqual(FinanceReportModel domainFinanceReport, FinanceReportDTO appFinanceReport)
    {
        return (domainFinanceReport.Id == appFinanceReport.Id)
            && (domainFinanceReport.WalletId == appFinanceReport.WalletId)
            && (domainFinanceReport.WalletName == appFinanceReport.WalletName)
            && (domainFinanceReport.Period == appFinanceReport.Period)
            && (domainFinanceReport.TotalIncome == appFinanceReport.TotalIncome)
            && (domainFinanceReport.TotalExpense == appFinanceReport.TotalExpense)
            && AreEqualFinanceOperations(domainFinanceReport.Operations, appFinanceReport.Operations);
    }

    private static bool AreEqualWalletOperationTypes(WalletModel domainWallet, WalletDTO appWallet)
    {
        if (domainWallet.FinanceOperationTypes.Count != appWallet.FinanceOperationTypes.Count)
            return false;

        for (int i = 0; i < appWallet.FinanceOperationTypes.Count; i++)
        {
            if (!AreEqual(domainWallet.FinanceOperationTypes[i], appWallet.FinanceOperationTypes[i]))
                return false;
        }

        return true;
    }

    private static bool AreEqualWalletFinanceOperations(WalletModel domainWallet, WalletDTO appWallet)
    {
        if (domainWallet.Incomes.Count != appWallet.Incomes.Count || domainWallet.Expenses.Count != appWallet.Expenses.Count)
            return false;

        for (int i = 0; i < appWallet.Incomes.Count; i++)
        {
            if (!AreEqual(domainWallet.Incomes[i], appWallet.Incomes[i]))
                return false;
        }

        for (int i = 0; i < appWallet.Expenses.Count; i++)
        {
            if (!AreEqual(domainWallet.Expenses[i], appWallet.Expenses[i]))
                return false;
        }

        return true;
    }

    private static bool AreEqualFinanceOperations(List<FinanceOperationModel> domainFinanceOperations, List<FinanceOperationDTO> appDinanceOperation)
    {
        if (domainFinanceOperations.Count != appDinanceOperation.Count)
            return false;

        for (int i = 0; i < appDinanceOperation.Count; i++)
        {
            if (!AreEqual(domainFinanceOperations[i], appDinanceOperation[i]))
                return false;
        }

        return true;
    }
}
