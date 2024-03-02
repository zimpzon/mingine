using System;
using UnityEngine;

namespace Ming
{
    /// <summary>
    /// Fixed size mesh. Triangles that are not in use will be rendered as degenerate (empty) by setting all vertices to 0.
    /// </summary>
    public class MingBatchMesh
    {
        [NonSerialized] public Mesh Mesh;
        [NonSerialized] public int Capacity;
        [NonSerialized] public int ActiveQuadCount;

        Vector3[] _vertices;
        Vector2[] _uv;
        int[] _indices;
        Color32[] _colors;

        int _idxAlreadyZeroed;

        const int VerticesPerQuad = 4;
        const int IndicesPerQuad = 6;

        public MingBatchMesh(int capacity)
        {
            Capacity = capacity;

            Mesh = new Mesh();
            Mesh.MarkDynamic();
            Mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000);

            _vertices = new Vector3[capacity * VerticesPerQuad];
            _uv = new Vector2[capacity * VerticesPerQuad];
            _colors = new Color32[capacity * VerticesPerQuad];
            _indices = new int[capacity * IndicesPerQuad];
            InitializeIndices();

            _idxAlreadyZeroed = 0;

            ApplyChanges();

            // Triangles are set only once
            Mesh.SetTriangles(_indices, 0, false);

            Mesh.UploadMeshData(markNoLongerReadable: false);
        }

        void InitializeIndices()
        {
            int vert0 = 0;
            for (int i = 0; i < _indices.Length; i += IndicesPerQuad)
            {
                _indices[i + 0] = vert0 + 0;
                _indices[i + 1] = vert0 + 1;
                _indices[i + 2] = vert0 + 3;

                _indices[i + 3] = vert0 + 1;
                _indices[i + 4] = vert0 + 2;
                _indices[i + 5] = vert0 + 3;

                vert0 += 4;
            }
        }

        public void Clear()
        {
            ActiveQuadCount = 0;
        }

        public void ApplyChanges()
        {
            ZeroVertices(ActiveQuadCount, _idxAlreadyZeroed - 1);
            _idxAlreadyZeroed = ActiveQuadCount;
            Mesh.RecalculateBounds(); // TODO: What be nice to get rid of this
            Mesh.vertices = _vertices;
            Mesh.uv = _uv;
            Mesh.colors32 = _colors;

            Mesh.UploadMeshData(markNoLongerReadable: false);
        }

        void ZeroVertices(int firstQuad, int lastQuad)
        {
            for (int i = firstQuad; i <= lastQuad; ++i)
            {
                int vert0 = i * 4;
                _vertices[vert0 + 0].x = 0.0f;
                _vertices[vert0 + 0].y = 0.0f;
                _vertices[vert0 + 0].z = 0.0f;

                _vertices[vert0 + 1].x = 0.0f;
                _vertices[vert0 + 1].y = 0.0f;
                _vertices[vert0 + 1].z = 0.0f;

                _vertices[vert0 + 2].x = 0.0f;
                _vertices[vert0 + 2].y = 0.0f;
                _vertices[vert0 + 2].z = 0.0f;

                _vertices[vert0 + 3].x = 0.0f;
                _vertices[vert0 + 3].y = 0.0f;
                _vertices[vert0 + 3].z = 0.0f;
            }
        }

        public void AddQuad(Vector3 center, Vector2 size, Vector2 uvTopLeft, Vector2 uvSize, Color32 colorTl, Color32 colorTr, Color32 colorBr, Color32 colorBl)
        {
            // 0---1
            // | / | = [0, 1, 3] and [1, 2, 3]
            // 3---2

            float halfW = size.x * 0.5f;
            float halfH = size.y * 0.5f;

            int vert0 = ActiveQuadCount * 4;
            _vertices[vert0 + 0].x = -halfW + center.x;
            _vertices[vert0 + 1].x = halfW + center.x;
            _vertices[vert0 + 2].x = halfW + center.x;
            _vertices[vert0 + 3].x = -halfW + center.x;

            _vertices[vert0 + 0].y = halfH + center.y;
            _vertices[vert0 + 1].y = halfH + center.y;
            _vertices[vert0 + 2].y = -halfH + center.y;
            _vertices[vert0 + 3].y = -halfH + center.y;

            _vertices[vert0 + 0].z = center.z;
            _vertices[vert0 + 1].z = center.z;
            _vertices[vert0 + 2].z = center.z;
            _vertices[vert0 + 3].z = center.z;

            _uv[vert0 + 0].x = uvTopLeft.x;
            _uv[vert0 + 0].y = uvTopLeft.y;

            _uv[vert0 + 1].x = uvTopLeft.x + uvSize.x;
            _uv[vert0 + 1].y = uvTopLeft.y;

            _uv[vert0 + 2].x = uvTopLeft.x + uvSize.x;
            _uv[vert0 + 2].y = uvTopLeft.y - uvSize.y;

            _uv[vert0 + 3].x = uvTopLeft.x;
            _uv[vert0 + 3].y = uvTopLeft.y - uvSize.y;

            _colors[vert0 + 0] = colorTl;
            _colors[vert0 + 1] = colorTr;
            _colors[vert0 + 2] = colorBr;
            _colors[vert0 + 3] = colorBl;

            ActiveQuadCount++;
        }

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Color32 color)
        {
            AddQuad(center, size, rotationDegrees, zSkew, Vector2.up, Vector2.one, color);
        }

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Vector2 uvTopLeft, Vector2 uvSize, Color32 color)
        {
            // 0---1
            // | / | = [0, 1, 3] and [1, 2, 3]
            // 3---2

            float halfW = size.x * 0.5f;
            float halfH = size.y * 0.5f;

            float sin = Mathf.Sin(-rotationDegrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(-rotationDegrees * Mathf.Deg2Rad);

            int vert0 = ActiveQuadCount * 4;
            _vertices[vert0 + 0].x = (-halfW * cos -  halfH * sin) + center.x;
            _vertices[vert0 + 1].x = ( halfW * cos -  halfH * sin) + center.x;
            _vertices[vert0 + 2].x = ( halfW * cos - -halfH * sin) + center.x;
            _vertices[vert0 + 3].x = (-halfW * cos - -halfH * sin) + center.x;

            _vertices[vert0 + 0].y = (-halfW * sin +  halfH * cos) + center.y;
            _vertices[vert0 + 1].y = ( halfW * sin +  halfH * cos) + center.y;
            _vertices[vert0 + 2].y = ( halfW * sin + -halfH * cos) + center.y;
            _vertices[vert0 + 3].y = (-halfW * sin + -halfH * cos) + center.y;

            _vertices[vert0 + 0].z = center.z - zSkew;
            _vertices[vert0 + 1].z = center.z - zSkew;
            _vertices[vert0 + 2].z = center.z;
            _vertices[vert0 + 3].z = center.z;

            _uv[vert0 + 0].x = uvTopLeft.x;
            _uv[vert0 + 0].y = uvTopLeft.y;

            _uv[vert0 + 1].x = uvTopLeft.x + uvSize.x;
            _uv[vert0 + 1].y = uvTopLeft.y;

            _uv[vert0 + 2].x = uvTopLeft.x + uvSize.x;
            _uv[vert0 + 2].y = uvTopLeft.y - uvSize.y;

            _uv[vert0 + 3].x = uvTopLeft.x;
            _uv[vert0 + 3].y = uvTopLeft.y - uvSize.y;

            _colors[vert0 + 0] = color;
            _colors[vert0 + 1] = color;
            _colors[vert0 + 2] = color;
            _colors[vert0 + 3] = color;

            ActiveQuadCount++;
        }
    }
}
