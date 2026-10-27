using Bibliotheca.Data;
using Bibliotheca.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotheca.Pages.Catalog;

public class CatalogListModel : PageModel
{
    private readonly ILiteraryWorkRepository _archive;

    public CatalogListModel(ILiteraryWorkRepository archive)
    {
        _archive = archive;
    }

    public List<LiteraryWork> AllWorks { get; set; } = new();

    public void OnGet()
    {
        AllWorks = _archive.ListAllWorks();
    }
}
