using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // Sesuaikan dengan port frontend Anda
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔹 Ambil connection string dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 Tambahkan DbContext untuk MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();



var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors("AllowLocalhost");  // Gunakan policy CORS yang telah ditentukan

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Endpoint untuk mengambil data users
app.MapGet("/users", async (ApplicationDbContext db) =>
{
    return await db.Users.ToListAsync();
});

app.MapGet("/projects", async (ApplicationDbContext db) =>
{
    return await db.Projects.ToListAsync();
});

// 🔹 Endpoint untuk menambahkan user baru
app.MapPost("/users", async (ApplicationDbContext db, User user) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

app.MapPost("/projects", async (ApplicationDbContext db, Project project) =>
{
    db.Projects.Add(project);
    await db.SaveChangesAsync();
    return Results.Created($"/projects/{project.Id}", project);
});
app.MapDelete("/projects/{id}", async (ApplicationDbContext db, int id) =>
{
    var project = await db.Projects.FindAsync(id);
    if (project == null)
    {
        return Results.NotFound($"Project dengan ID {id} tidak ditemukan.");
    }

    db.Projects.Remove(project);
    await db.SaveChangesAsync();

    return Results.Ok($"Project dengan ID {id} berhasil dihapus.");
});

app.MapPut("/projects/{id}", async (ApplicationDbContext db, int id, Project updatedProject) =>
{
    var project = await db.Projects.FindAsync(id);
    if (project == null)
    {
        return Results.NotFound($"Project dengan ID {id} tidak ditemukan.");
    }

    // Update properti project dengan data baru
    project.Name = updatedProject.Name;
    project.Location = updatedProject.Location;
    project.Stage = updatedProject.Stage;
    project.Category = updatedProject.Category;
    project.StartDate = updatedProject.StartDate;
    project.Details = updatedProject.Details;
    project.CreatorId = updatedProject.CreatorId;

    await db.SaveChangesAsync();

    return Results.Ok(project);
});

app.MapGet("/projects/{id}", async (ApplicationDbContext db, int id) =>
{
    var project = await db.Projects.FindAsync(id);
    
    if (project == null)
    {
        return Results.NotFound(new { message = "Project not found" });
    }

    return Results.Ok(project);
});


app.Run();

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
}
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

// Model Project
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Location {get; set;}
    public string? Stage { get; set; }
    public string? Category { get; set; }
    public DateTime StartDate { get; set; }
    public string? Details {get; set;}
    public string?  CreatorId {get; set;}

}
