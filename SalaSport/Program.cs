using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalaSport.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalaSportContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalaSportContext")
        ?? throw new InvalidOperationException("Connection string 'SalaSportContext' not found.")));

builder.Services.AddDbContext<SalaSportIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalaSportContext")
        ?? throw new InvalidOperationException("Connection string 'SalaSportContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<SalaSportIdentityContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");

    options.Conventions.AllowAnonymousToPage("/Trainers/Index");
    options.Conventions.AllowAnonymousToPage("/Trainers/Details");

    options.Conventions.AllowAnonymousToPage("/Subscriptions/Index");
    options.Conventions.AllowAnonymousToPage("/Subscriptions/Details");

    options.Conventions.AuthorizePage("/Trainers/Create", "AdminPolicy");
    options.Conventions.AuthorizePage("/Trainers/Edit", "AdminPolicy");
    options.Conventions.AuthorizePage("/Trainers/Delete", "AdminPolicy");

    options.Conventions.AuthorizePage("/Subscriptions/Create", "AdminPolicy");
    options.Conventions.AuthorizePage("/Subscriptions/Edit", "AdminPolicy");
    options.Conventions.AuthorizePage("/Subscriptions/Delete", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/My");

    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Appointments", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/MemberSubscriptions", "AdminPolicy");

});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "Trainer", "User" };
    foreach (var r in roles)
        if (!await roleManager.RoleExistsAsync(r))
            await roleManager.CreateAsync(new IdentityRole(r));

    var adminEmail = "admin@fitness.local";
    var admin = await userManager.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, "Admin");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();


app.Run();
