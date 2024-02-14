using Ming.Util;
using UnityEngine;

namespace Ming.Projectiles
{
    [CreateAssetMenu(fileName = "new MingProjectileBlueprint.asset", menuName = "Ming/Projectiles/Projectile Blueprint", order = 10)]
    public class MingProjectileBlueprint : ScriptableObject
    {
        public Sprite Sprite;
        public Material Material;
        public string SortingLayerName = "Default";
        public int SortingOrder = 0;
        public Vector2 Size = Vector2.one;
        public Color32 Color = UnityEngine.Color.white;
        public float CollisionSize = 1.0f;
        public LayerMask CollisionMask;
        public float MaxDistance = 20;
        public float MaxTime = 10;
        public bool HasDropshadow;
        public Sprite DropshadowSprite;
        public Material DropshadowMaterial;
        public string DropshadowSortingLayerName = "Default";
        public int DropshadowSortingOrder = 0;
        public Vector2 DropshadowSize = Vector2.one;
        public Color32 DropshadowColor = UnityEngine.Color.black;
        public Vector2 DropshadowOffset;
    }
}
