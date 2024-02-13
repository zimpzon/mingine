using Ming.Engine;
using UnityEngine;

namespace Ming.Projectiles
{
    public struct MingProjectile
    {
        public void Reset()
        {
            Idx = 0;
            StartTime = MingTime.Time;
            StartPos = Vector2.zero;
            Origin = Vector2.zero;
            OriginOffset = Vector2.zero;
            MaxDist = float.MaxValue;
            MaxTime = float.MaxValue;

            Sprite = null;
            Material = null;
            Size = Vector2.one;
            Color = new Color32(255, 255, 255, 255);
            HasDropshadow = false;
            DropshadowSprite = null;
            DropshadowMaterial = null;
            DropshadowSize = Vector2.one;
            DropshadowColor = new Color32(0, 0, 0, 255);
            DropshadowOffset = Vector2.zero;

            CustomData = null;
            CollisionSize = 1.0f;
            BounceWalls = false;
            BouncesLeft = 0;

            Velocity = Vector2.zero;
            ActualPos = Vector2.zero;
            RotationDegrees = 0;
            Z = 0;

            UpdateCallback = null;
        }

        public void ApplyBlueprint(MingProjectileBlueprint desc)
        {
            Reset();

            Sprite = desc.Sprite;
            Material = desc.Material;
            Size = desc.Size;
            Color = desc.Color;

            HasDropshadow = desc.EmitDropshadow;
            DropshadowSprite = desc.DropshadowSprite;
            DropshadowMaterial = desc.DropshadowMaterial;
            DropshadowSize = desc.DropshadowSize;
            DropshadowColor = desc.DropshadowColor;
            DropshadowOffset = desc.DropshadowOffset;

            CollisionSize = desc.CollisionSize;
            BounceWalls = desc.BounceWalls;
            BouncesLeft = desc.MaxWallBounces;

            MaxDist = desc.MaxDistance;
            MaxTime = desc.MaxTime;
        }

        public int Idx;
        public float StartTime;
        public Vector2 StartPos;
        public Vector2 Origin;
        public Vector2 OriginOffset;
        public float MaxDist;
        public float MaxTime;

        public Sprite Sprite;
        public Material Material;
        public Vector2 Size;
        public Color32 Color;
        public bool HasDropshadow;
        public Sprite DropshadowSprite;
        public Material DropshadowMaterial;
        public Vector2 DropshadowSize;
        public Color DropshadowColor;
        public Vector2 DropshadowOffset;
        public Object CustomData;

        public float CollisionSize;
        public bool BounceWalls;
        public int BouncesLeft;

        public float Speed;
        public Vector2 Velocity;
        public Vector2 ActualPos;
        public float RotationDegrees;
        public float Z;
        
        public delegate bool TickDelegate(ref MingProjectile projectile);
        public TickDelegate UpdateCallback;
    }
}
