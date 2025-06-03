using UmbracoPOCV13.HealthCheckHandlersMiddleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region HealthCheckMiddleware Registeration
builder.Services.AddSingleton<NoSniffMiddleware>();
builder.Services.AddSingleton(new ClickJackingMiddleware());
#endregion

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

#region HealthCheckAppRegisteration
app.UseMiddleware<NoSniffMiddleware>();
app.UseMiddleware<ClickJackingMiddleware>();
#endregion


await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });


await app.RunAsync();

