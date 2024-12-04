using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Security;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.Admins;
using DomainLayerTests.Data.Services;
using FakeItEasy;
using System.Linq.Expressions;

namespace DomainLayerTests.Services;

[TestClass]
public class AdminServiceTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Admin> _repository;
    private readonly IMapper _mapper;
    private readonly IAdminService _adminService;
    private readonly IPasswordCoder _passwordCoder;

    public AdminServiceTests()
    {
        _repository = A.Fake<IRepository<Admin>>();
        _unitOfWork = A.Fake<IUnitOfWork>();
        _mapper = A.Fake<IMapper>();
        _passwordCoder = A.Fake<IPasswordCoder>();

        A.CallTo(() => _unitOfWork.GetRepository<Admin>()).Returns(_repository);


        _adminService = new AdminService(_passwordCoder, _unitOfWork, _mapper);
    }

    [TestMethod]
    public void Constructor_ArgumentIsNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AdminService(null, _unitOfWork, _mapper));
    }

    [TestMethod]
    [DynamicData(nameof(AdminServiceTestsDataProvider.GetAdminsShouldReturnMappedAdminModelsTestData), typeof(AdminServiceTestsDataProvider))]
    public void GetAdmins_ShouldReturnMappedAdminModels(List<Admin> admins, List<AdminModel> adminModels)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Admin>, IOrderedQueryable<Admin>>>._,
            A<Expression<Func<Admin, bool>>>._,
            A<int>._, A<int>._,
            A<string[]>._))
            .Returns(admins);

        A.CallTo(() => _mapper.Map<AdminModel>(A<Admin>._))
            .ReturnsLazily((object a) => new AdminModel { Email = ((Admin)a).Email, Password = ((Admin)a).Password });

        var result = _adminService.GetAdmins();

        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(adminModels, result);
    }

    [TestMethod]
    [DynamicData(nameof(AdminServiceTestsDataProvider.TrySignInAsyncWithValidCredentialsShouldReturnAdminModelTestData), typeof(AdminServiceTestsDataProvider))]
    public async Task TrySignInAsync_WithValidCredentials_ShouldReturnAdminModel(Admin admin, AdminModel adminModel)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Admin>,
                 IOrderedQueryable<Admin>>>._,
                 A<Expression<Func<Admin, bool>>>._,
                 A<int>._, A<int>._,
                 A<string[]>._))
            .Returns(new List<Admin> { admin });
        A.CallTo(() => _passwordCoder.ComputeSHA256Hash(admin.Password)).Returns(admin.Password);
        A.CallTo(() => _mapper.Map<AdminModel>(admin)).Returns(adminModel);

        // Act
        var result = await _adminService.TrySignInAsync(admin.Email, admin.Password);

        Assert.IsNotNull(result);
        Assert.AreEqual(adminModel.Email, result.Email);
    }

    [TestMethod]
    [DynamicData(nameof(AdminServiceTestsDataProvider.TrySignInAsyncWithInvalidCredentialsShouldReturnNullTestData), typeof(AdminServiceTestsDataProvider))]
    public async Task TrySignInAsync_WithInvalidCredentials_ShouldReturnNull(List<Admin> admins, string email, string password)
    {
        A.CallTo(() => _repository.GetAllAsync(
                A<Func<IQueryable<Admin>,
                 IOrderedQueryable<Admin>>>._,
                 A<Expression<Func<Admin, bool>>>._,
                 A<int>._, A<int>._,
                 A<string[]>._))
            .Returns(admins);
        A.CallTo(() => _mapper.Map<AdminModel>(null)).Returns(null);

        var result = await _adminService.TrySignInAsync(email, password);

        Assert.IsNull(result);
    }

    [TestMethod]
    [DynamicData(nameof(AdminServiceTestsDataProvider.IsItAdminWithAdminEmailShouldReturnTrueTestData), typeof(AdminServiceTestsDataProvider))]
    public void IsItAdmin_WithCorrectAdminEmail_ShouldReturnTrue(List<Admin> admins, string email)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Admin>,
                 IOrderedQueryable<Admin>>>._,
                 A<Expression<Func<Admin, bool>>>._,
                 A<int>._, A<int>._,
                 A<string[]>._))
            .Returns(admins);

        A.CallTo(() => _mapper.Map<AdminModel>(A<Admin>._))
            .ReturnsLazily((object a) => new AdminModel { Email = ((Admin)a).Email, Password = ((Admin)a).Password });

        var result = _adminService.IsItAdmin(email);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DynamicData(nameof(AdminServiceTestsDataProvider.IsItAdminWithNonAdminEmailShouldReturnFalseTestData), typeof(AdminServiceTestsDataProvider))]
    public void IsItAdmin_WithNonAdminEmail_ShouldReturnFalse(List<Admin> admins, string email)
    {
        A.CallTo(() => _repository.GetAllAsync(
            A<Func<IQueryable<Admin>,
                 IOrderedQueryable<Admin>>>._,
                 A<Expression<Func<Admin, bool>>>._,
                 A<int>._, A<int>._,
                 A<string[]>._))
            .Returns(admins);

        A.CallTo(() => _mapper.Map<AdminModel>(A<Admin>._))
            .ReturnsLazily((object a) => new AdminModel { Email = ((Admin)a).Email, Password = ((Admin)a).Password });

        var result = _adminService.IsItAdmin(email);

        Assert.IsFalse(result);
    }
}

