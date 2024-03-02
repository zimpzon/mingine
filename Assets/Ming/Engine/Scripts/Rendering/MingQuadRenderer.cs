using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ming
{
    public class MingQuadRenderer : MingBehaviour, IMingObject
    {
        public string SortingLayerName = "Default";
        public int SortingOrder = 1;

        [MingReadOnly] public int QuadsPerBatchMesh = 1024;
        [MingReadOnly] public int SpritesRendered;
        [MingReadOnly] public int MeshesRendered;

        private const int InitialRendererCapacity = 32;
        private MingBatchRenderer[] _batches;
        private ulong[] _keys;
        private int _rendererCount;

        private List<GameObject> _unityMeshRenderers = new List<GameObject>();

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Color32 color, Sprite sprite, Material material, int layer)
        {
            var batch = GetBatchRenderer(sprite, material, layer);
            batch.AddQuad(center, size, rotationDegrees, zSkew, color, sprite);
        }

        public void AddQuad(Vector3 center, Vector2 size, Color32 colorTl, Color32 colorTr, Color32 colorBr, Color32 colorBl, Sprite sprite, Material material, int layer)
        {
            var batch = GetBatchRenderer(sprite, material, layer);
            batch.AddQuad(center, size, colorTl, colorTr, colorBr, colorBl, sprite);
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

            MingMain.Instance.MingUpdater.RegisterForUpdate(this, MingUpdatePass.MingDrawMeshes);
        }

        void OnDisable()
        {
            MingMain.Instance.MingUpdater.UnregisterForUpdate(this, MingUpdatePass.MingDrawMeshes);
        }

        GameObject CreateUnityMeshRenderer()
        {
            var go = new GameObject();
            go.layer = gameObject.layer;
            go.name = $"{nameof(MingQuadRenderer)} mesh";

            var meshRenderer = go.AddComponent<MeshRenderer>();
            meshRenderer.sortingLayerName = SortingLayerName;
            meshRenderer.sortingOrder = SortingOrder;

            go.AddComponent<MeshFilter>();
            go.transform.SetParent(this.transform);
            return go;
        }

        public void MingUpdate(MingUpdatePass pass)
        {
            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.SetTRS(Vector3.zero, Quaternion.identity, Vector3.one);

            int unityRendererIdx = 0;

            SpritesRendered = 0;
            MeshesRendered = 0;
            for (int i = 0; i < _rendererCount; ++i)
            {
                var batch = _batches[i];
                batch.ApplyChanges();
                int activeMeshes = batch.GetActiveMeshCount();
                for (int j = 0; j < activeMeshes; ++j)
                {
                    if (unityRendererIdx == _unityMeshRenderers.Count)
                    {
                        _unityMeshRenderers.Add(CreateUnityMeshRenderer());
                    }

                    var unityMeshRenderer = _unityMeshRenderers[unityRendererIdx++];
                    unityMeshRenderer.SetActive(true);
                    unityMeshRenderer.GetComponent<MeshFilter>().mesh = batch.Meshes[j].Mesh;

                    var unityMesh = unityMeshRenderer.GetComponent<MeshRenderer>();
                    unityMesh.material = batch.Material;

                    //Graphics.DrawMesh(batch.Meshes[j].Mesh, matrix, batch.Material, batch.Layer);
                    SpritesRendered += batch.Meshes[j].ActiveQuadCount;
                    MeshesRendered++;
                }
                batch.Clear();
            }

            // disable Unity mesh renderers not used in this frame
            for (int i = unityRendererIdx; i < _unityMeshRenderers.Count; ++i)
            {
                _unityMeshRenderers[i].SetActive(false);
            }
        }
    }
}
