using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using recipe_shuffler.Data;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.Services;
using recipe_shuffler.Services.Tags;
using Swashbuckle.AspNetCore.Filters;
using System.Text;


// OData
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<RecipeList>(nameof(RecipeList)); 
    builder.EntitySet<TagList>(nameof(TagList));

    builder.EnableLowerCamelCase();
    IEdmModel? model = builder.GetEdmModel();
    builder.ValidateModel(model);
    return model;
}

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DBContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);
// Jwt
string secretToken = builder.Configuration["AppSettings:Token"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken)),
        ValidateIssuer = false,
        ValidateAudience = false
    });

builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("api", GetEdmModel())
.Filter()
.OrderBy()
.Count()
.Expand()
.Select()
.SetMaxTop(null));

// Service Layer
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITagsService, TagsService>();
builder.Services.AddHttpContextAccessor(); // HttpContext

// CORS
string? MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

WebApplication? app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Jwt
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
