using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace PocStatus.Pages;

public class StatusModel(IWebHostEnvironment hostEnvironment) : PageModel
{
    public string HostName { get; private set; } = string.Empty;
    public string EnvironmentName { get; private set; } = string.Empty;
    public DateTime UtcNow { get; private set; }
    public string Version { get; private set; } = string.Empty;

    public void OnGet()
    {
        // HOSTNAME is set by Kubernetes to the pod name; falls back to the machine name
        // when running outside a container (e.g. `dotnet run` on a laptop).
        HostName = Environment.GetEnvironmentVariable("HOSTNAME")
            ?? Environment.MachineName;
        EnvironmentName = hostEnvironment.EnvironmentName;
        UtcNow = DateTime.UtcNow;
        Version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "unknown";
    }
}
