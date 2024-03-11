using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Data;
using Mvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("MvcContext"),
	new MariaDbServerVersion(new Version(10, 3, 29)),
	mysqlOptions => mysqlOptions.MigrationsAssembly("Mvc")));

//vão poder ser injetados pelo mecanismo de injeção de dependencia
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<SalesRecordService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Obter uma instância do SeedingService e chamar o método de seeding
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var seedingService = services.GetRequiredService<SeedingService>();
	seedingService.Seed(); // Chame o método que realiza o seeding

	// Configure the HTTP request pipeline.





	var enUS = new CultureInfo("en-US"); //locação estados unidos
	var localizationOptions = new RequestLocalizationOptions
	{
		DefaultRequestCulture = new RequestCulture(enUS),
		SupportedCultures = new List<CultureInfo> { enUS },
		SupportedUICultures = new List<CultureInfo> { enUS }


	}; 
	app.UseRequestLocalization(localizationOptions);
	 // <------------------------------------------>
	if (!app.Environment.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
		seedingService.Seed();

	}
}
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
