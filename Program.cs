using System.Globalization;
using Microsoft.AspNetCore.Localization;
using StoriesOfInclusion.Services;
using StoriesOfInclusion.Components;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor Server Interactive Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Application State & Supabase Services
builder.Services.AddScoped<AppStateService>();
builder.Services.AddSingleton<ISupabaseService, SupabaseService>();

// Configure Bilingual Localization (English & Bengali)
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add Authentication & Authorization Services
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("superadmin"));
    options.AddPolicy("AdminAndAbove", policy => policy.RequireRole("superadmin", "admin"));
    options.AddPolicy("UserAndAbove", policy => policy.RequireRole("superadmin", "admin", "user"));
});

var app = builder.Build();

// Configure Localization Middleware
var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("bn") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
localizationOptions.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
app.UseRequestLocalization(localizationOptions);

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

// Map Blazor Interactive Server Root Component
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
