using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipeCollection.asset", menuName = "Ming/InfiniGrid/Tile recipe collection")]
    public class MingGridTileRecipeCollection : ScriptableObject
    {
        public MingGridTileRecipe[] RecipeCollection;

        private Dictionary<uint, MingGridTileRecipe> RecipeLut;

        public MingGridTileRecipe GetRecipe(uint tiledId)
        {
            if (!RecipeLut.TryGetValue(tiledId, out MingGridTileRecipe recipe))
            {
                Debug.LogError($"No recipe found for tile id {tiledId}");
                return null;
            }

            return recipe;
        }

        public void Init()
        {
            int countDistinct = RecipeCollection.Select(r => r.TiledId).Distinct().Count();
            if (countDistinct != RecipeCollection.Length)
            {
                Debug.LogError("Duplicate tile ids found in recipe collection");
            }

            RecipeLut = RecipeCollection.ToDictionary(r => r.TiledId);
        }
    }
}
