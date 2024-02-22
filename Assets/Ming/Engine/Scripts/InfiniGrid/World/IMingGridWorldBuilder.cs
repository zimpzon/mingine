namespace Ming
{
    public interface IMingGridWorldBuilder
    {
        MingGridChunk CreateChunk(long chunkId, int chunkSize);
    }
}
