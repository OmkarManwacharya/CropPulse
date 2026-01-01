using CropDiseaseDetection.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. DATABASE CONNECTION
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. ADD MVC SERVICES
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 3. SEED DATABASE & ENSURE DIRECTORIES
// This block runs every time the app starts to ensure your Disease table isn't empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Create database if it doesn't exist and apply migrations
    context.Database.EnsureCreated();

    // Ensure the uploads folder exists for your images
    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    if (!Directory.Exists(uploadPath))
    {
        Directory.CreateDirectory(uploadPath);
    }

    // Add initial disease data if the table is empty
    if (!context.Diseases.Any())
    {
        context.Diseases.AddRange(
            new Disease
            {
                Crop = "Tomato",
                DiseaseName = "Leaf Curl Virus",
                Symptoms = "Yellowing and upward curling of leaves.",
                Treatment = "Use neem oil spray and remove whiteflies."
            },
            new Disease
            {
                Crop = "Potato",
                DiseaseName = "Early Blight",
                Symptoms = "Small, dark spots on older leaves.",
                Treatment = "Apply fungicide and ensure crop rotation."
            }
        );
        context.SaveChanges();
    }
}

// 4. CONFIGURE HTTP PIPELINE
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Essential for serving images from wwwroot/uploads

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();