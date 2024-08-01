using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Finance_manager_API.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class EntityController<T> : ControllerBase where T : Models.Base.ModelDTO
{
    protected readonly ILogger<EntityController<T>> _logger;
    protected readonly IMapper _mapper;

    public EntityController(ILogger<EntityController<T>> logger, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }
}
