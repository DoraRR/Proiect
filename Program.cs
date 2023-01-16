using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GymManager.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<GymManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymManagerContext") ?? throw new InvalidOperationException("Connection string 'GymManagerContext' not found.")));

builder.Services.AddDbContext<GymManagerIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GymManagerContext") ?? throw new InvalidOperationException("Connection string 'GymManagerContext' not found."))
);



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<GymManagerIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
