using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ming
{
    public class MingGridWorld
    {
        public int ChunkCells => _chunkSize * _chunkSize;

        private readonly IMingGridChunkStore _chunkStore;
        private readonly IMingGridWorldBuilder _worldBuilder;
        private int _chunkSize;

        [NonSerialized] public readonly Dictionary<long, MingGridChunk> ActiveChunks = new();

        public MingGridWorld(IMingGridChunkStore chunkStore, IMingGridWorldBuilder worldBuilder, int chunkSize)
        {
            _chunkStore = chunkStore;
            _worldBuilder = worldBuilder;
            _chunkSize = chunkSize;
        }

        public void EnsureLoaded(RectInt tileRect)
        {
            RectInt chunkSpaceRect = MingGridUtil.GetTouchedChunks(tileRect, _chunkSize);

            foreach(Vector2Int gridPosition in chunkSpaceRect.allPositionsWithin)
            {
                long chunkId = MingGridUtil.GetChunkId(gridPosition);
                EnsureLoaded(chunkId, _chunkSize);
            }
        }

        public void EnsureLoaded(long chunkId, int chunkSize)
        {
            if (!ActiveChunks.ContainsKey(chunkId))
            {
                if (!_chunkStore.TryGetChunk(chunkId, out MingGridChunk newChunk))
                {
                    newChunk = _worldBuilder.CreateChunk(chunkId, chunkSize);
                }

                ActiveChunks[chunkId] = newChunk;
            }
        }
    }
}
