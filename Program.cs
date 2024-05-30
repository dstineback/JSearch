var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Anti-Forgery services
builder.Services.AddAntiforgery(options =>
{
    // Configure Anti-Forgery options
    options.HeaderName = "X-CSRF-TOKEN"; // Optional: Change the header name
    options.FormFieldName = "__RequestVerificationToken"; // Optional: Change the form field name
    options.SuppressXFrameOptionsHeader = false; // Optional: Whether to suppress the X-Frame-Options header
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages();

app.Run();
