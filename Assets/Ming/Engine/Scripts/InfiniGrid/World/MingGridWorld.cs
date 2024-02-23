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
        private readonly int _chunkSize;

        [NonSerialized] public readonly Dictionary<ulong, MingGridChunk> ActiveChunks = new();

        public MingGridWorld(IMingGridChunkStore chunkStore, IMingGridWorldBuilder worldBuilder, int chunkSize)
        {
            _chunkStore = chunkStore;
            _worldBuilder = worldBuilder;
            _chunkSize = chunkSize;
        }

        public void EnsureLoaded(RectInt gridRect)
        {
            RectInt chunkSpaceRect = MingGridUtil.GetOverlappedChunks(gridRect, _chunkSize);

            for (int y = chunkSpaceRect.yMax - 1; y >= chunkSpaceRect.yMin; y--)
            {
                for (int x = chunkSpaceRect.xMin; x < chunkSpaceRect.xMax; x++)
                {
                    Vector2Int chunkBottomLeft = new(x * _chunkSize, y * _chunkSize);
                    ulong chunkId = MingGridUtil.GetChunkId(chunkBottomLeft, _chunkSize);
                    EnsureLoaded(chunkId);
                }
            }
        }

        public void EnsureLoaded(ulong chunkId)
        {
            if (!ActiveChunks.ContainsKey(chunkId))
            {
                if (!_chunkStore.TryGetChunk(chunkId, out MingGridChunk newChunk))
                {
                    newChunk = _worldBuilder.CreateChunk(chunkId, _chunkSize);
                }

                ActiveChunks[chunkId] = newChunk;
            }
        }
    }
}
