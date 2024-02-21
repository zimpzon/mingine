using UnityEngine;

namespace Ming
{
    public interface IMingGridWorldBuilder
    {
        MingGridChunk CreateChunk(Vector2Int chunkPosition, int chunkSize);
    }
}
