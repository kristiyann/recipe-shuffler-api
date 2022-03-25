using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using recipe_shuffler.Data;
using recipe_shuffler.Models;
using recipe_shuffler.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using recipe_shuffler.Services.Tags;


// OData
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Recipe>("Recipes");
    builder.EntitySet<User>("Users");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DBContext
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("api/", GetEdmModel()).Filter().Select().Expand().OrderBy().Count().SetMaxTop(null));

// Service Layer
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITagsService, TagsService>();

// CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
