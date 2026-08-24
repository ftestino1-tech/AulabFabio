using AulabChronicle.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AulabChronicle.Repositories;
using AulabChronicle.Services;
using AulabChronicle.Models.ViewModels;
using AulabChronicle.Models.Domain;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AulabDbContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("ChroniclePostConnectionString"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ChroniclePostConnectionString"))
    )
);


builder.Services.ConfigureApplicationCookie(options =>
{
   options.LoginPath = "/Account/Login" ;
   options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>(); 
builder.Services.AddScoped<ICrudService<CategoryDto, Category, long>, CategoryService>();
builder.Services.AddScoped<ICrudService<ArticleDto, Article, long>, ArticleService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IArticleImageRepository, ArticleImageRepository>();
builder.Services.AddScoped<IImageService, SupabaseImageService>();
builder.Services.AddScoped<ICareerRequestRepository, CareerRequestRepository>();
builder.Services.AddScoped<ICareerRequestService, CareerRequestService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AulabDbContext>()
    .AddDefaultTokenProviders(); 


builder.Services.Configure<IdentityOptions>(options =>
{
   options.Password.RequireDigit = true; 
   options.Password.RequireLowercase = true; 
   options.Password.RequireNonAlphanumeric = true; 
   options.Password.RequireUppercase = true; 
   options.Password.RequiredLength = 6; 
   options.Password.RequiredUniqueChars = 1; 
   options.User.AllowedUserNameCharacters = 
       "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789 -._@+"; 
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
