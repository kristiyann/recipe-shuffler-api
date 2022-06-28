using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using recipe_shuffler.DTO.Recipes;
using recipe_shuffler.DTO.Tags;
using recipe_shuffler.DataTransferObjects;

namespace recipe_shuffler.Extensions
{
    public static class EdmModelExtensions
    {
        public static IEdmModel UseEdmModel(this ODataConventionModelBuilder odataBuilder)
        {
            odataBuilder.EnableLowerCamelCase();

            odataBuilder.EntitySet<RecipeList>(nameof(RecipeList));
            odataBuilder.EntitySet<TagList>(nameof(TagList));
            odataBuilder.EntitySet<CollectionList>(nameof(CollectionList));

            IEdmModel model = odataBuilder.GetEdmModel();
            odataBuilder.ValidateModel(model);
            return model;
        }

    }
}
