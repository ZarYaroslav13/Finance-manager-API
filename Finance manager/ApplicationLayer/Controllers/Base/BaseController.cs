using ApplicationLayer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApplicationLayer.Controllers.Base;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IMapper _mapper;

    protected const string _adminPolicy = "OnlyForAdmins";

    public BaseController(IMapper mapper, ILogger<BaseController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    protected int GetUserId()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            throw new InvalidOperationException(nameof(identity));

        string stringId = identity.FindFirst(nameof(AccountDTO.Id)).Value;

        int id = 0;

        if (!int.TryParse(stringId, out id))
            throw new InvalidOperationException(nameof(stringId));

        return id;
    }

    protected string GetUserEmail()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            throw new InvalidOperationException(nameof(identity));

        return identity.Name;
    }
}
