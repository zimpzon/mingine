using Ming.Engine;
using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileUpdaters
    {
        public static bool ChaseTarget(ref MingProjectile p)
        {
            // Spawn fast -> stop -> start engines
            const float SpawnTime = 1.0f;

            float speed = p.Velocity.magnitude;

            float moveSpeed;
            if (p.TimeSinceSpawn < SpawnTime)
            {
                // First x seconds: Go from full speed to 0
                moveSpeed = (SpawnTime - p.TimeSinceSpawn) * (1.0f / SpawnTime);
                moveSpeed *= moveSpeed;
                moveSpeed *= speed;
            }
            else
            {
                // After x seconds: speed up to max speed
                float t = (p.TimeSinceSpawn - SpawnTime) * 2;
                moveSpeed = Mathf.Clamp(t * t * t, 0.0f, speed);
            }

            float turnPower = moveSpeed * 0.1f;
            if (p.TimeSinceSpawn < SpawnTime)
            {
                turnPower = 0.0f;
            }

            // this assumes target transform was set as CustomObject1 when spawning the projectile
            var desiredDir = (Vector2)((Transform)p.CustomObject1).position - p.Position;
            p.Velocity.x += (desiredDir.x - p.Velocity.x) * MingTime.DeltaTime * turnPower;
            p.Velocity.y += (desiredDir.y - p.Velocity.y) * MingTime.DeltaTime * turnPower;
            p.Velocity = p.Velocity.normalized;

            p.Position += p.Velocity * MingTime.DeltaTime * moveSpeed;
            p.RotationDegrees = Mathf.Atan2(p.Velocity.x, p.Velocity.y) * Mathf.Rad2Deg;

            // return false on destroy
            return true;
        }

        public static bool BasicMove(ref MingProjectile p)
        {
            p.Position += p.Velocity * MingTime.DeltaTime;

            bool res = Physics2D.OverlapCircle(p.Position, p.CollisionSize, p.CollisionMask);
            return !res;
        }
    }
}
