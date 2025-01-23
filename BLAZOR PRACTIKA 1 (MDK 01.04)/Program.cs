using BLAZOR_PRACTIKA_1.NDK_01_0U.ApiRequest;
using BLAZOR_PRACTIKA_1__MDK_01._04_.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ApiRequestService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7102/") });
builder.Services.AddSingleton<UserService>();

// Регистрация ApiRequestService
builder.Services.AddScoped<ApiRequestService>();
await builder.Build().RunAsync();

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
