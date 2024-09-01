using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class WalletController : BaseController
{
    private readonly IWalletService _service;

    public WalletController(IWalletService service, IMapper mapper, ILogger<WalletController> logger) : base(mapper, logger)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet("GetWalletsOfAccount")]
    public List<WalletDTO> GetWallets()
    {
        return _service.GetAllWalletsOfAccount(GetUserId())
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [HttpPost("CreateNewWallet")]
    public WalletDTO Create(WalletDTO wallet)
    {
        wallet.AccountId = GetUserId();

        return _mapper.Map<WalletDTO>(
                _service.AddWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpPut("UpdateWallet")]
    public WalletDTO Update(WalletDTO wallet)
    {
        if (wallet.AccountId != GetUserId())
            throw new UnauthorizedAccessException(nameof(wallet.AccountId));

        return _mapper.Map<WalletDTO>(
                _service.UpdateWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpDelete("DeleteWallet{id}")]
    public void Delete(int id)
    {
        if (!_service.IsAccountOwnerWallet(GetUserId(), id))
            throw new UnauthorizedAccessException(nameof(id));

        _service.DeleteWalletById(id);
    }

    [HttpGet("GetWalletOfAccount/{id}")]
    public WalletDTO GetById(int id)
    {
        return _mapper.Map<WalletDTO>(
                _service.FindWallet(id));
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpGet("Admin/GetWalletsOfAccount/{accountId}")]
    public List<WalletDTO> GetWalletsOfAccount(int accountId)
    {
        return _service.GetAllWalletsOfAccount(accountId)
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("Admin/DeleteWallet/{id}")]
    public void DeleteWalletOfAccount(int id)
    {
        _service.DeleteWalletById(id);
    }
}
