using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipe.asset", menuName = "Ming/InfiniGrid/Tile recipe")]
    public class MingGridTileRecipe : ScriptableObject
    {
        public string Name = "(not set)";
        public uint TiledId;
        public int TileLevel;
        public bool Walkable;
        public Sprite TileSprite;
        public MingTileRenderLayer RenderLayer;
        public Sprite[] RuleSprites;
        public MingTileRenderLayer[] RuleSpritesRenderLayers;
    }
}
