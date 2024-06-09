using BlazorWebApp_EP.Components;
using BlazorWebApp_EP.Components.DI;
using Microsoft.AspNetCore.Components;
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
builder.Services.AddScoped<IProductService, ProductService>();

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

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