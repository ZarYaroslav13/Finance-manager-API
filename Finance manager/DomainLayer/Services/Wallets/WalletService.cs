using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services.Wallets;

public class WalletService : BaseService, IWalletService
{
    private readonly IRepository<Wallet> _repository;

    public WalletService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _repository = _unitOfWork.GetRepository<Wallet>();
    }

    public List<WalletModel> GetAllWalletsOfAccount(int accountId)
    {
        return _repository
            .GetAll(filter: w => w.AccountId == accountId)
            .Select(_mapper.Map<WalletModel>)
            .ToList();
    }

    public WalletModel AddWallet(WalletModel wallet)
    {
        ArgumentNullException.ThrowIfNull(wallet);

        if (wallet.Id != 0)
            throw new ArgumentException(nameof(wallet));

        var result = _mapper.Map<WalletModel>(
                        _repository.Insert(
                                _mapper.Map<Wallet>(wallet)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public WalletModel UpdateWallet(WalletModel updatedWallet)
    {
        ArgumentNullException.ThrowIfNull(updatedWallet);

        if (updatedWallet.Id == 0)
            throw new ArgumentException(nameof(updatedWallet));

        var walletDb = _mapper.Map<Wallet>(updatedWallet);
        if (_repository.GetAll(filter: w => w == walletDb).Any())
            throw new InvalidOperationException();

        var result = _mapper.Map<WalletModel>(
                        _repository.Update(walletDb));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void DeleteWalletById(int id)
    {
        _repository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public WalletModel FindWallet(int id)
    {
        return _mapper
            .Map<WalletModel>(
                _repository
                    .GetById(id));
    }
}
