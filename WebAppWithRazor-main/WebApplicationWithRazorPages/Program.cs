using Bibliotheca.Data;

var builder = WebApplication.CreateBuilder(args);

// Register Razor Pages and the data access layer
builder.Services.AddRazorPages();
builder.Services.AddScoped<ILiteraryWorkRepository, SqlLiteraryWorkRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/SystemError");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
