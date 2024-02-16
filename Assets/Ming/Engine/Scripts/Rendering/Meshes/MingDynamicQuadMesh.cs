using System.Collections.Generic;
using UnityEngine;

namespace Ming
{
    public class MingDynamicQuadMesh
    {
        Mesh _mesh;
        private readonly List<Vector3> _vertices;
        List<Vector2> _uv;
        List<int> _indices;
        List<Color> _colors;

        public MingDynamicQuadMesh(int initialCapacity)
        {
            _mesh = new Mesh();
            _mesh.MarkDynamic();

            const int VerticesPerQuad = 4;
            const int IndicesPerQuad = 6;
            _vertices = new List<Vector3>(initialCapacity * VerticesPerQuad);
            _uv = new List<Vector2>(initialCapacity * VerticesPerQuad);
            _colors = new List<Color>(initialCapacity * VerticesPerQuad);
            _indices = new List<int>(initialCapacity * IndicesPerQuad);
        }

        public int QuadCount()
        {
            return _vertices.Count / 4;
        }

        public void Clear()
        {
            _vertices.Clear();
            _uv.Clear();
            _colors.Clear();
            _indices.Clear();
        }

        public void ApplyChanges()
        {
            _mesh.Clear();
            _mesh.SetVertices(_vertices);
            _mesh.SetUVs(0, _uv);
            _mesh.SetColors(_colors);
            _mesh.SetTriangles(_indices, 0, false);
            _mesh.UploadMeshData(false);
        }

        public Mesh GetMesh()
        {
            return _mesh;
        }

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Color color)
        {
            AddQuad(center, size, rotationDegrees, zSkew, Vector2.up, Vector2.one, color);
        }

        public void AddQuad(Vector3 center, Vector2 size, float rotationDegrees, float zSkew, Vector2 UVTopLeft, Vector2 uvSize, Color color)
        {
            // 0---1
            // | / | = [0, 1, 3] and [1, 2, 3]
            // 3---2

            float halfW = size.x * 0.5f;
            float halfH = size.y * 0.5f;
            float halfSkew = zSkew * 0.5f;
            _vertices.Add(new Vector3(center.x - halfW, center.y + halfH, center.z - halfSkew));
            _vertices.Add(new Vector3(center.x + halfW, center.y + halfH, center.z - halfSkew));
            _vertices.Add(new Vector3(center.x + halfW, center.y - halfH, center.z + halfSkew));
            _vertices.Add(new Vector3(center.x - halfW, center.y - halfH, center.z + halfSkew));

            _uv.Add(UVTopLeft);
            _uv.Add(new Vector2(UVTopLeft.x + uvSize.x, UVTopLeft.y));
            _uv.Add(new Vector2(UVTopLeft.x + uvSize.x, UVTopLeft.y - uvSize.y));
            _uv.Add(new Vector2(UVTopLeft.x, UVTopLeft.y - uvSize.y));

            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);

            int idx0 = _vertices.Count - 4;
            _indices.Add(idx0 + 0);
            _indices.Add(idx0 + 1);
            _indices.Add(idx0 + 3);

            _indices.Add(idx0 + 1);
            _indices.Add(idx0 + 2);
            _indices.Add(idx0 + 3);
        }
    }
}
