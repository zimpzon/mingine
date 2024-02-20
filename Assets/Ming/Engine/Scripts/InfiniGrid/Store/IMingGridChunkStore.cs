using UnityEngine;

namespace Ming
{
    public interface IMingGridChunkStore
    {
        bool TryLoadChunk(Vector2Int chunkCell, out MingGridChunk loadedChunk);
        void SaveChunk(Vector2Int chunkPos);
    }
}
