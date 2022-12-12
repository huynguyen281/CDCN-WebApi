using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SpendingManagement.Application.Accounts;
using SpendingManagement.Application.Budgets;
using SpendingManagement.Application.Categories;
using SpendingManagement.Application.Expenditures;
using SpendingManagement.Application.Statistics;
using SpendingManagement.Application.Tokens;
using SpendingManagement.Data.EF;
using SpendingManagement.Data.Entities;
using SpendingManagement.Repository.Budgets;
using SpendingManagement.Repository.Categories;
using SpendingManagement.Repository.Expenditures;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<IExpenditureService, ExpenditureService>();
builder.Services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IBudgetRepository, BudgetRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images\Categories")),
    RequestPath = new PathString("/Images/Categories")
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
