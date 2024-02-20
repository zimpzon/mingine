using UnityEngine;

namespace Ming
{
    public class MingGridChunkStore : IMingGridChunkStore
    {
        public bool TryLoadChunk(Vector2Int chunkCell, out MingGridChunk loadedChunk)
        {
            loadedChunk = null;
            return false;
        }

        public void SaveChunk(Vector2Int chunkPos)
        {
            throw new System.NotImplementedException();
        }
    }
}
