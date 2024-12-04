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

    public async Task<List<WalletModel>> GetAllWalletsOfAccountAsync(int accountId)
    {
        return (await _repository
            .GetAllAsync(filter: w => w.AccountId == accountId))
            .Select(_mapper.Map<WalletModel>)
            .ToList();
    }

    public async Task<WalletModel> AddWalletAsync(WalletModel wallet)
    {
        ArgumentNullException.ThrowIfNull(wallet);

        if (wallet.Id != 0)
            throw new ArgumentException(nameof(wallet));

        if (wallet.AccountId <= 0)
            throw new ArgumentOutOfRangeException(nameof(wallet));

        var result = _repository.Insert(
                                _mapper.Map<Wallet>(wallet));
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<WalletModel>(result);
    }

    public async Task<WalletModel> UpdateWalletAsync(WalletModel updatedWallet)
    {
        ArgumentNullException.ThrowIfNull(updatedWallet);

        if (updatedWallet.Id == 0)
            throw new ArgumentException(nameof(updatedWallet));

        var result = _mapper.Map<WalletModel>(
                        _repository.Update(
                            _mapper.Map<Wallet>(updatedWallet)));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteWalletByIdAsync(int id)
    {
        _repository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<WalletModel> FindWalletAsync(int id)
    {
        return _mapper
            .Map<WalletModel>(
               await _repository.GetByIdAsync(id));
    }

    public async Task<bool> IsAccountOwnerWalletAsync(int acountId, int walletId)
    {
        if (acountId <= 0 || walletId <= 0)
            throw new ArgumentOutOfRangeException("account id and wallet id cannot be less or equal 0");

        var wallet = (await _repository.GetByIdAsync(walletId));

        return wallet.AccountId == acountId;
    }
}
