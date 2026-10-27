using Bibliotheca.Data;
using Bibliotheca.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotheca.Pages.Catalog;

public class CatalogEditModel : PageModel
{
    private readonly ILiteraryWorkRepository _archive;

    public CatalogEditModel(ILiteraryWorkRepository archive)
    {
        _archive = archive;
    }

    [BindProperty]
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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        int updated = _archive.UpdateWork(Entry);

        if (updated == 0)
        {
            return NotFound();
        }

        return RedirectToPage("List");
    }
}
