using UnityEngine;

namespace Ming
{
    public class MingGridChunkRenderer
    {
        public void abc(MingGridChunk chunk, Mesh mesh)
        {
            // ou
            byte tileId = 1;

        }
    }

    public class MingGridChunk
    {
        public ulong ChunkId;
        public Vector2Int GridPosition;
        public RectInt GridBounds;

        public int[] FloorTiles;

        private int _chunkCells;

        public MingGridChunk(ulong chunkId, int chunkSize)
        {
            ChunkId = chunkId;
            GridPosition = MingGridUtil.GetChunkGridBottomLeftPosition(chunkId, chunkSize);
            GridBounds = new RectInt(GridPosition.x, GridPosition.y, chunkSize, chunkSize);

            _chunkCells = chunkSize * chunkSize;
            FloorTiles = new int[_chunkCells];
        }

        // Save data:
        // tile layer
        // 1 byte - tileId (floor type, wall type, etc.)
        // 
    }
}
