using UnityEngine;

namespace Ming
{
    public class MingGridChunkStore : IMingGridChunkStore
    {
        public bool TryLoadChunk(Vector2Int chunkId, out MingGridChunk loadedChunk)
        {
            loadedChunk = null;
            return false;
        }

        public void SaveChunk(MingGridChunk chunk)
        {
            throw new System.NotImplementedException();
        }
    }
}
