
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

using Lotus.LSG;
using Lotus.Web.Account;

//
// �������
//
var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services.AddControllers();


    builder.Services.AddBlazorise(options =>
    {
        options.ChangeTextOnKeyPress = true; // optional
    })
      .AddBootstrapProviders()
      .AddFontAwesomeIcons();

    // ����������� HttpClient
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddHttpClient(CAuthorizeApi.HttpClientName, (HttpClient client) =>
        {
            client.BaseAddress = new Uri("http://localhost:39992/");
        });
    }
    else
    {
        builder.Services.AddHttpClient(CAuthorizeApi.HttpClientName, (IServiceProvider service_provider, HttpClient client) =>
        {
            client.BaseAddress = new Uri("http://bredy-demente.1gb.ru/");
        });
    }


    //
    // ���� ������
    //
    builder.Services.AddDbContext<CRepositoryDatabase>(options =>
    {
        //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

        //
        // MySql
        //
        String connect_str = builder.Configuration.GetConnectionString("DefaultConnection");
        MySqlServerVersion server_version = new MySqlServerVersion(new Version(5, 7, 0));
        options.UseMySql(connect_str, server_version, (options_builder_mysql) =>
        {

        });
    });

    //
    // ��������
    //
    builder.ConfigureAccount("DefaultConnection");
}

//
// ��������
//
var app = builder.Build();
{
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

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseAccount();



    // � ������ Configure ��� ����������� �������� �����, � �������� ����� ����������� �������� �������, 
    // ���������� ����� MapBlazorHub(), ������� ��������� ���������� ����������� � ��������� ����������� SignalR. 
    // ��������� ���� ����� ����������� ����� ����������� ����� �������� � �������� � ������ ��������� �������. 
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapBlazorHub();

        // ����� ����, ����� ������ MapFallbackToPage("/_Host") ��������� ���������� �������� Razor Page �� ��������� ��� ����������
        endpoints.MapFallbackToPage("/_Host");
    });

    app.Run();
}
