using SecretHitlerServerFrontend.Classes;

var Builder = WebApplication.CreateBuilder(args);
Builder.Services.AddRazorPages();
Builder.Services.AddServerSideBlazor();
Builder.Services.AddSingleton<Games>();

var App = Builder.Build();

if (!App.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    App.UseHsts();
}

App.UseHttpsRedirection();

App.UseStaticFiles();

App.UseRouting();

App.MapBlazorHub();
App.MapFallbackToPage("/_Host");

App.Run();