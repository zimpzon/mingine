using Ming.Engine;
using Ming.Rendering;
using Ming.Util;
using System;
using UnityEngine;

namespace Ming.Projectiles
{
    [RequireComponent(typeof(MingQuadRenderer))]
    public class MingProjectileManager : MingBehaviour, IMingUpdate
    {
        public int InitialCapacity = 5000;
        public float OffsetY;
        public MingUpdater MingUpdater;

        [SerializeField, MingLayer] public LayerMask ProjectileLayer;
        [SerializeField, MingLayer] public LayerMask LightLayer;

        private MingQuadRenderer _mingQuadRenderer;

        // To allow updating structs in-place we have to use array. A List<> will return a copy when accessing an element.
        private MingProjectile[] _projectiles;
        private int _projectileLayer;
        private int _lightLayer;

        [Header("Debug")]
        public int ActiveProjectiles;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < ActiveProjectiles; ++i)
            {
                var p = _projectiles[i];
                var pos = p.ActualPos;
                pos.y += OffsetY;
                Gizmos.DrawWireSphere(pos, p.CollisionSize);
            }
        }

        void Awake()
        {
            _mingQuadRenderer = GetComponent<MingQuadRenderer>();
            _projectiles = new MingProjectile[InitialCapacity];
            _projectileLayer = (int)ProjectileLayer;
            _lightLayer = (int)LightLayer;
        }

        public void Clear()
        {
            ActiveProjectiles = 0;
        }

        public void SpawnProjectile(ref MingProjectile p)
        {
            _projectiles[ActiveProjectiles++] = p;

            if (ActiveProjectiles == _projectiles.Length)
            {
                Array.Resize(ref _projectiles, _projectiles.Length + (_projectiles.Length / 2));
                //Debug.Log("Projectile array was expanded. Consider increasing initial capacity. New size: " + _projectiles.Length);
            }
        }

        void RenderProjectile(ref MingProjectile p)
        {
            Vector3 pos = p.ActualPos;
            pos.z = p.Z;
            pos.y += OffsetY;
            _mingQuadRenderer.AddQuad(pos, p.Size, p.RotationDegrees, p.Size.y, p.Color, p.Sprite, p.Material, _projectileLayer);

            if (p.HasDropshadow)
            {
                pos += (Vector3)p.DropshadowOffset;
                _mingQuadRenderer.AddQuad(pos, p.DropshadowSize, p.RotationDegrees, p.DropshadowSize.y, p.DropshadowColor, p.DropshadowSprite, p.DropshadowMaterial, _lightLayer);
            }
        }

        void OnEnable() { MingUpdater.RegisterForUpdate(this, MingUpdatePass.MingDrawMeshes); }
        void OnDisable() { MingUpdater.UnregisterForUpdate(this, MingUpdatePass.MingDrawMeshes); }

        public void MingUpdate(MingUpdatePass pass)
        {
            int i = 0;
            while (i < ActiveProjectiles)
            {
                bool success = _projectiles[i].UpdateCallback(ref _projectiles[i]);
                if (!success)
                {
                    // Overwrite current with bottom of list and rerun current (i not incremented)
                    _projectiles[i] = _projectiles[ActiveProjectiles - 1];
                    ActiveProjectiles--;
                    continue;
                }

                RenderProjectile(ref _projectiles[i]);

                i++;
            }
        }
    }
}
