using AutoMapper;
using DataLayer;
using DomainLayer.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.Data;

namespace DomainLayerTests.Models;

[TestClass]
public class FinanceReportTests
{
    IMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        _mapper = new MapperConfiguration(
               cfg =>
                   cfg.AddProfile<DomainDbMappingProfile>())
           .CreateMapper();
    }

    [TestMethod]
    public void FinanceReport_Constructor_Exception()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceReport(1, null, new Period()));
    }

    [TestMethod]
    public void FinanceReport_Constructor_FinanceReport()
    {
        const int walletId = 1;
        const string walletName = "Name";
        Period period = new Period() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };

        var result = new FinanceReport(walletId, walletName, period);

        Assert.IsNotNull(result);
        Assert.AreEqual(walletId, result.WalletId);
        Assert.AreEqual(walletName, result.WalletName);
        Assert.AreEqual(period, result.Period);
    }

    [TestMethod]
    public void FinanceReport_Operations_FinanceOperationsList()
    {
        const int walletId = 1;
        const string walletName = "Name";
        Period period = new Period() { StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue };

        var bdFinanceOperationType = FillerBbData
            .FinanceOperationTypes
            .FirstOrDefault();

        var bdFinanceOperations = FillerBbData
            .FinanceOperations
            .Where(fo => fo.TypeId == bdFinanceOperationType.Id)
            .ToList();

        bdFinanceOperations.ForEach(fo => fo.Type = bdFinanceOperationType);

        var financeOperations = bdFinanceOperations
            .Select(_mapper.Map<FinanceOperation>)
            .ToList();

        int totalIncome = financeOperations.OfType<Income>().Select(i => i.Amount).Sum();
        int totalExpense = financeOperations.OfType<Expense>().Select(i => i.Amount).Sum();

        var report = new FinanceReport(walletId, walletName, period);

        report.Operations = financeOperations;

        Assert.AreEqual(totalIncome, report.TotalIncome);
        Assert.AreEqual(totalExpense, report.TotalExpense);
        Assert.IsTrue(Enumerable.SequenceEqual(report.Operations, financeOperations));
    }

    [TestMethod]
    [DynamicData(nameof(FinanceReportTestsDataProvider.EqualsData), typeof(FinanceReportTestsDataProvider))]
    public void FinanceReport_AreEquals_Bool(FinanceReport fr1, object fr2, bool expected)
    {
        bool result = fr1.Equals(fr2);

        Assert.AreEqual(expected, result);
    }
}
