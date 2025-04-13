using Inventory_API.Services;
using Inventory_API.Services.Interface;
//using Inventory_API.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Inventory API",
            Description = "Currently its just a Get API.",
            TermsOfService = new Uri("https://hianime.to/home")
        })
);



builder.Services.AddDbContext<Inventory_API.Models.ErptestingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ERPDB"));
});

//checking more for something
//Services
builder.Services.AddScoped<IInventorySerivce, InventoryService>();
builder.Services.AddScoped<IInvProductManagementServices, InvProductManagementServices>();
builder.Services.AddScoped<IInvAlerts_AutomationServices, InvAlerts_AutomationServices>();
//builder.Services.AddScoped<IInvPurchaseManagementServices, InvPurchaseManagementServices>();
builder.Services.AddScoped<IInvReporting_AnalyticsServices, InvReporting_AnalyticsServices>();
builder.Services.AddScoped<IInvSalesOrderManagementServices, InvSalesOrderManagementServices>();
builder.Services.AddScoped<IInvStockManagementServices, InvStockManagementServices>();
builder.Services.AddScoped<IInvWarehouseLocationServices, InvWarehouseLocationServices>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
    var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
//app.MapHub<InventoryHub>("/inventoryHub");
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller?}/{action?}/{id?}"
 ).WithStaticAssets();

app.Run();
