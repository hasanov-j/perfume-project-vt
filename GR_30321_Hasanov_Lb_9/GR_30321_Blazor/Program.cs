using GR_30321_Blazor.Components;
using GR_30321_Blazor.Services;
using GR_30321_Hasanov_Lb_3_Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IPerfumeService<Perfume>, ApiPerfumeService>();

builder.Services
.AddHttpClient<IPerfumeService<Perfume>, ApiPerfumeService>(c => 
c.BaseAddress = new Uri("https://localhost:7002/api/perfumes"));

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
