namespace Ming
{
    public class MingGridWorldBuilderDefault : IMingGridWorldBuilder
    {
        public MingGridChunk CreateChunk(ulong chunkId, int chunkSize)
        {
            return new MingGridChunk(chunkId, chunkSize);
        }
    }
}
