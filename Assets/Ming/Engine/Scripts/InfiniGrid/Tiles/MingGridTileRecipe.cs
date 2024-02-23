using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new tileRecipe.asset", menuName = "Ming/InfiniGrid/Tile recipe")]
    public class MingGridTileRecipe : ScriptableObject
    {
        public int TiledId;
        public Sprite Floor;
        public Sprite Wall;
        public Sprite HalfRoof;
        public Sprite FullRoof;
    }
}
