using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new MingGridTileSprites.asset", menuName = "Ming/InfiniGrid/TileSprites")]
    public class MingGridTileSprites : ScriptableObject
    {
        public Sprite[] TileSprites;
    }
}
