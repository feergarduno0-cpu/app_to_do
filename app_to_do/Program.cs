using app_to_do.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores y vistas
builder.Services.AddControllersWithViews();

// Registrar el servicio de autenticación con MySQL
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Account/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// CONFIGURACIÓN PARA QUE INICIE EN EL LOGIN:
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();