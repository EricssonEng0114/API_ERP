using API_ERP.Data;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autofac.Core;
using Microsoft.AspNetCore.CookiePolicy;
using System.Configuration;
using API_ERP.Models.DB.ViewModels;
using API_ERP.Interfaces;
using API_ERP.Services.Common;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
});

builder.Services.AddCascadingAuthenticationState();

#region JWT Setup
// configure strongly typed settings objects
var appSettingsSection = configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);


// Add JWT authentication services
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
#endregion

#region Register DB Setup
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    {
        options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.EnableSensitiveDataLogging(true);
    });

#endregion

#region services for enable the Dependency Injection.
////*Util Services*////
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserService, UserService>();
#endregion

builder.Services.AddApplicationInsightsTelemetry();

#region for cookies
builder.Services.Configure<CookiePolicyOptions>(options =>
{

    // prevent access from javascript 
    options.HttpOnly = HttpOnlyPolicy.Always;

    // If the URI that provides the cookie is HTTPS, 
    // cookie will be sent ONLY for HTTPS requests 
    // (refer mozilla docs for details) 
    options.Secure = CookieSecurePolicy.SameAsRequest;

    // refer "SameSite cookies" on mozilla website 
    options.MinimumSameSitePolicy = SameSiteMode.None;

});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    //endpoints.MapRazorPages();
});

#region for cookies - add the CookiePolicy middleware 
app.UseCookiePolicy();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
#endregion

app.Use(async (ctx, next) =>
{
    await next().ConfigureAwait(false);

    if ((ctx.Response.StatusCode == 404 || ctx.Response.StatusCode == 500) && !ctx.Response.HasStarted)
    {
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/";
        await next().ConfigureAwait(false);
    }
});

app.MapControllers();


app.Run();
