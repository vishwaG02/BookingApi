using Booking.App;
using Booking.App.Middleware;
using Booking.BAL.ViewModel.Booking;
using Booking.DAL.Context;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration, builder.Environment);
// Add services to the container.

builder.Services.AddControllers(options =>
{
    var jsonInputFormatter = options.InputFormatters
        .OfType<SystemTextJsonInputFormatter>()
        .FirstOrDefault();

    if (jsonInputFormatter != null)
    {
        jsonInputFormatter.SupportedMediaTypes.Clear();
        jsonInputFormatter.SupportedMediaTypes.Add("application/json");
    }
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking.App", Version = "v1" });
    c.ExampleFilters();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "X-API-Key",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Enter your API key in the 'X-API-Key' header"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference= new OpenApiReference
               {
                   Type=ReferenceType.SecurityScheme,
                   Id="ApiKey"
               }
            },
             Array.Empty<string>()
        }
   });
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateBookingRequestExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CancelBookingResponseExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateBookingResponseExample>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

// 📘 ReDoc — nice read-only docs
app.UseReDoc(c =>
{
    c.RoutePrefix = "docs";
    c.SpecUrl = "/swagger/v1/swagger.json";
    c.DocumentTitle = "My API Docs (ReDoc)";
});

// ✅ Redirect /swagger to /docs BEFORE routing
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path.StartsWithSegments("/swagger"))
//    {
//        context.Response.Redirect("/docs");
//        return;
//    }
//    await next();
//});

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
