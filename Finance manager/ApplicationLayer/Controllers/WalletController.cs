using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Wallets;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class WalletController : EntityController
{
    private readonly IWalletService _service;

    public WalletController(ILogger<EntityController> logger, IMapper mapper, IWalletService service) : base(logger, mapper)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public List<WalletDTO> GetWalletsOfAccount(int accountId)
    {
        return _service.GetAllWalletsOfAccount(accountId)
                .Select(_mapper.Map<WalletDTO>)
                .ToList();
    }

    [HttpPost]
    public WalletDTO CreateWallet(WalletDTO wallet)
    {
        return _mapper.Map<WalletDTO>(
                _service.AddWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpPut]
    public WalletDTO UpdateWallet(WalletDTO wallet)
    {
        return _mapper.Map<WalletDTO>(
                _service.UpdateWallet(
                    _mapper.Map<WalletModel>(wallet)));
    }

    [HttpDelete]
    public void DeleteWallet(int id)
    {
        _service.DeleteWalletById(id);
    }

    [HttpGet("{id}")]
    public WalletDTO GetWalletById(int id)
    {
        return _mapper.Map<WalletDTO>(
                _service.FindWallet(id));
        ;
    }
}
