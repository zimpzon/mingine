namespace Ming
{
    public interface IMingGridWorldBuilder
    {
        MingGridChunk CreateChunk(ulong chunkId, int chunkSize);
    }
}
