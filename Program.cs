using GestorTareas.Data;
using GestorTareas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// 1. Configuración del motor SQlite viculada a gestortarea.db
builder.Services.AddDbContext<AppDbContext>( options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source = gestortareas.db"));

// 2. Registro del Servicio de Dominio para la inyeccion de dependencias
builder.Services.AddScoped<ITareaService,TareaService>(); 

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

app.UseAuthorization();

app.MapRazorPages();
//--------------------------------
//Migracion Automatica al arranque

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
	try
	{
		var context = services.GetRequiredService<AppDbContext>();
		context.Database.Migrate(); // Crea la base de datos y ejecuta el Seed Data automático
	}
	catch (Exception ex)
	{

		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "Error crítico al migrar o inicializar la base de datos.");
	}
}

//--------------------------------

app.Run();
