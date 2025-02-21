using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.Security;
using Infrastructure.UnitOfWork;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services.Admins;
using System.Net.Mail;

namespace FinanceManager.Domain.Services.Accounts;

public class AccountService : BaseService, IAccountService
{
    private readonly IPasswordCoder _passwordCoder;
    private readonly IRepository<Account> _repository;
    private readonly IAdminService _adminService;

    public AccountService(IAdminService adminService, IPasswordCoder passwordCoder, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));

        _passwordCoder = passwordCoder ?? throw new ArgumentNullException(nameof(passwordCoder));

        _repository = _unitOfWork.GetRepository<Account>();
    }

    public const string NameAccountRole = "User";

    public string GetNameAccountRole() => NameAccountRole;

    public async Task<List<AccountModel>> GetAccountsAsync(string adminEmail, int skip = 0, int take = 0)
    {
        if (!_adminService.IsItAdmin(adminEmail))
            throw new UnauthorizedAccessException();

        if (skip < 0 || take < 0)
            throw new ArgumentException("skip and take arguments cannot be less 0");

        var accounts = await _repository.GetAllAsync(skip: skip, take: take);

        return accounts.Select(_mapper.Map<AccountModel>)
                .ToList();
    }

    public async Task<AccountModel> AddAccountAsync(AccountModel account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (account.Id != 0)
            throw new ArgumentException(nameof(account));

        await CanTakeThisEmailAsync(account.Id, account.Email);

        account.Password = _passwordCoder.ComputeSHA256Hash(account.Password);

        var result = _repository.Insert(_mapper.Map<Account>(account));
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<AccountModel>(result);
    }

    public async Task<AccountModel> UpdateAccountAsync(AccountModel updatedAccount)
    {
        ArgumentNullException.ThrowIfNull(updatedAccount);

        if (updatedAccount.Id == 0)
            throw new ArgumentException(nameof(updatedAccount));

        await CanTakeThisEmailAsync(updatedAccount.Id, updatedAccount.Email);

        var repoResult = _repository.Update(
                _mapper.Map<Account>(updatedAccount));

        var result = _mapper.Map<AccountModel>(repoResult);
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public void DeleteAccountWithId(int id)
    {
        _repository.Delete(id);

        _unitOfWork.SaveChangesAsync();
    }

    public async Task<AccountModel> TrySignInAsync(string email, string password)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

        string encodedPassword = _passwordCoder.ComputeSHA256Hash(password);

        var account = (await _repository.GetAllAsync())
                .FirstOrDefault(a =>
                    a.Email == email
                    && a.Password == encodedPassword);

        var result = _mapper.Map<AccountModel>(account); ;

        return result;
    }

    public bool IsItEmail(string emailAddress)
    {
        try
        {
            MailAddress m = new(emailAddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public async Task<bool> CanTakeThisEmailAsync(int id, string emailAddress)
    {
        if (!IsItEmail(emailAddress))
        {
            throw new FormatException("Email format is incorrect!");
        }

        var async = await _repository.GetAllAsync();

        if (async.Any(a => a.Email == emailAddress && a.Id != id))
        {
            throw new ArgumentException("An account with this email already exist!");
        }

        return true;
    }
}
