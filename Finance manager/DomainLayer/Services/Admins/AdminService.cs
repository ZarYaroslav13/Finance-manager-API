using AutoMapper;
using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.Security;
using Infrastructure.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services.Admins;

public class AdminService : BaseService, IAdminService
{
    private readonly IPasswordCoder _passwordCoder;
    private readonly IRepository<Admin> _repository;

    public const string NameAdminRole = "Admin";
    public const string NameAdminPolicy = "OnlyForAdmins";


    public AdminService(IPasswordCoder passwordCoder, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _passwordCoder = passwordCoder ?? throw new ArgumentNullException(nameof(passwordCoder));
        _repository = _unitOfWork.GetRepository<Admin>();
    }

    public string GetNameAdminRole() => NameAdminRole;

    public string GetNameAdminPolicy() => NameAdminPolicy;

    public List<AdminModel> GetAdmins()
    {
        return _repository.GetAllAsync().GetAwaiter().GetResult()
            .Select(_mapper.Map<AdminModel>)
            .ToList();
    }

    public async Task<AdminModel> TrySignInAsync(string email, string password)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
        ArgumentNullException.ThrowIfNullOrWhiteSpace(password);

        string encodedPassword = _passwordCoder.ComputeSHA256Hash(password);

        var account = (await _repository.GetAllAsync())
                .FirstOrDefault(a =>
                    a.Email == email
                    && a.Password == encodedPassword);

        var result = _mapper.Map<AdminModel>(account); ;

        return result;
    }

    public bool IsItAdmin(string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

        return GetAdmins().Any(a => a.Email == email);
    }
}
