using Cards.Web;
using Cards.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddApplicationPart(Assembly.Load(new AssemblyName("Cards.Presentation")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigureNpgsqlContext(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureCardService();
builder.Services.ConfigureCardRepository();
builder.Services.ConfigureAppUserRepository();
builder.Services.ConfigureUnitOfWorkRepository();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthService();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


var app = builder.Build();

//HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
}
app.UseExceptionHandler("/Home/Error");

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Card API v1");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
