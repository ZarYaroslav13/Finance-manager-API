﻿using AutoMapper;
using DomainLayer.Services.Accounts;
using Finance_manager_API.Controllers.Base;
using Finance_manager_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finance_manager_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : EntityController<AccountDTO>
    {
        AccountService _accountService;

        public AccountController(ILogger<EntityController<AccountDTO>> logger, IMapper mapper, AccountService service) : base(logger, mapper)
        {
            _accountService = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<AccountDTO> Get()
        {
            return new List<AccountDTO>();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public AccountDTO Get(int id)
        {
            return new AccountDTO();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post(AccountDTO account)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, AccountDTO value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
