﻿using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Models.Base;

public class HumanDTO : ModelDTO
{
    [Required]
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value.Trim(); }
    }

    [Required]
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value.Trim(); }
    }

    [Required]
    [EmailAddress]
    public string Email
    {
        get { return _email; }
        set { _email = value.Trim(); }
    }

    [Required]
    [Length(10, 50)]
    [DataType(DataType.Password)]
    public string Password
    {
        get { return _password; }
        set { _password = value.Trim(); }
    }

    private string _lastName = string.Empty;
    private string _firstName = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;
}
