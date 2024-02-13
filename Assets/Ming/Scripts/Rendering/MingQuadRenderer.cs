using Ming.Engine;
using Ming.Rendering.Meshes;
using System;
using UnityEngine;

namespace Ming.Rendering
{
    public class MingQuadRenderer : MingBehaviour, IMingUpdate
    {
        public Vector3 Offset = Vector3.zero;
        public MingUpdater MingUpdater;

        [NonSerialized] public int QuadsPerBatchMesh = 256;
        [NonSerialized] public int SpritesRendered;
        [NonSerialized] public int MeshesRendered;

        private const int InitialRendererCapacity = 128;
        private MingBatchRenderer[] _batches;
        private ulong[] _keys;
        private int _rendererCount;

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Color32 color, Sprite sprite, Material material, int layer)
        {
            var batch = GetBatchRenderer(sprite, material, layer);
            batch.AddQuad(center, size, rotationDegrees, zSkew, color, sprite);
        }

        public MingBatchRenderer GetBatchRenderer(Sprite sprite, Material material, int layer)
        {
            ulong key = ((ulong)sprite.texture.GetInstanceID() << 29) + ((ulong)material.GetInstanceID() << 6) + (ulong)layer;
            int idx = -1;
            for (int i = 0; i < _rendererCount; ++i)
            {
                if (_keys[i] == key)
                {
                    idx = i;
                    break;
                }
            }

            if (idx < 0)
            {
                idx = CreateBatchRenderer(key, sprite.texture, material, layer);
                //Debug.LogFormat("HordeBatchRenderer created, idx = {0}, key = {1}", idx, key);
                //Debug.LogWarning("FIX ME. HordeBatchMesh have to call RecalculateBounds or everything is culled."); // Maybe manually render to camera on PreRender.
            }

            return _batches[idx];
        }

        private int CreateBatchRenderer(ulong key, Texture sprite, Material material, int layer)
        {
            if (_rendererCount >= _batches.Length)
            {
                Array.Resize(ref _batches, _batches.Length + _batches.Length / 2);
                Array.Resize(ref _keys, _batches.Length);
            }

            var batch = new MingBatchRenderer(key, sprite, material, layer, QuadsPerBatchMesh);
            _batches[_rendererCount] = batch;
            _keys[_rendererCount++] = key;
            return _rendererCount - 1;
        }

        void OnEnable()
        {
            _batches = new MingBatchRenderer[InitialRendererCapacity];
            _keys = new ulong[InitialRendererCapacity];
            _rendererCount = 0;

            MingUpdater.RegisterForUpdate(this, MingUpdatePass.MingDrawMeshes);
        }

        void OnDisable()
        {
            MingUpdater.UnregisterForUpdate(this, MingUpdatePass.MingDrawMeshes);
        }

        public void MingUpdate(MingUpdatePass pass)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.SetTRS(Offset, Quaternion.identity, Vector3.one);

            SpritesRendered = 0;
            MeshesRendered = 0;
            for (int i = 0; i < _rendererCount; ++i)
            {
                var batch = _batches[i];
                batch.ApplyChanges();
                int activeMeshes = batch.GetActiveMeshCount();
                for (int j = 0; j < activeMeshes; ++j)
                {
                    // TODO: DrawMesh does not always show in the editor. Seems to lag exactly one update behind.
                    Graphics.DrawMesh(batch.Meshes[j].Mesh, matrix, batch.Material, batch.Layer);
                    SpritesRendered += batch.Meshes[j].ActiveQuadCount;
                    MeshesRendered++;
                }
                batch.Clear();
            }
        }
    }
}
