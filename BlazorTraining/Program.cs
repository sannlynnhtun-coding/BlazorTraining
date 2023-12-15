using BlazorTraining.Api;
using BlazorTraining.Data;
using BlazorTraining.Db;
using BlazorTraining.Services;
using BlazorTraining.StateContainer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();

builder.Services.AddScoped<CounterStateContainer>();

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddScoped<InjectService>();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
}, 
ServiceLifetime.Transient, 
ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
