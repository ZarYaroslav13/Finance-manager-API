using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : EntityController<AccountDTO>
    {
        IAccountService _accountService;

        public AccountController(ILogger<EntityController<AccountDTO>> logger, IMapper mapper, IAccountService service) : base(logger, mapper)
        {
            _accountService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public AccountDTO LogIn(string email, string password)
        {
            return _mapper.Map<AccountDTO>(_accountService.TryLogIn(email, password));
        }
    }
}
