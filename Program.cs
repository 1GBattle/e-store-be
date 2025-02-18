using e_store_be.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();

//http pipeline
app.UseCors(opt =>
{
    opt.AllowAnyMethod().AllowAnyHeader().WithOrigins("https://localhost:3000");
});

app.MapControllers();
DbInitializer.InitDb(app);
app.Run();
