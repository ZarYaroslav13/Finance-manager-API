using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Wallets;

public class WalletService : EntityService<WalletModel, Wallet>, IWalletService
{
    public WalletService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public List<WalletModel> GetAllWalletsOfAccount(int accountId)
    {
        return _repository
            .GetAll(filter: w => w.AccountId == accountId)
            .Select(_mapper.Map<WalletModel>)
            .ToList();
    }

    public WalletModel AddNewWallet(Wallet wallet)
    {
        return _mapper
            .Map<WalletModel>(
                _repository
                    .Insert(_mapper.Map<Wallets>(wallet)));
    }

    public WalletModel Update(Wallet updatedWallet)
    {
        return _mapper
            .Map<WalletModel>(
                _repository
                    .Update(updatedWallet));
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public WalletModel Find(int id)
    {
        return _mapper
            .Map<WalletModel>(
                _repository
                    .GetById(id));
    }
}
