using Bibliotheca.Data;
using Bibliotheca.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotheca.Pages.Catalog;

public class CatalogRemoveModel : PageModel
{
    private readonly ILiteraryWorkRepository _archive;

    public CatalogRemoveModel(ILiteraryWorkRepository archive)
    {
        _archive = archive;
    }

    public LiteraryWork Entry { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        var work = _archive.FindWorkById(id);

        if (work is null)
        {
            return NotFound();
        }

        Entry = work;
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        int removed = _archive.DeleteWork(id);

        if (removed == 0)
        {
            return NotFound();
        }

        return RedirectToPage("List");
    }
}
