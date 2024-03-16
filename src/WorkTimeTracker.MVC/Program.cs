using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Application;
using WorkTimeTracker.Application.ApplicationUser;
using WorkTimeTracker.Domain.Entities;
using WorkTimeTracker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

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
    foreach (var role in Enum.GetValues(typeof(Roles)))
    {
        await roleManager.CreateAsync(new IdentityRole(role.ToString()!));
    }
}

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

if (!dbContext.Users.Any())
{
    var startAdmin = new Employee("Admin", "Admin", "", null, 0, DateTime.Now, null, null);
    await userManager.SetEmailAsync(startAdmin, "admin@admin.pl");
    await userManager.SetUserNameAsync(startAdmin, "admin@admin.pl");
    var res1 = await userManager.CreateAsync(startAdmin, "Admin123!");
    var res2 = await userManager.AddToRoleAsync(startAdmin, Roles.Admin.ToString());
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
