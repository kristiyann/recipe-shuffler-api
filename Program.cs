using Microsoft.EntityFrameworkCore;
using recipe_shuffler.Data;
using recipe_shuffler.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using recipe_shuffler.Services.Tags;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using recipe_shuffler.Extensions;


// OData
//static IEdmModel GetEdmModel()
//{
//    ODataConventionModelBuilder builder = new();
//    builder.EntitySet<RecipeList>(nameof(RecipeList));
//    builder.EntitySet<TagList>(nameof(TagList));

//    builder.EnableLowerCamelCase();
//    var model = builder.GetEdmModel();
//    builder.ValidateModel(model);
//    return model;
//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DBContext
builder.Services.AddDbContext<DataContext>(options => {
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

builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("api", new ODataConventionModelBuilder().UseEdmModel())
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
builder.Services.AddScoped<ICollectionsService, CollectionsService>();
builder.Services.AddHttpContextAccessor(); // HttpContext

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

// Jwt
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
