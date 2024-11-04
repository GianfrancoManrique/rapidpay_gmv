using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RapidPay.General.Repositories;
using RapidPay.General.Services.Implementations;
using RapidPay.General.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUFEService, UFEService>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddScoped<ICardManagmentService, CardManagmentService>();
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
