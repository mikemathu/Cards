using Cards.Web.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureNpgsqlContext(builder.Configuration);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureCardService();
builder.Services.ConfigureCardRepository();
builder.Services.ConfigureAppUserRepository();
builder.Services.ConfigureUnitOfWorkRepository();


builder.Services.AddControllers()
.AddApplicationPart(Assembly.Load(new AssemblyName("Cards.Presentation")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
