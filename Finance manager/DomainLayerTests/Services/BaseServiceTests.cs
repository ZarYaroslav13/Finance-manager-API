using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using DomainLayer.Services.Accounts;
using FakeItEasy;

namespace DomainLayerTests.Services;

[TestClass]
public class BaseServiceTests
{
    [TestMethod]
    public void Constructor_ArgumentsAreNull_ThrowException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(A.Dummy<IUnitOfWork>(), null));
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(null, A.Dummy<IMapper>()));
        Assert.ThrowsException<ArgumentNullException>(() => new AccountService(null, null));
    }

    [TestMethod]
    public void Constructor_CreatedNeededReposetory_Reposetory()
    {
        var unitOfWork = A.Fake<IUnitOfWork>();

        var service = new AccountService(unitOfWork, A.Dummy<IMapper>());

        A.CallTo(() => unitOfWork.GetRepository<Account>()).MustHaveHappenedOnceExactly();
    }
}
