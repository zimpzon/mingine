﻿using Ming.Engine;
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
            SortingLayerName = "Default";
            SortingOrder = 0;
            Size = Vector2.one;
            Color = new Color32(255, 255, 255, 255);
            CustomData = null;
            CollisionSize = 1.0f;
            Velocity = Vector2.zero;
            Position = Vector2.zero;
            RotationDegrees = 0;
            Z = 0;
            HasDropshadow = false;
            DropshadowSprite = null;
            DropshadowMaterial = null;
            DropshadowSize = Vector2.one;
            DropshadowColor = new Color32(0, 0, 0, 255);
            DropshadowOffset = Vector2.zero;

            UpdateCallback = null;
        }

        public void ApplyBlueprint(MingProjectileBlueprint blueprint)
        {
            Reset();

            Sprite = blueprint.Sprite;
            Material = blueprint.Material;
            SortingLayerName = blueprint.SortingLayerName;
            SortingOrder = blueprint.SortingOrder;
            Size = blueprint.Size;
            Color = blueprint.Color;

            HasDropshadow = blueprint.HasDropshadow;
            DropshadowSprite = blueprint.DropshadowSprite;
            DropshadowMaterial = blueprint.DropshadowMaterial;
            DropshadowSortingLayerName = blueprint.DropshadowSortingLayerName;
            DropshadowSortingOrder = blueprint.DropshadowSortingOrder;
            DropshadowSize = blueprint.DropshadowSize;
            DropshadowColor = blueprint.DropshadowColor;
            DropshadowOffset = blueprint.DropshadowOffset;

            CollisionSize = blueprint.CollisionSize;
            CollisionMask = blueprint.CollisionMask;

            MaxDist = blueprint.MaxDistance;
            MaxTime = blueprint.MaxTime;
        }

        public int Idx;
        public Sprite Sprite;
        public Material Material;
        public string SortingLayerName;
        public int SortingOrder;
        public float StartTime;
        public Vector2 StartPos;
        public Vector2 Origin;
        public Vector2 OriginOffset;
        public float MaxDist;
        public float MaxTime;
        public Vector2 Size;
        public Color32 Color;
        public Object CustomData;
        public float CollisionSize;
        public LayerMask CollisionMask;
        public float Speed;
        public Vector2 Velocity;
        public Vector2 Position;
        public float RotationDegrees;
        public float Z;
        public bool HasDropshadow;
        public Sprite DropshadowSprite;
        public Material DropshadowMaterial;
        public string DropshadowSortingLayerName;
        public int DropshadowSortingOrder;
        public Vector2 DropshadowSize;
        public Color DropshadowColor;
        public Vector2 DropshadowOffset;

        public delegate bool TickDelegate(ref MingProjectile projectile);
        public TickDelegate UpdateCallback;
    }
}
