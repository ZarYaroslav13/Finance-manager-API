using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IMapper _mapper;

    public BaseController(IMapper mapper, ILogger<BaseController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}
