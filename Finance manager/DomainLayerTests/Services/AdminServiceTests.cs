using DataLayer;
using DataLayer.Models;
using DomainLayer.Services.Admins;
using DomainLayerTests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayerTests.Services;

[TestClass]
public class AdminServiceTests
{
    private readonly IAdminService _service;
    private readonly string _adminEmail = DbEntitiesTestDataProvider.Accounts[5].Email;
    private readonly string _nonAdminEmail = "nonadmin@example.com";

    public AdminServiceTests()
    {
        _service = new AdminService();
    }

    [TestMethod]
    public void IsItAdmin_EmailIsInAdminList_ReturnTrue()
    {
        var adminEmail = FillerBbData.Accounts[5].Email; // Using the email of the admin

        var result = _service.IsItAdmin(adminEmail);
        
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsItAdmin_EmailIsNotInAdminList_ReturnFalse()
    {
        var result = _service.IsItAdmin(_nonAdminEmail);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsItAdmin_EmailIsNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _service.IsItAdmin(null));
    }

    [TestMethod]
    public void IsItAdmin_EmailIsEmpty_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => _service.IsItAdmin(string.Empty));
    }

    [TestMethod]
    public void IsItAdmin_EmailIsWhitespace_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => _service.IsItAdmin("   "));
    }
}
