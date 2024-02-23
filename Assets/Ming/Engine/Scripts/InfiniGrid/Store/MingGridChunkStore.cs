using UnityEngine;

namespace Ming
{
    public class MingGridChunkStore : IMingGridChunkStore
    {
        public bool TryGetChunk(ulong chunkId, out MingGridChunk loadedChunk)
        {
            loadedChunk = null;
            return false;
        }

        public void ReturnChunk(MingGridChunk chunk)
        {
            throw new System.NotImplementedException();
        }
    }
}
