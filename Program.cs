using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddRazorPages(); // Habilitar Razor Pages en el proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") // Usar la cadena de conexión definida en la configuración.
    )
);

var app = builder.Build();

// Configurar el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Usar la página de manejo de errores en producción.

    // El valor predeterminado de HSTS es 30 días. Es recomendable ajustar esto en escenarios de producción.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirigir automáticamente a HTTPS.
app.UseStaticFiles();      // Habilitar el uso de archivos estáticos.

app.UseRouting();          // Habilitar el enrutamiento.

app.UseAuthorization();    // Habilitar la autorización.

app.MapRazorPages();       // Mapear las Razor Pages para que respondan a las solicitudes.

app.Run();                 // Ejecutar la aplicación.
