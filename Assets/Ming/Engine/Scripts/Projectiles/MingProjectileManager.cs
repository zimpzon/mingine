using System;
using UnityEngine;

namespace Ming
{
    [RequireComponent(typeof(MingQuadRenderer))]
    public class MingProjectileManager : MingBehaviour, IMingObject
    {
        [SerializeField] private int InitialCapacity = 1000;
        [SerializeField] private string SpriteSortingLayerName = "Default";
        [SerializeField] private int SpriteSortingOrder = 1;
        [SerializeField] public LayerMask ProjectileLayer;

        private MingQuadRenderer _mingQuadRenderer;

        // To allow updating structs in-place we have to use array. A List<> will return a copy when accessing an element.
        private MingProjectile[] _projectiles;
        private int _projectileLayer;

        [Header("Debug")]
        public int ActiveProjectiles;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < ActiveProjectiles; ++i)
            {
                var p = _projectiles[i];
                var pos = p.Position;
                Gizmos.DrawWireSphere(pos, p.CollisionSize);
            }
        }

        void Awake()
        {
            _mingQuadRenderer = GetComponent<MingQuadRenderer>();
            _projectiles = new MingProjectile[InitialCapacity];
            _projectileLayer = (int)ProjectileLayer;
        }

        public void Clear()
        {
            ActiveProjectiles = 0;
        }

        public void SpawnProjectile(ref MingProjectile p)
        {
            _projectiles[ActiveProjectiles++] = p;

            bool expandLength = ActiveProjectiles == _projectiles.Length;
            if (expandLength)
            {
                Array.Resize(ref _projectiles, _projectiles.Length + (_projectiles.Length / 2));
            }
        }

        void RenderProjectile(ref MingProjectile p)
        {
            Vector3 pos = p.Position;
            _mingQuadRenderer.AddQuad(pos, p.Size, p.RotationDegrees, p.Size.y, p.Color, p.Sprite, p.Material, _projectileLayer);
        }

        void OnEnable() { MingMain.Instance.MingUpdater.RegisterForUpdate(this, MingUpdatePass.MingDrawMeshes); }
        void OnDisable() { MingMain.Instance.MingUpdater.UnregisterForUpdate(this, MingUpdatePass.MingDrawMeshes); }

        public void MingUpdate(MingUpdatePass pass)
        {
            int i = 0;
            while (i < ActiveProjectiles)
            {
                bool success = _projectiles[i].UpdateProjectile(ref _projectiles[i]);
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
