namespace Ming
{
    public class MingGridWorldBuilderDefault : IMingGridWorldBuilder
    {
        public MingGridChunk CreateChunk(long chunkId, int chunkSize)
        {
            return new MingGridChunk(chunkId, chunkSize);
        }
    }
}
