using BookStoreApp.Client.Pages;
using BookStoreApp.Components;
using BookStoreApp.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//Interfaces come from Services that were auto generated on NSwagStudio
//Inside NSwag I ticked the Checkbox "Use the Base Url for request"
//alternatively you can unselect the checkbox
//this line interact with the API
builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7151"));
//builder.Services.AddSingleton(provider => "https://localhost:7151");
//builder.Services.AddScoped<IClient, Client>();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BookStoreApp.Client._Imports).Assembly);

app.Run();
