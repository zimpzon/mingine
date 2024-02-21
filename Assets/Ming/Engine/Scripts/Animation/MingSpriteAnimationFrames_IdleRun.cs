using UnityEngine;

namespace Ming
{
    [CreateAssetMenu(fileName = "new MingSpriteAnimationFrames_IdleRun.asset", menuName = "Ming/Animations/Sprite Frames, IdleRun")]
    public class MingSpriteAnimationFrames_IdleRun : ScriptableObject
    {
        public float AnimationFramesPerSecond;
        public Sprite[] Idle;
        public Sprite[] Run;
    }
}
