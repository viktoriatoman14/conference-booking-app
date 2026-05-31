using Microsoft.EntityFrameworkCore;
using ConferenceBookingApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // Dodane dla ciasteczek

var builder = WebApplication.CreateBuilder(args);

// 1. Pobranie konfiguracji po³¹czenia z pliku appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Rejestracja naszego ApplicationDbContext w systemie
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// ==========================================
// NOWOŒÆ: Rejestracja autentykacji ciasteczkowej
// ==========================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Gdzie przekierowaæ, gdy ktoœ jest niezalogowany
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Czas sesji
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ==========================================
// WA¯NE: Najpierw sprawdzamy KIM KTOŒ JEST (Authentication), 
// a potem CO MO¯E ROBIÆ (Authorization)
// ==========================================
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
