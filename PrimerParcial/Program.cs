using Microsoft.EntityFrameworkCore;
using PrimerParcial.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC :)
builder.Services.AddControllersWithViews();

// DbContext con la cadena "DefaultConnection"
builder.Services.AddDbContext<RecetasDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();