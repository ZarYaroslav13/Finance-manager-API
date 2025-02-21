using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Security;
using Infrastructure.UnitOfWork;
using FinanceManager.Domain.Services.Accounts;
using FinanceManager.Domain.Services.Admins;
using FakeItEasy;

namespace FinanceManager.Domain.Tests.Services;

[TestClass]
public class BaseServiceTests
{
    [TestMethod]
    public void Constructor_ArgumentsAreNull_ThrowException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(A.Dummy<IAdminService>(), A.Dummy<IPasswordCoder>(), A.Dummy<IUnitOfWork>(), null));
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(A.Dummy<IAdminService>(), A.Dummy<IPasswordCoder>(), null, A.Dummy<IMapper>()));
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(A.Dummy<IAdminService>(), A.Dummy<IPasswordCoder>(), null, null));
    }

    [TestMethod]
    public void Constructor_CreatedNeededReposetory_Reposetory()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();

        var service = new AccountService(A.Dummy<IAdminService>(), A.Dummy<IPasswordCoder>(), unitOfWork, A.Dummy<IMapper>());

        A.CallTo(() => unitOfWork.GetRepository<Account>()).MustHaveHappenedOnceExactly();
    }
}
