using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new MingProjectileBlueprint.asset", menuName = "Ming/Projectiles/Projectile Blueprint")]
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
    }
}
