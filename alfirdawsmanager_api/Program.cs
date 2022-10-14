using alfirdawsmanager.Data.Models;
using alfirdawsmanager.Service.Extensions;
using alfirdawsmanager.Service.Helpers.EmailHelpers;
using alfirdawsmanager.Service.Infrastructure;
using alfirdawsmanager.Service.Interface;
using alfirdawsmanager.Service.Service;
using AutoMapper;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJWTTokenServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Alfirdawsmanager API",
        Description = "An API to manage the complete back-end of the Al.firdaws platform",

    });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "alfirdawsmanager_api.xml");
    options.IncludeXmlComments(filePath);
});
builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
    build.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
}));

#region Service and Interface Injection
builder.Services.AddScoped<AlfirdawsManagerDbContext, AlfirdawsManagerDbContext>();
builder.Services.AddScoped<IAuthenticateInterface, AuthenticateService>();
builder.Services.AddScoped<ISettingsInterface, SettingsService>();
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IRoleInterface, RoleService>();
builder.Services.AddScoped<IModuleInterface, ModuleService>();
builder.Services.AddScoped<ICategoryInterface, CategoryService>();
builder.Services.AddScoped<ICountryInterface, CountryService>();
builder.Services.AddScoped<ICampaignTypeInterface, CampaignTypeService>();
builder.Services.AddScoped<IReachTypeInterface, ReachTypeService>();
builder.Services.AddScoped<IPeriodTypeInterface, PeriodTypeService>();
builder.Services.AddScoped<ICampaignInterface, CampaignService>();
builder.Services.AddScoped<ISubscriptionModelInterface, SubscriptionModelService>();
builder.Services.AddScoped<IAddressTypeInterface, AddressTypeService>();
builder.Services.AddScoped<IPaymentTypeInterface, PaymentTypeService>();
builder.Services.AddScoped<IInvoiceTypeInterface, InvoiceTypeService>();
builder.Services.AddScoped<IVatTypeInterface, VatTypeService>();
builder.Services.AddScoped<IPricingModelInterface, PricingModelService>();
builder.Services.AddScoped<ICaseTypeInterface, CaseTypeService>();
#endregion

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfigurator());
});

var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();
Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment = app.Services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();

ActivationEmail.Initialize(_hostingEnvironment);




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
