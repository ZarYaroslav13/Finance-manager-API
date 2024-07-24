using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer;

namespace DomainLayerTests
{
    [TestClass]
    public class FinanceReportCreatorTests
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IRepository<DataLayer.Models.FinanceOperation> _repository;

        protected readonly FinanceReportCreator _creator;
    }
}