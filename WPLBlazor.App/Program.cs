
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.EntityFrameworkCore;
using WPLBlazor.App.Data;
using WPLBlazor.App.Components;

var builder = WebApplication.CreateBuilder(args);

// Add API Services

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//Add Authentication Services



// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<WPLDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WPLStatsDB")));

builder.Services    
.AddBlazorise( options =>
     {
         options.Immediate = true;
     })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
