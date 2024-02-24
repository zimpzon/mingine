using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipe.asset", menuName = "Ming/InfiniGrid/Tile recipe")]
    public class MingGridTileRecipe : ScriptableObject
    {
        public uint TiledId;
        public MingTileRenderLayer RenderLayer;
        public Sprite TileSprite;
    }
}
