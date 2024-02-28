using System;
using System.Linq;
using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipeCollection.asset", menuName = "Ming/InfiniGrid/Tile recipe collection")]
    public class MingGridTileRecipeCollection : ScriptableObject
    {
        public MingGridTileRecipe[] RecipeCollection;

        private MingGridTileRecipe[] _idxLookup;

        public MingGridTileRecipe GetRecipe(uint tiledId)
        {
            if (_idxLookup == null)
            {
                throw new ArgumentException($"{nameof(Init)} has not been called");
            }

            MingGridTileRecipe recipe = _idxLookup[tiledId];
            if (recipe == null)
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

            _idxLookup = new MingGridTileRecipe[RecipeCollection.Max(r => r.TiledId) + 1];
            for (int i = 0; i < RecipeCollection.Length; ++i)
            {
                MingGridTileRecipe recipe = RecipeCollection[i];
                _idxLookup[recipe.TiledId] = recipe;
            }
        }
    }
}
