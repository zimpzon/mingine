using UnityEngine;

namespace Ming.Animation
{
    [CreateAssetMenu(fileName = "new MingSpriteAnimationFrames_IdleRun.asset", menuName = "Ming/Animations/Sprite Frames, IdleRun", order = 20)]
    public class MingSpriteAnimationFrames_IdleRun : ScriptableObject
    {
        public float DefaultAnimationFramesPerSecond;
        public Sprite[] Idle;
        public Sprite[] Run;
    }
}
