using Microsoft.IdentityModel.Tokens;
using PPlusDostepy.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
builder.Services.AddSingleton<ICompanyService, CompanyService>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(cfg =>
    {
        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtIsuser"],
            ValidAudience = builder.Configuration["JwtIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]))
        };
    });
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterEndpoints();

app.MapGet("/token", () =>
{
    var claims = new[]
    {
            new Claim(ClaimTypes.NameIdentifier, "user-id"),
            new Claim(ClaimTypes.Name, "Test Name"),
            new Claim(ClaimTypes.Role, "Admin"),
    };

    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken
    (
        issuer: builder.Configuration["JwtIssuer"],
        audience: builder.Configuration["JwtIssuer"],
        claims: claims,
        expires: DateTime.UtcNow.AddDays(60),
        notBefore: DateTime.UtcNow,
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])), SecurityAlgorithms.HmacSha256)
    );

    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
    return Results.Ok(jwtToken);
});

app.Run();
#region PierwotnyKod
//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");
#endregion




//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}