using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application.Employees;
using WorkTimeTracker.Data;
using WorkTimeTracker.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ContainerDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Employee>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 6;

        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

if (!dbContext.Roles.Any())
{
    await roleManager.CreateAsync(new IdentityRole("HR"));
    await roleManager.CreateAsync(new IdentityRole("Pracownik"));
    await roleManager.CreateAsync(new IdentityRole("Manager"));
    await roleManager.CreateAsync(new IdentityRole("Dyrektor"));
    await roleManager.CreateAsync(new IdentityRole("Administrator"));
}

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();

if (!dbContext.Users.Any())
{
    var startAdmin = new Employee("Admin", "Admin", "", null, 0, DateTime.Now, null, null);
    var res1 = await userManager.CreateAsync(startAdmin, "Admin123!");
    var res2 = await userManager.AddToRoleAsync(startAdmin, "Administrator");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
