using careerPortal.Models;
using careerPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpClient for API calls
builder.Services.AddHttpClient("UsaJobs", client =>
{
    client.BaseAddress = new Uri("https://data.usajobs.gov/");
   // client.DefaultRequestHeaders.Add("User-Agent", builder.Configuration["UsaJobs:UserAgent"]);
    client.DefaultRequestHeaders.Add("Authorization-Key", builder.Configuration["UsaJobs:ApiKey"]);
   // client.DefaultRequestHeaders.Add("Host", "data.usajobs.gov");
});

// If using Entity Framework (uncomment if needed)
// builder.Services.AddDbContext<JobDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add this to your services
builder.Services.AddSingleton<IJobManagerService, JobManagerService>();

builder.Services.AddSingleton<JobService>();

// Configure session state (if needed for login)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable session (if added above)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();