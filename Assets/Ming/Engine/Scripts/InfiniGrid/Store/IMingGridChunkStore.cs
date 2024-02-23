namespace Ming
{
    public interface IMingGridChunkStore
    {
        bool TryGetChunk(ulong chunkId, out MingGridChunk loadedChunk);
        void ReturnChunk(MingGridChunk chunk);
    }
}
