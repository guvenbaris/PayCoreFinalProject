using AutoMapper;
using Moq;
using NHibernate;
using PayCore.Application.AutoMapperProfiles;
using PayCore.Application.Interfaces.Jwt;
using PayCore.Application.Interfaces.Services;
using PayCore.Application.Interfaces.UnitOfWork;
using PayCore.Application.Models;
using PayCore.BusinessService.Services;
using PayCore.Domain.Entities;
using PayCore.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paycore.TestX.AuthTests
{
    public class AuthServiceTest
    {
        private readonly Mock<IUserService> _userSeviceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;

        private readonly IMapper _mapperMock;


    }
}
