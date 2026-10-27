using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotheca.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class SystemErrorModel : PageModel
{
    public string? TraceId { get; set; }

    public bool HasTraceId => !string.IsNullOrEmpty(TraceId);

    public void OnGet()
    {
        TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
