﻿using ApplicationLayer.Mapper.Profiles;
using ApplicationLayer.Models;
using ApplicationLayerTests.Data.Mapper.Profiles;
using AutoMapper;
using DomainLayer.Models;
using ApplicationLayerTests.TestToolExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayerTests.Mapper.Profiles;

[TestClass]
public class AdminProfileTests
{
    private readonly IMapper _mapper;

    public AdminProfileTests()
    {
        _mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<AdminProfile>();
                })
            .CreateMapper();
    }

    [TestMethod]
    [DynamicData(nameof(AdminProfileTestDataProvider.DomainAdmin), typeof(AdminProfileTestDataProvider))]
    public void Map_AdminDataMappedCorrectly_AdminModels(AdminModel domainAdmin)
    {
        var appAdmin = _mapper.Map<AdminDTO>(domainAdmin);

        Assert.That.AreEqual(domainAdmin, appAdmin);
    }

    [TestMethod]
    [DynamicData(nameof(AdminProfileTestDataProvider.DomainAdmin), typeof(AdminProfileTestDataProvider))]
    public void Map_AdminDataAreNotLostAfterMapping_AdminModels(AdminModel domainAdmin)
    {
        var mappedomainAdmin = _mapper
            .Map<AdminModel>(
                _mapper
                    .Map<AdminDTO>(domainAdmin));

        Assert.AreEqual(domainAdmin, mappedomainAdmin);
    }
}
