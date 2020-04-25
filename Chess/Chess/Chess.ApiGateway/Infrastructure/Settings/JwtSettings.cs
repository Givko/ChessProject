﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string ExpirationInMinutes { get; set; }
        public string Issuer { get; set; }
        public string[] Audiences { get; set; }
    }
}
