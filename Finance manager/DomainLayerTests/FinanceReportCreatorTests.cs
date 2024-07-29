using AutoMapper;
using DataLayer;
using DataLayer.UnitOfWork;
using DomainLayer;
using DomainLayer.Infrastructure;
using DomainLayer.Models;
using DomainLayerTests.Data;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace DomainLayerTests
{
    [TestClass]
    public class FinanceReportCreatorTests
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        private FinanceReportCreator _creator;

        [TestInitialize]
        public void Setup()
        {
            _mapper = new MapperConfiguration(
                cfg =>
                    cfg.AddProfile<DomainDbMappingProfile>())
            .CreateMapper();

            var options = new DbContextOptionsBuilder<AppDbContext>();

            options.UseInMemoryDatabase("TestDbForServises");

            _context = new AppDbContext(options.Options);

            _unitOfWork = new UnitOfWork(_context);

            _creator = new(_unitOfWork, _mapper);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        [DynamicData(nameof(FinanceReportCreatorTestsDataProvider.ConstructorException), typeof(FinanceReportCreatorTestsDataProvider))]
        public void FinanceReportCreator_Constructor_Exception(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinanceReportCreator(unitOfWork, mapper));
        }

        [TestMethod]
        public void FinanceReportCreator_CreateFinanceReport_Exception()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _creator.CreateFinanceReport(null, new DateTime(), new DateTime()));
        }

        [TestMethod]
        public void FinanceReportCreator_CreateFinanceReport_ArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _creator.CreateFinanceReport(A.Dummy<WalletModel>(), DateTime.MaxValue, DateTime.MinValue));
        }

        [TestMethod]
        public void FinanceReportCreator_CreateFinanceReport_FinanceReport()
        {
            _context.AddRange(FillerBbData.FinanceOperationTypes);
            _context.AddRange(FillerBbData.FinanceOperations);
            _context.AddRange(FillerBbData.Wallets);
            _context.SaveChanges();

            var wallet = _mapper.Map<WalletModel>(_unitOfWork.GetRepository<DataLayer.Models.Wallet>().GetById(1));

            var period = new Period() { StartDate = new DateTime(2024, 1, 7), EndDate = new DateTime(2024, 3, 28) };

            var financeOperations = new List<FinanceOperationModel>();
            financeOperations.AddRange(wallet.Incomes.Where(fo => period.StartDate.Date <= fo.Date && fo.Date.Date <= period.EndDate));
            financeOperations.AddRange(wallet.Expenses.Where(fo => period.StartDate <= fo.Date.Date && fo.Date.Date <= period.EndDate));

            var expected = new FinanceReportModel(wallet.Id, wallet.Name, period) { Operations = financeOperations.OrderBy(fo => fo.Id).ToList() };

            var result = _creator.CreateFinanceReport(wallet, period.StartDate, period.EndDate);

            result.Operations = result.Operations.OrderBy(fo => fo.Id).ToList();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FinanceReportCreator_CreateDailyFinanceReport_FinanceReport()
        {
            _context.AddRange(FillerBbData.FinanceOperationTypes);
            _context.AddRange(FillerBbData.FinanceOperations);
            _context.AddRange(FillerBbData.Wallets);
            _context.SaveChanges();

            var wallet = _mapper.Map<WalletModel>(_unitOfWork.GetRepository<DataLayer.Models.Wallet>().GetById(1));

            var day = new DateTime(2024, 4, 11);

            var financeOperations = new List<FinanceOperationModel>();
            financeOperations.AddRange(wallet.Incomes.Where(fo => day.Date == fo.Date.Date));
            financeOperations.AddRange(wallet.Expenses.Where(fo => day.Date == fo.Date.Date));

            var expected = new FinanceReportModel(wallet.Id, wallet.Name, new Period() { StartDate = day, EndDate = day })
            {
                Operations = financeOperations
                .OrderBy(fo => fo.Id)
                .ToList()
            };

            var result = _creator.CreateFinanceReport(wallet, day);

            Assert.AreEqual(expected, result);
        }
    }
}