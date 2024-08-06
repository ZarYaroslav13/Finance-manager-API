using AutoMapper;
using DataLayer.Models;
using DataLayer.Security;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using System.Net.Mail;

namespace DomainLayer.Services.Accounts;

public class AccountService : EntityService<AccountModel, Account>, IAccountService
{
    public AccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public AccountModel AddNewAccount(AccountModel account)
    {
        CanTakeThisEmail(account.Email);

        var result = _mapper
            .Map<AccountModel>(
                _repository
                    .Insert(_mapper.Map<Account>(account)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public AccountModel UpdateAccount(AccountModel updatedAccount)
    {
        CanTakeThisEmail(updatedAccount.Email);

        var result = _mapper
            .Map<AccountModel>(
                _repository
                    .Update(_mapper.Map<Account>(updatedAccount)));
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

        string encodedPassword = new PasswordCoder().ComputeSHA256Hash(password);

        return _mapper.Map<AccountModel>(
            _repository
                .GetAll()
                .SingleOrDefault(a =>
                        a.Email == email
                        && a.Password == encodedPassword));
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
