using Cards.Frontend;
using Cards.Presentation;
using Cards.Web;
using Cards.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(PresentationAssemblyReference).Assembly;
var frontendAssembly = typeof(FrontendAssemblyReference).Assembly;

builder.Services.AddMvc()
    .AddApplicationPart(presentationAssembly)
    .AddApplicationPart(frontendAssembly);

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
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
