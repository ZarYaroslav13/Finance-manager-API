using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationLayer.Controllers;

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

    [HttpGet("Recognize-Email")]
    public bool RecognizeEmail(string email)
    {
        return _accountService.IsItEmail(email);
    }

    [HttpPost]
    public AccountDTO CreateAccount(AccountDTO account)
    {
        return _mapper.Map<AccountDTO>(
                _accountService.AddNewAccount(
                    _mapper.Map<AccountModel>(account)));
    }

    [HttpPut]
    public AccountDTO UpdateAccount(AccountDTO account)
    {
        return _mapper.Map<AccountDTO>(
                _accountService.UpdateAccount(
                    _mapper.Map<AccountModel>(account)));
    }

    [HttpDelete]
    public void DeleteAccountById(int id)
    {
        _accountService.DeleteAccountWithId(id);
    }
}
