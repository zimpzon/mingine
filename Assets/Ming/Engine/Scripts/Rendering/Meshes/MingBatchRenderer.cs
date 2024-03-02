using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Ming
{
    /// <summary>
    /// Renders quads sharing same material and texture. These can be rendered in one draw call.
    /// </summary>
    public class MingBatchRenderer
    {
        [NonSerialized] public Texture Texture;
        [NonSerialized] public Material Material;
        [NonSerialized] public ulong Id;
        [NonSerialized] public int Layer;
        [NonSerialized] public int QuadsPerMesh;
        [NonSerialized] public List<MingBatchMesh> Meshes = new List<MingBatchMesh>();
        [NonSerialized] public int QuadCount;

        int _totalQuadCapacity;
        float _textureXToUV;
        float _textureYToUV;

        public MingBatchRenderer(ulong id, Texture texture, Material material, int layer, int quadsPerMesh)
        {
            Layer = layer;
            QuadsPerMesh = quadsPerMesh;

            Texture = texture;
            _textureXToUV = 1.0f / Texture.width;
            _textureYToUV = 1.0f / Texture.height;

            Material = new Material(material)
            {
                hideFlags = HideFlags.HideAndDontSave,
                mainTexture = texture
            };
        }

        public int GetActiveMeshCount()
        {
            return (QuadCount + QuadsPerMesh - 1) / QuadsPerMesh;
        }

        public void Clear()
        {
            QuadCount = 0;
            for (int i = 0; i < Meshes.Count; ++i)
                Meshes[i].Clear();
        }

        public void ApplyChanges()
        {
            for (int i = 0; i < Meshes.Count; ++i)
                Meshes[i].ApplyChanges();
        }

        public void AddQuad(Vector3 center, Vector2 size, Color32 colorTl, Color32 colorTr, Color32 colorBr, Color32 colorBl, Sprite sprite)
        {
            QuadCount++;
            if (QuadCount > _totalQuadCapacity)
            {
                var newMesh = new MingBatchMesh(QuadsPerMesh);
                Meshes.Add(newMesh);
                _totalQuadCapacity += QuadsPerMesh;
            }

            int meshIdx = (QuadCount - 1) / QuadsPerMesh;
            var currentMesh = Meshes[meshIdx];

            var textureRect = sprite.rect;
            Vector2 uvTopLeft = new Vector2(textureRect.x * _textureXToUV, (textureRect.y + textureRect.height) * _textureYToUV);
            Vector2 uvSize = new Vector2(textureRect.width * _textureXToUV, textureRect.height * _textureYToUV);
            currentMesh.AddQuad(center, size, uvTopLeft, uvSize, colorTl, colorTr, colorBr, colorBl);
        }

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Color32 color, Sprite sprite)
        {
            QuadCount++;
            if (QuadCount > _totalQuadCapacity)
            {
                var newMesh = new MingBatchMesh(QuadsPerMesh);
                Meshes.Add(newMesh);
                _totalQuadCapacity += QuadsPerMesh;
            }

            int meshIdx = (QuadCount - 1) / QuadsPerMesh;
            var currentMesh = Meshes[meshIdx];

            var textureRect = sprite.rect;
            Vector2 uvTopLeft = new Vector2(textureRect.x * _textureXToUV, (textureRect.y + textureRect.height) * _textureYToUV);
            Vector2 uvSize = new Vector2(textureRect.width * _textureXToUV, textureRect.height * _textureYToUV);
            currentMesh.AddQuad(center, size, rotationDegrees, zSkew, uvTopLeft, uvSize, color);
        }
    }
}
