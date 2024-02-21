using UnityEngine;

namespace Ming
{
    public class MingGridWorldBuilderDefault : IMingGridWorldBuilder
    {
        public MingGridChunk CreateChunk(Vector2Int worldPosition, int chunkSize)
        {
            return new MingGridChunk(worldPosition, chunkSize);
        }
    }
}
