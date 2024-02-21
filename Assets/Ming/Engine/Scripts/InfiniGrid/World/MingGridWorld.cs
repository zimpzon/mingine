using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ming
{
    public class MingGridWorld
    {
        public const int ChunkSize = 16;
        public const int ChunkCells = ChunkSize * ChunkSize;

        private readonly IMingGridChunkStore _chunkStore;
        private readonly IMingGridWorldBuilder _worldBuilder;
        [NonSerialized] public readonly Dictionary<Vector2Int, MingGridChunk> LoadedChunks = new();

        public MingGridWorld(IMingGridChunkStore chunkStore, IMingGridWorldBuilder worldBuilder)
        {
            _chunkStore = chunkStore;
            _worldBuilder = worldBuilder;
        }

        public void EnsureLoaded(RectInt tileRect)
        {
            RectInt chunkRect = MingGridUtil.GetOverlappingChunks(tileRect);

            foreach (Vector2Int chunkId in chunkRect.allPositionsWithin)
            {
                EnsureLoaded(chunkId);
            }
        }

        public void EnsureLoaded(Vector2Int chunkId)
        {
            if (!LoadedChunks.ContainsKey(chunkId))
            {
                if (!_chunkStore.TryLoadChunk(chunkId, out MingGridChunk newChunk))
                {
                    newChunk = _worldBuilder.CreateChunk(chunkId, ChunkSize);
                }

                LoadedChunks[chunkId] = newChunk;
            }
        }
    }
}
