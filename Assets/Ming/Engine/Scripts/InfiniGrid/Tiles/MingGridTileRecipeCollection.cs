using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipeCollection.asset", menuName = "Ming/InfiniGrid/Tile recipe collection")]
    public class MingGridTileRecipeCollection : ScriptableObject
    {
        public MingGridTileRecipe[] RecipeCollection;

        private Dictionary<int, MingGridTileRecipe> RecipeLut;

        public MingGridTileRecipe GetRecipe(int tiledId)
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
            RecipeLut = RecipeCollection.ToDictionary(r => r.TiledId);
        }
    }
}
