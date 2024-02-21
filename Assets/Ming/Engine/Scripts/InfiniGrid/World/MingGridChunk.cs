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
        public Vector2Int ChunkId;
        public Vector2Int GridPosition;
        public RectInt GridBounds;

        public int[] FloorTiles;

        private int _chunkCells;

        public MingGridChunk(Vector2Int chunkId, int chunkSize)
        {
            ChunkId = chunkId;
            GridPosition = ChunkId * chunkSize;
            GridBounds = new RectInt(GridPosition.x, GridPosition.y, chunkSize, chunkSize);

            _chunkCells = chunkSize * chunkSize;
            FloorTiles = new int[_chunkCells];
        }

        // sort order:
        //   floor tiles ("walls" are plain floor tiles, below a roof tile)
        //   props
        //   items
        //   npc's
        //   grounded projectiles
        //   player
        //   top part of tall props
        //   roof (only relevant where tile above is not a roof tile
        //   flying (incl. flying projectiles)

        // rendering:
        //    use sprite layers because particles (and possibly other things)

        // ChunkSize^2 must be uploaded to the GPU when a single tile changes
        // Up to 4 chunks may be affected by explosions, etc.

        // Save data:
        // tile layer
        // 1 byte - tileId (floor type, wall type, etc.)
        // 
    }
}
