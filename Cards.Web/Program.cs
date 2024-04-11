using Cards.Presentation;
using Cards.Web;
using Cards.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMvc()
    .AddApplicationPart(typeof(PresentationAssemblyReference).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureApiBehaviorOptions();

builder.Services.ConfigureCors();
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
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Card API v1");
    });
}
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCardsFrontendStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
