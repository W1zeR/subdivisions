using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebMVC.Models;
using WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddHttpClient();
services.AddAutoMapper(typeof(SubdivisionRequestMapProfile));
services.AddScoped<ISubdivisionService, SubdivisionService>();
services.AddControllersWithViews();
services.AddWebEncoders(o =>
{
    o.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic,
        UnicodeRanges.CyrillicExtendedA, UnicodeRanges.CyrillicExtendedB);
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
