using System.Collections.Generic;
using UnityEngine;

namespace Ming
{
    public class MingGridWorld
    {
        public const int ChunkSize = 8;
        public const int ChunkCells = ChunkSize * ChunkSize;

        private readonly IMingGridChunkStore _chunkStore;
        private readonly IMingGridWorldBuilder _worldBuilder;
        private readonly Dictionary<Vector2Int, MingGridChunk> _loadedChunks = new();

        public MingGridWorld(IMingGridChunkStore chunkStore, IMingGridWorldBuilder worldBuilder)
        {
            _chunkStore = chunkStore;
            _worldBuilder = worldBuilder;
        }

        public void EnsureLoaded(RectInt cellArea)
        {
            RectInt chunkRect = MingGridUtil.ChunkRectFromCellRect(cellArea);

            foreach (Vector2Int chunkCell in chunkRect.allPositionsWithin)
            {
                EnsureLoaded(chunkCell);
            }
        }

        public void EnsureLoaded(Vector2Int chunkCell)
        {
            if (!_loadedChunks.ContainsKey(chunkCell))
            {
                if (!_chunkStore.TryLoadChunk(chunkCell, out MingGridChunk newChunk))
                {
                    newChunk = _worldBuilder.CreateChunk(chunkCell);
                }

                _loadedChunks[chunkCell] = newChunk;
            }
        }
    }
}
