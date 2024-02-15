using Ming.Util;
using UnityEngine;

namespace Ming.Projectiles
{
    [CreateAssetMenu(fileName = "new MingProjectileBlueprint.asset", menuName = "Ming/Projectiles/Projectile Blueprint", order = 10)]
    public class MingProjectileBlueprint : ScriptableObject
    {
        public Sprite Sprite;
        public Material Material;
        public Vector2 Size = Vector2.one;
        public Color32 Color = UnityEngine.Color.white;

        public float CollisionSize = 1.0f;
        public Vector2 CollisionOffset;
        public LayerMask CollisionLayerMask;

        public float MaxDistance = 20;
        public float MaxTime = 10;

        public bool HasDropshadow;
        public Sprite DropshadowSprite;
        public Material DropshadowMaterial;
        public Vector2 DropshadowSize = Vector2.one;
        public Color32 DropshadowColor = UnityEngine.Color.black;
        public Vector2 DropshadowOffset;
    }
}
