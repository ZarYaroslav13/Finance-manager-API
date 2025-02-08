using FinanceManager.ApiService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceManager.ApiService.Controllers.Base;

[Authorize]
[Route("finance-manager/[controller]s")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IMapper _mapper;

    public BaseController(IMapper mapper, ILogger<BaseController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    protected string GetUserRole()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity == null)
            throw new InvalidOperationException(nameof(identity));

        return identity.FindFirst(identity.RoleClaimType).Value;
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
