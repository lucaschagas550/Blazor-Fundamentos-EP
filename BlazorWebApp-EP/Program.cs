using Blazored.LocalStorage;
using BlazorWebApp_EP.Components;
using BlazorWebApp_EP.Components.DI;
using BlazorWebApp_EP.Components.Roteamento.Users;
using BlazorWebApp_EP.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//Cascading Value
//builder.Services.AddCascadingValue(sp =>
//{
//    var StyleContext = new StyleContext { BackgroundColor = "#ADD8E6" };
//    var source = new CascadingValueSource<StyleContext>(StyleContext, isFixed: false);
//    return source;
//});


//Entity framework
builder.Services.AddDbContext<PrimeiraAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrimeiraAppContext")));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Redireciona para pagina de erro no arquivo Roteamento/Erros
app.UseStatusCodePagesWithRedirects("/erro/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery(); //AntiForgery para bloquear ataques CSRF

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebApp_EP.Client._Imports).Assembly);

app.Run();


//Propagar as alteracoes do cascadingValues para os componentes
public static class CascadingValueSource
{
    public static CascadingValueSource<T> CreateNotifying<T>(T value, bool isFixed = false) where T : INotifyPropertyChanged
    {
        var source = new CascadingValueSource<T>(value, isFixed);

        value.PropertyChanged += (sender, args) => source.NotifyChangedAsync();

        return source;
    }
}