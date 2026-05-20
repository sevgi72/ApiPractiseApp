using WebApplication2.Filters;
using WebApplication2.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Register the handler
builder.Services.AddTransient<AuthTokenHandler>();

// Register a named client with the handler
builder.Services.AddHttpClient("ApiClient")
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddTransient<TokenAuthFilterAttribute>();

// Overwrite the default client if requested without name by any controllers
builder.Services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName)
    .AddHttpMessageHandler<AuthTokenHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
