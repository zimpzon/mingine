using UnityEngine;

namespace Ming
{
    public class Mapping
    {
        public int Id;
        public Sprite TileSprite;
    }

    public class MingGridTileSprites : ScriptableObject
    {
        public Mapping[] TileSprites;
    }
}
