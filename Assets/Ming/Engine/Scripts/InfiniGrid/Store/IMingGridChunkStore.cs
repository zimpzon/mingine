namespace Ming
{
    public interface IMingGridChunkStore
    {
        bool TryGetChunk(long chunkId, out MingGridChunk loadedChunk);
        void SaveChunk(MingGridChunk chunk);
    }
}
