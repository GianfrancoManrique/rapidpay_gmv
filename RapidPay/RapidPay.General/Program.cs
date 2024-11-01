using RapidPay.General.Repositories;
using RapidPay.General.Services;

var builder = WebApplication.CreateBuilder(args);

// add services
builder.Services.AddScoped<ICardManagmentService, CardManagmentService>();
// add repositories
builder.Services.AddScoped<ICreditCardRepository, CreditCardRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    {
    // Enable Swagger in development mode
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
