﻿using JLearning.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JLearning.Extensions
{
    public static class AddJWTServicesExtensions
    {
        public static void AddJWTServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Add Jwt Setings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            Services.AddSingleton(bindJwtSettings);
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("64A63153 - 11C1 - 4919 - 9133 - EFAF99A9B456")),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                   // ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }
    }
}
