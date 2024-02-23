using UnityEngine;

namespace Ming
{
    public class MingGridWorldBuilderDefault : IMingGridWorldBuilder
    {
        public MingGridChunk CreateChunk(ulong chunkId, int chunkSize)
        {
            MingGridChunk newChunk = new(chunkId, chunkSize);
            for (int i = 0; i < newChunk.FloorTiles.Length; i++)
            {
                newChunk.FloorTiles[i] = Random.value < 0.1f ? 0 : 1;
            }
            return newChunk;
        }
    }
}
