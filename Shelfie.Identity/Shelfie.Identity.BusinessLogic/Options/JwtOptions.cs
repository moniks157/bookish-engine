﻿namespace Shelfie.Identity.BusinessLogic.Options;

public sealed class JwtOptions
{
    public string? Secret { get; set; }
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
    public double TokenExpiration { get; set; }
}
