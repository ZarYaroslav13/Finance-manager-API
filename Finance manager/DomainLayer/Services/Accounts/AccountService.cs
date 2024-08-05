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

    public static bool IsItEmail(string emailAddress)
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

    public AccountModel AddNewAccount(Account account)
    {
        if (IsItEmail(account.Email))
        {
            throw new FormatException("Email format is incorrect!");
        }

        if (_repository.GetAll().Any(a => a.Email == account.Email))
        {
            throw new ArgumentException("An account with this email already exist!");
        }

        var result = _mapper
            .Map<AccountModel>(
                _repository
                    .Insert(_mapper.Map<Accounts>(account)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public AccountModel UpdateAccount(Account updatedAccount)
    {
        if (IsItEmail(updatedAccount.Email))
        {
            throw new FormatException("Email format is incorrect!");
        }

        if (_repository.GetAll().Any(a => a.Email == updatedAccount.Email))
        {
            throw new ArgumentException("I cannot change your email address because an account with this new email adress already exist");
        }

        var result = _mapper
            .Map<AccountModel>(
                _repository
                    .Update(updatedAccount));
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
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        string encodedPassword = new PasswordCoder().ComputeSHA256Hash(password);

        return _mapper.Map<AccountModel>(
            _repository
                .GetAll()
                .SingleOrDefault(a =>
                        a.Email == email
                        && a.Password == encodedPassword));
    }
}
