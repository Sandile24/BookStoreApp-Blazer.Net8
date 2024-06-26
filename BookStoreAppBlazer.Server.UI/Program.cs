using Blazored.LocalStorage;
using BookStoreAppBlazer.Server.UI.Configurations;
using BookStoreAppBlazer.Server.UI.Providers;
using BookStoreAppBlazer.Server.UI.Services;
using BookStoreAppBlazer.Server.UI.Services.Authentication;
using BookStoreAppBlazer.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//add this after installing Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();
//After autogenerate of ServiceClient  class, add this line of code
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7151"));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

//add this for automapper to work.
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
p.GetRequiredService<ApiAuthenticationStateProvider>());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
