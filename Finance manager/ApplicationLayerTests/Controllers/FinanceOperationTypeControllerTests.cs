using ApplicationLayer.Controllers;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Services.Finances;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ApplicationLayerTests.Controllers;

[TestClass]
public class FinanceOperationTypeControllerTests
{
    private readonly FinanceOperationTypeController _controller;
    private readonly IFinanceService _financeService;
    private readonly IMapper _mapper;
    private readonly ILogger<FinanceOperationTypeController> _logger;
    private ClaimsPrincipal _user;
    private readonly int _userId = 1;
    private readonly string _email = "user@example.com";

    public FinanceOperationTypeControllerTests()
    {
        _financeService = A.Fake<IFinanceService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<FinanceOperationTypeController>>();

        _controller = new(_financeService, _mapper, _logger);

        _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(nameof(AccountDTO.Id), _userId.ToString()),
            new Claim(ClaimTypes.Name, _email)
        }, "mock"));

        var httpContext = A.Fake<HttpContext>();
        A.CallTo(() => httpContext.User).Returns(_user);
        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = httpContext
        };
    }

    [TestMethod]
    public void Constructor_ArgumentsAreNull_ThrowsArgumentNullException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new FinanceOperationTypeController(null, _mapper, _logger));
    }


}
