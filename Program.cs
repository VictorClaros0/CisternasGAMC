using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddRazorPages(); // Habilitar Razor Pages en el proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") // Usar la cadena de conexi�n definida en la configuraci�n.
    )
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>() // Agregar Identity
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = "UserLoginCookie";
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

var app = builder.Build();

// Configurar el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Usar la p�gina de manejo de errores en producci�n.
    app.UseHsts(); // Habilitar HSTS
}

app.UseHttpsRedirection(); // Redirigir autom�ticamente a HTTPS.
app.UseStaticFiles();      // Habilitar el uso de archivos est�ticos.

app.UseRouting();          // Habilitar el enrutamiento.

app.UseAuthentication();   // Habilitar la autenticaci�n.
app.UseAuthorization();    // Habilitar la autorizaci�n.

app.MapRazorPages();       // Mapear las Razor Pages para que respondan a las solicitudes.

app.Run();                 // Ejecutar la aplicaci�n.
