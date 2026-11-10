using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.HttpOverrides;
using StoriesOfInclusion.Services;
using StoriesOfInclusion.Components;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor Server Interactive Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add HttpContextAccessor, Application State & Supabase Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AppStateService>();
builder.Services.AddSingleton<ISupabaseService, SupabaseService>();
builder.Services.AddSingleton<MemberPortfolioService>();

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

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

// Direct Role Login Endpoint (/any_user_login/{role})
app.MapGet("/any_user_login/{role}", (string role, HttpContext context) =>
{
    var normalizedRole = (role ?? "").Trim().ToLowerInvariant() switch
    {
        "superadmin" or "super_admin" or "super-admin" or "super" => "superadmin",
        "admin" or "staff" => "admin",
        "user" or "member" or "contributor" => "user",
        _ => "guest"
    };

    var redirectUrl = normalizedRole switch
    {
        "superadmin" => "/dashboard/superadmin",
        "admin" => "/dashboard/admin",
        "user" => "/dashboard/user",
        _ => "/"
    };

    context.Response.Cookies.Append("soi_active_role", normalizedRole, new CookieOptions
    {
        Path = "/",
        HttpOnly = false,
        SameSite = SameSiteMode.Lax,
        Expires = DateTimeOffset.UtcNow.AddDays(30)
    });

    return Results.Redirect(redirectUrl);
});

app.MapGet("/any_user_login", () => Results.Redirect("/role-login"));

// Map Blazor Interactive Server Root Component
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
