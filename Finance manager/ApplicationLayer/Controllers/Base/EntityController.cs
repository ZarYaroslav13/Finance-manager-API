using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class EntityController : Controller
{
    protected readonly ILogger<EntityController> _logger;
    protected readonly IMapper _mapper;

    public EntityController(ILogger<EntityController> logger, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
}
