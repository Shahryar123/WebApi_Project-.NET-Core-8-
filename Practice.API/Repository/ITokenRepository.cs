﻿using Microsoft.AspNetCore.Identity;

namespace Practice.API.Repository
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
