using Microsoft.EntityFrameworkCore;
using Blog.Web.Data;
using Blog.Web.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDbContext>(
    options =>options.UseMySql(builder.Configuration.GetConnectionString("BloggieDbConnectionString"), 
    new MySqlServerVersion(new Version(8, 0, 0)))
);

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>(); 


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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
