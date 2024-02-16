using Ming;
using UnityEngine;

namespace Ming
{
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("Ming/Animation/MingSimpleSpriteAnimator")]
    public class MingSimpleSpriteAnimator : MingBehaviour
    {
        public Sprite[] AnimationSprites;
        public float AnimationFramesPerSecond = 5.0f;

        SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _renderer.sprite = GetAnimationSprite(AnimationSprites, AnimationFramesPerSecond);
        }

        public static Sprite GetAnimationSprite(Sprite[] sprites, float animationFramesPerSecond, bool useUnscaledTime = true)
        {
            float t = useUnscaledTime ? MingTime.UnscaledTime : MingTime.Time;
            int id = (int)(t * animationFramesPerSecond) % sprites.Length;
            return sprites[id];
        }

        public static Rect GetAnimationUvRect(Sprite[] sprites, float animationFramesPerSecond)
        {
            Sprite spr = GetAnimationSprite(sprites, animationFramesPerSecond);
            return spr.textureRect;
        }
    }
}
