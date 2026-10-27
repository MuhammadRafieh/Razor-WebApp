using Bibliotheca.Data;
using Bibliotheca.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bibliotheca.Pages.Catalog;

public class CatalogAddModel : PageModel
{
    private readonly ILiteraryWorkRepository _archive;

    public CatalogAddModel(ILiteraryWorkRepository archive)
    {
        _archive = archive;
    }

    [BindProperty]
    public LiteraryWork NewEntry { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _archive.CreateWork(NewEntry);

        return RedirectToPage("List");
    }
}
