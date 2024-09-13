using ApplicationLayer.Controllers;
using ApplicationLayerTests.Data.Controllers.Base;
using AutoMapper;
using DomainLayer.Services.Accounts;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace ApplicationLayerTests.Controllers.Base;

[TestClass]
public class BaseControllerTests
{
    private readonly IAccountService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountController> _logger;
    private readonly AccountController _controller;

    public BaseControllerTests()
    {
        _service = A.Fake<IAccountService>();
        _mapper = A.Fake<IMapper>();
        _logger = A.Fake<ILogger<AccountController>>();

        _controller = new(_service, _mapper, _logger);
    }

    [TestMethod]
    [DynamicData(nameof(BaseControllerTestDataProvider.ConstructorArgumentIsEqualNullArgumentNullExceptionTestData), typeof(BaseControllerTestDataProvider))]
    public void Constructor_ArgumentIsEqualNull_ArgumentNullException(IMapper mapper, ILogger<AccountController> logger)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new AccountController(_service, mapper, logger));
    }
}
