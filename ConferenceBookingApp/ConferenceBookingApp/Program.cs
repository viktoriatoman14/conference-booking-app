using Microsoft.EntityFrameworkCore;
using ConferenceBookingApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies; // Dodane dla ciasteczek
using ConferenceBookingApp.Services; 



var builder = WebApplication.CreateBuilder(args);

// Pobranie konfiguracji po³¹czenia z pliku appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//Rejestracja naszego ApplicationDbContext w systemie
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Gdzie przekierowaæ, gdy ktoœ jest niezalogowany
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Czas sesji
    });

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, EmailSender>();

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();//czy mo¿e siê zalogowaæ
app.UseAuthorization();//jakie ma uprawnienia   

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
