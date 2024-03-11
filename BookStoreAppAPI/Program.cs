using BookStoreAppAPI.Configuration;
using BookStoreAppAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("BookStoreConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseSqlServer(conString);
});

//ApiUser inserted inside the AddIdentityCore not to leave out the fields that were added to the User table
builder.Services.AddIdentityCore<ApiUser>()
    .AddRoles<IdentityRole>()// intended for identifying the role of a user in a system
    .AddEntityFrameworkStores<BookStoreDbContext>();

//add this for automapper to work.
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//setting up the logger, reference from appsettings
//Serilog.aspnet, Serilog.Expresion, Serilog.Sink.Seq--These are required 
//for the log to work.
builder.Host.UseSerilog((ctx, lc)=>
lc.WriteTo.Console().ReadFrom!.Configuration(ctx.Configuration));;


//to allow clients from any where to access the system
//for this to work, add the (app.UseCors("AllowAll");)
//this configuration allows unrestricted cross-origin
//access to the specified resource,
//which may be necessary during development
//but should be used cautiously in production environments.
builder.Services.AddCors(options=> {
    options.AddPolicy("AllowAll", 
     b => b
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());
});


//First install Microsoft.AspNetCore.Authentication.JwtBearer
//then add the below code
//This is used to authenticate users
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//to allow clients from any where to access the system
//By using app.UseCors("AllowAll"),
//you're essentially enabling CORS support for your application,
//allowing cross-origin requests according to the rules defined in the "AllowAll" policy.
app.UseCors("AllowAll");

app.UseAuthentication();// add this after adding authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
