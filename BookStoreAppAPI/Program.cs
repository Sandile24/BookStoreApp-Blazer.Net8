using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//setting up the logger, reference from appsettings
//Serilo.aspnet, Serilog.Expresion, Serilog.Sink.Seq--These are required 
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

app.UseAuthorization();

app.MapControllers();

app.Run();
