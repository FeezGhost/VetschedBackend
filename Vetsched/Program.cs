using Loader.infrastructure.Extensions;
using Loader.infrastructure.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Entities;
using Vetsched.Data.MappingProfiles;
using Vetsched.Services.PetSrv;
using Vetsched.Services.IdentitySrv;
using Vetsched.Services.ServicesProviderSrv;
using Vetsched.Services.UserProfileSrv;
using Vetsched.Helper.Conversions;
using Loader.infrastructure.Helper.Auth;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;

//builder.Services.AddDbContext<VetschedContext>(options =>
//    options.UseNpgsql(
//        configuration.GetConnectionString("DefaultConnection"),
//        npgsqlOptionsAction: sqlOptions =>
//        {
//            sqlOptions.MigrationsAssembly("api");
//            sqlOptions.EnableRetryOnFailure(
//            maxRetryCount: 10,
//            maxRetryDelay: TimeSpan.FromSeconds(30),
//            errorCodesToAdd: null);
//}), ServiceLifetime.Scoped);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));



builder.Services.ConfigureDBContext(configuration);
builder.Services.ConfigureAuthentication(configuration);
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
     .AddEntityFrameworkStores<VetschedContext>()
     .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
options.TokenLifespan = TimeSpan.FromDays(31));

//AutoMapper Configuration
builder.Services.AddAutoMapper(mc =>
{
    mc.AddProfile<VetschedMappingProfile>();
});

//Services
builder.Services.AddScoped<IdentityServiceInterface, IdentityService>();
builder.Services.AddScoped<IServicesProviderService, ServicesProviderService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IPetService, PetService>();
//Repositories
builder.Services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
//Helper
builder.Services.AddScoped<IAuthHelper, AuthHelper>();
builder.Services.AddScoped<IConversionHelper, ConversionHelper>();

builder.Services.AddControllers();
builder.Services.AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
//{
//    var VetContext = serviceScope.ServiceProvider.GetService<VetschedContext>();
//    VetContext.Database.Migrate();

//    using (var conn = (NpgsqlConnection)VetContext.Database.GetDbConnection())
//    {
//        conn.Open();
//        conn.ReloadTypes();
//    }
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
