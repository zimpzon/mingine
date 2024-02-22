using UnityEngine;

namespace Ming
{
    public class MingGridChunkStore : IMingGridChunkStore
    {
        public bool TryGetChunk(long chunkId, out MingGridChunk loadedChunk)
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
