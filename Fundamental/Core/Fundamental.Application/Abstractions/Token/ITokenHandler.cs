using Fundamental.Application.DTOs.TokenDTOs;
using Fundamental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        TokenDto CreateAccessToken(AppUser user,int minute);
        List<Claim> CreateClaims(AppUser user);

    }
}
