using UnityEngine;

namespace Ming
{
    public interface IMingGridChunkStore
    {
        bool TryLoadChunk(Vector2Int chunkId, out MingGridChunk loadedChunk);
        void SaveChunk(MingGridChunk chunk);
    }
}
