using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WalletController : BaseController
{
    private readonly IWalletService _service;

    public WalletController(IWalletService service, IMapper mapper, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Policy = "OnlyForAdmins")]
    [HttpGet("GetWalletsOfAccount")]
    public List<WalletDTO> GetWalletsOfAccount(int accountId)
    {
        return _service.GetAllWalletsOfAccount(accountId)
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [HttpPost("CreateNewWallet")]
    public WalletDTO CreateWallet(WalletDTO wallet)
    {
        return _mapper.Map<WalletDTO>(
                _service.AddWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }


    [HttpPut("UpdateWallet")]
    public WalletDTO UpdateWallet(WalletDTO wallet)
    {
        return _mapper.Map<WalletDTO>(
                _service.UpdateWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }


    [HttpDelete("DeleteWallet")]
    public void DeleteWallet(int id)
    {
        _service.DeleteWalletById(id);
    }


    [HttpGet("GetWalletOfAccount/{id}")]
    public WalletDTO GetWalletById(int id)
    {
        return _mapper.Map<WalletDTO>(
                _service.FindWallet(id));
    }
}
