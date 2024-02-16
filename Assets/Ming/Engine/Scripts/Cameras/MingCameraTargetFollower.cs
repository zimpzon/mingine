using UnityEngine;

namespace Ming
{
    [AddComponentMenu("Ming/Cameras/MingCameraTargetFollower")]
    public class MingCameraTargetFollower : MingBehaviour
    {
        public float MoveSpeed = 5.0f;
        public Transform Target;
        public float TargetOffsetZ;
        public float MoveSpeedZ = 5;
        public float DistanceFromTarget;
        public float DistanceFromTargetOffsetZ;

        Vector3 _currentPos;
        float _currentOffsetZ;
        Transform _trans;

        private void Awake()
        {
            _trans = transform;
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void SetPosition(Vector3 pos)
        {
            _currentPos = pos;
        }

        private void Update()
        {
            UpdateCamera(Time.unscaledDeltaTime);
        }

        public void UpdateCamera(float dt)
        {
            // XY
            var movement = (Target.position - _currentPos);
            DistanceFromTarget = movement.magnitude;
            movement *= MoveSpeed;

            const float CloseEnough = 0.1f;
            if (DistanceFromTarget > CloseEnough)
                _currentPos += movement * dt;

            // Z
            float moveZ = (TargetOffsetZ - _currentOffsetZ);
            DistanceFromTargetOffsetZ = moveZ;
            moveZ *= MoveSpeedZ;

            const float CloseEnoughZ = 0.1f;
            if (Mathf.Abs(moveZ) > CloseEnoughZ)
                _currentOffsetZ += moveZ * dt;

            _currentPos.z = _currentOffsetZ;
            _trans.localPosition = _currentPos;
        }
    }
}
