using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MenuComida.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//para acceder al httpcontext desde el layout

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MenuComidaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MenuComidaContext") ?? throw new InvalidOperationException("Connection string 'MenuComidaContext' not found.")));


builder.Services.AddSession(); // Agrega esta línea para habilitar las sesiones.

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

// Agrega el middleware de sesión.
app.UseSession(); // Agrega esta línea antes de UseAuthorization.

app.UseAuthorization();

app.MapRazorPages();

app.Run();
