using DataLayer;
using DataLayer.Models;
using DataLayer.Repository;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests;

[TestClass]
public class RepositoryTests
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly Repository<Account> _repository;

    public RepositoryTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDbForRepository"));

        _serviceProvider = services.BuildServiceProvider();

        _context = _serviceProvider.GetRequiredService<AppDbContext>();

        _repository = new(_context);
    }

    [TestMethod]
    public void Repository_Constructor_Exception()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Repository<Account>(null));
    }

    [TestMethod]
    public void Repository_GetAll_ListAccounts()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.AddRange(FillerBbData.Wallets);
        _context.SaveChanges();

         var accounts = _repository.GetAll( includeProperties: nameof(Account.Wallets), 
                                            orderBy: qa => qa.OrderBy(a => a.LastName))
                                   .ToList();

        var orderedAccounts = _repository.GetAll().
                                            OrderBy(a => a.LastName).
                                            ToList();
        
        Assert.IsNotNull(accounts);
        Assert.IsNotNull(accounts.FirstOrDefault().Wallets);

        for(var i = 0; i < accounts.Count; i++)
        {
            Assert.AreEqual(accounts[i].Id, orderedAccounts[i].Id);
        }

        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void Repository_Insert_Void()
    {
        var newAccount = new Account() { 
                                        LastName = "LastName",
                                        FirstName = "FirstName", 
                                        Email = "Email", 
                                        Password = "Password"
        };

        _repository.Insert(newAccount);
        _context.SaveChanges();

        var lastAccount = _repository.GetAll().LastOrDefault();

        Assert.IsTrue(newAccount.Equals(lastAccount));

        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void Repository_Update_Void() 
    {
        var account = new Account()
        {
            LastName = "LastName",
            FirstName = "FirstName",
            Email = "Email",
            Password = "Password"
        };

        _repository.Insert(account);
        _context.SaveChanges();

        account.FirstName = "New FirstName";
        _repository.Update(account);
        _context.SaveChanges();

        var modifitedAccount = _repository.GetAll().LastOrDefault();

        Assert.AreEqual(account, modifitedAccount);

        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void Repository_Delete_Void()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.SaveChanges();

        var removedAccount = FillerBbData.Accounts[3];

        _repository.Delete(removedAccount);
        _context.SaveChanges();

        var accounts = _repository.GetAll();

        Assert.IsFalse(accounts.Contains(removedAccount));

        _context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void Repository_GetById_Account()
    {
        _context.AddRange(FillerBbData.Accounts);
        _context.SaveChanges();

        var neededAccount = FillerBbData.Accounts[2];

        var foundAccount = _repository.GetById(neededAccount.Id);

        Assert.AreEqual(neededAccount, foundAccount);

        _context.Database.EnsureDeleted();
    }
}
