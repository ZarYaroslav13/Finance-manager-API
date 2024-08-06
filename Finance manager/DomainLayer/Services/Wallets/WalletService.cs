﻿using AutoMapper;
using DataLayer.Models;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

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
        var result = _mapper.Map<WalletModel>(
                        _repository.Insert(
                                _mapper.Map<Wallet>(wallet)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public WalletModel Update(Wallet updatedWallet)
    {
        var result = _mapper.Map<WalletModel>(
                        _repository.Update(
                                _mapper.Map<Wallet>(updatedWallet)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public WalletModel Find(int id)
    {
        return _mapper
            .Map<WalletModel>(
                _repository
                    .GetById(id));
    }
}
