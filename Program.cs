using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Rewrite;
using eTickets.Data.Cart;
using eTicket.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Private configs
// builder.Configuration.AddIniFile("config.ini", optional: false, reloadOnChange: true);
// builder.Configuration.AddEnvironmentVariables();
// // Add DbContext config
// builder.Services.AddDbContext<AppDbContext>(
//     options => options.UseSqlServer(builder.Configuration["Database:ConnectionString"]));

// Add DbContext config
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IEntityBaseRepository<Actor>, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IOrdersService, OrdersServices>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(ShoppingCart.GetShoppingCart);
builder.Services.AddSession();

// Authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// builder.Services.AddHttpLogging(logging =>
// {
//     logging.LoggingFields = HttpLoggingFields.RequestBody |
//                             HttpLoggingFields.RequestPath;
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

// Authentication and authorization
app.UseAuthentication();
app.UseAuthorization();


// app.Use(async (context, next) =>
// {
//     context.Request.EnableBuffering();
//     // Leave the body open so the next middleware can read it.
//     using (var reader = new StreamReader(
//         context.Request.Body,
//         encoding: Encoding.UTF8,
//         leaveOpen: true))
//     {
//         var body = await reader.ReadToEndAsync();
//         if (body is not null)
//         {
//             Console.WriteLine($"Request body: {context.Request.Method} {HttpUtility.UrlDecode(body)}");
//         }
//         // Reset the request body stream position so the next middleware can read it
//         context.Request.Body.Position = 0;
//     }
//     await next(context);
// });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

// Seed database
AppDbInitializer.Seed(app);
await AppDbInitializer.SeedUsersAndRolesAsync(app);

app.Run();
