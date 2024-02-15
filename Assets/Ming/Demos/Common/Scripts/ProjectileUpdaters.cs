using Ming.Engine;
using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileUpdaters
    {
        public static bool CirclingMove(ref MingProjectile p)
        {
            p.Origin += p.Velocity * MingTime.DeltaTime;
            var oldPos = p.Position;

            float deg = MingTime.Time * 500 + p.Idx * 5;
            float sin = Mathf.Sin(-deg * Mathf.Deg2Rad);
            float cos = Mathf.Cos(-deg * Mathf.Deg2Rad);

            p.Position.x = p.Origin.x + cos - sin;
            p.Position.y = p.Origin.y + sin + cos;

            var move = p.Position - oldPos;
            p.RotationDegrees = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;

            // return false on destroy
            return true;
        }

        public static bool ChaseTarget(ref MingProjectile p)
        {
            float timeAlive = MingTime.Time - p.StartTime;

            // Spawn fast -> stop -> start engines
            const float SpawnTime = 1.0f;

            float moveSpeed;
            if (timeAlive < SpawnTime)
            {
                // First x seconds: Go from full speed to 0
                moveSpeed = (SpawnTime - timeAlive) * (1.0f / SpawnTime);
                moveSpeed *= moveSpeed;
                moveSpeed *= p.Speed;
            }
            else
            {
                // After x seconds: speed up to max speed
                float t = (timeAlive - SpawnTime) * 2;
                moveSpeed = Mathf.Clamp(t * t * t, 0.0f, p.Speed);
            }

            float turnPower = moveSpeed * 0.1f;
            if (timeAlive < SpawnTime)
                turnPower = 0.0f;

            // this assumes target transform was set as CustomObject1 when spawning the projectile
            var desiredDir = (Vector2)((Transform)p.CustomObject1).position - p.Position;
            p.Velocity.x += (desiredDir.x - p.Velocity.x) * MingTime.DeltaTime * turnPower;
            p.Velocity.y += (desiredDir.y - p.Velocity.y) * MingTime.DeltaTime * turnPower;
            p.Velocity = p.Velocity.normalized;

            p.Position += p.Velocity * MingTime.DeltaTime * moveSpeed * p.Speed;
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
