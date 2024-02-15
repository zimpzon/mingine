using UnityEngine;

namespace Ming.Projectiles
{
    public struct MingProjectile
    {
        public delegate bool UpdateProjectileCallback(ref MingProjectile projectile);
        public delegate bool OnCollisionCallback(Collider2D other);

        public enum Expiry { MaxDistanceReached, MaxTimeReached }

        public void ApplyBlueprint(MingProjectileBlueprint blueprint)
        {
            Sprite = blueprint.Sprite;
            Material = blueprint.Material;
            Size = blueprint.Size;
            Color = blueprint.Color;

            HasDropshadow = blueprint.HasDropshadow;
            DropshadowSprite = blueprint.DropshadowSprite;
            DropshadowMaterial = blueprint.DropshadowMaterial;
            DropshadowSize = blueprint.DropshadowSize;
            DropshadowColor = blueprint.DropshadowColor;
            DropshadowOffset = blueprint.DropshadowOffset;

            CollisionSize = blueprint.CollisionSize;
            CollisionMask = blueprint.CollisionLayerMask;

            MaxDist = blueprint.MaxDistance;
            MaxTime = blueprint.MaxTime;
        }

        // Blueprint data
        public Sprite Sprite; // Change: Maybe (within sprite sheet?)
        public Material Material; // Change: No
        public Vector2 Size; // Change: Yes
        public Color32 Color; // Change: Yes

        public float MaxDist; // Change: Yes
        public float MaxTime; // Change: Yes

        public float CollisionSize; // Change: Yes
        public LayerMask CollisionMask; // Change: Yes

        public bool HasDropshadow; // Change: Yes
        public Sprite DropshadowSprite; // Change: Maybe (within sprite sheet?)
        public Material DropshadowMaterial; // Change: No
        public Vector2 DropshadowSize; // Change: Yes
        public Color DropshadowColor; // Change: Yes
        public Vector2 DropshadowOffset; // Change: Yes

        // Other data
        public float TimeSinceSpawn; // Change: Yes, but also updated by engine
        public float DistanceTravelled; // Change: Yes, but also updated by engine
        public Vector2 Position; // Change: Yes, but also updated by engine if AutoMove is true
        public Vector2 Velocity; // Change: Yes
        public bool AutoMove; // Change: Yes
        public float RotationDegrees; // Change: Yes

        // Custom data (Change: Yes)
        public Object CustomObject1;
        public Object CustomObject2;
        public Object CustomObject3;
        public float CustomFloat1;
        public float CustomFloat2;
        public float CustomFloat3;
        public int CustomInt1;
        public int CustomInt2;
        public int CustomInt3;
        public Vector3 CustomVec1;
        public Vector3 CustomVec2;
        public Vector3 CustomVec3;

        // Callbacks
        public UpdateProjectileCallback UpdateProjectile; // Change: Yes
        public OnCollisionCallback OnCollision; // Change: Yes
    }
}
