using BulkyBooks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Make the program use sql server and pass the connection string to
//the ApplicationDBContext constructor through the options object.
//Program.cs will search DefaultConnection in the ConnectionStrings block
//defined in the appsettings.json file.
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

//Afterwards, make sure that the following packages are installed in NuGet:
//MicrosotEntityFrameworkCore
//MicrosotEntityFrameworkCore.sqlserver
//MicrosotEntityFrameworkCore.tools
//Then, enter the following command on the nuget console:
// add-migration AddCategoryToDatabase

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
