using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models;

public class Account : Base.Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Login {  get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}