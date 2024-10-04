using Microsoft.EntityFrameworkCore;
using CisternasGAMC.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddRazorPages(); // Habilitar Razor Pages en el proyecto.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") // Usar la cadena de conexi�n definida en la configuraci�n.
    )
);

var app = builder.Build();

// Configurar el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Usar la p�gina de manejo de errores en producci�n.

    // El valor predeterminado de HSTS es 30 d�as. Es recomendable ajustar esto en escenarios de producci�n.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redirigir autom�ticamente a HTTPS.
app.UseStaticFiles();      // Habilitar el uso de archivos est�ticos.

app.UseRouting();          // Habilitar el enrutamiento.

app.UseAuthorization();    // Habilitar la autorizaci�n.

app.MapRazorPages();       // Mapear las Razor Pages para que respondan a las solicitudes.

app.Run();                 // Ejecutar la aplicaci�n.
