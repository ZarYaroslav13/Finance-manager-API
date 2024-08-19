using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Security;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using System.Net.Mail;

namespace DomainLayer.Services.Accounts;

public class AccountService : BaseService, IAccountService
{
    private readonly PasswordCoder _passwordCoder;
    private readonly IRepository<Account> _repository;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _passwordCoder = new PasswordCoder();

        _repository = _unitOfWork.GetRepository<Account>();
    }

    public AccountModel AddAccount(AccountModel account)
    {
        ArgumentNullException.ThrowIfNull(account);

        if (account.Id != 0)
            throw new ArgumentException(nameof(account));

        CanTakeThisEmail(account.Email);

        account.Password = _passwordCoder.ComputeSHA256Hash(account.Password);

        var result = _mapper
            .Map<AccountModel>(
                _repository
                    .Insert(_mapper.Map<Account>(account)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public AccountModel UpdateAccount(AccountModel updatedAccount)
    {
        ArgumentNullException.ThrowIfNull(updatedAccount);

        if (updatedAccount.Id == 0)
            throw new ArgumentException(nameof(updatedAccount));

        CanTakeThisEmail(updatedAccount.Email);

        var result = _mapper
            .Map<AccountModel>(
                _repository.Update(_mapper.Map<Account>(updatedAccount)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void DeleteAccountWithId(int id)
    {
        _repository.Delete(id);

        _unitOfWork.SaveChanges();
    }

    public AccountModel TryLogIn(string email, string password)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

        string encodedPassword = _passwordCoder.ComputeSHA256Hash(password);

        var result = _mapper.Map<AccountModel>(
           _repository
               .GetAll()
               .FirstOrDefault(a =>
                       a.Email == email
                       && a.Password == encodedPassword)); ;

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

    public bool CanTakeThisEmail(string emailAddress)
    {
        if (!IsItEmail(emailAddress))
        {
            throw new FormatException("Email format is incorrect!");
        }

        if (_repository.GetAll().Any(a => a.Email == emailAddress))
        {
            throw new ArgumentException("An account with this email already exist!");
        }

        return true;
    }
}
