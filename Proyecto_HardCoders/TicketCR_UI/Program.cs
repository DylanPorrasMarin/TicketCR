var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//PARA HACER CAMBIOS EN VIVO, SIN NECESIDAD DE ELIMINAR CACHE
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


//Hbilitar el service para acceder a la sesion
builder.Services.AddHttpContextAccessor();

//Crear una sesion default para la aplicacion
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "TicketCR"; //El usuario en sesion
    options.IdleTimeout = TimeSpan.FromSeconds(9999999); //Cuando tiempo se mantendra la sesion despues de abandonar la sesion
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HardcodersLanding}/{action=LandingHardcoders}/{id?}");
app.MapControllerRoute(
    name: "ComprarBoletos",
    pattern: "{controller=TicketCR}/{action=ComprarBoletos}/{id?}");
app.Run();
