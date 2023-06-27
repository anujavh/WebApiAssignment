
global using WebApiAssignemnt.Models;
global using WebApiAssignemnt.Data;
using WebApiAssignemnt.Services.UserDetailService;
using WebApiAssignemnt.Services.MessageDetailService;
using WebApiAssignemnt.AutoMapperConfig;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
//var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders().AddConsole();

builder.Logging.AddConsole();


builder.Services.AddDbContext<DataContext>();
//options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found."))
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IUserDetailService, UserDetailService>();
builder.Services.AddScoped<ISendMessageService, SendMessageService>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));


builder.Services.AddAuthentication().AddJwtBearer();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
