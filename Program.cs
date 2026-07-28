var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Plain machine-readable liveness/readiness probe for Kubernetes, separate from
// the human-facing /Status page.
app.MapGet("/healthz", () => Results.Ok(new { status = "Healthy" }));

app.Run();
