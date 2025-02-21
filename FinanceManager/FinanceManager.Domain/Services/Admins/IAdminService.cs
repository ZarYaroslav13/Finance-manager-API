﻿using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Services.Admins;

public interface IAdminService
{
    public string GetNameAdminRole();

    public string GetNameAdminPolicy();

    public List<AdminModel> GetAdmins();

    public Task<AdminModel> TrySignInAsync(string email, string password);

    public bool IsItAdmin(string email);
}
