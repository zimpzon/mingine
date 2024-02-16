using UnityEngine;

namespace Ming
{
    public static class MingSimpleSpriteAnimator
    {
        public static Sprite GetAnimationSprite(Sprite[] sprites, float animationFramesPerSecond, bool useUnscaledTime = true)
        {
            float t = useUnscaledTime ? MingTime.UnscaledTime : MingTime.Time;
            int id = (int)(t * animationFramesPerSecond) % sprites.Length;
            return sprites[id];
        }
    }
}
