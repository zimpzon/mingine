using Ming.Engine;
using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileUpdaters
    {
        public static bool BasicMove(ref MingProjectile p)
        {
            p.ActualPos += p.Velocity * MingTime.DeltaTime;

            // return false on destroy
            return true;
        }

        public static bool CirclingMove(ref MingProjectile p)
        {
            p.Origin += p.Velocity * MingTime.DeltaTime;
            var oldPos = p.ActualPos;

            float deg = Time.time * 500 + p.Idx * 5;
            float sin = Mathf.Sin(-deg * Mathf.Deg2Rad);
            float cos = Mathf.Cos(-deg * Mathf.Deg2Rad);

            p.ActualPos.x = p.Origin.x + cos - sin;
            p.ActualPos.y = p.Origin.y + sin + cos;

            var move = p.ActualPos - oldPos;
            p.RotationDegrees = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;

            // return false on destroy
            return true;
        }

        public static bool ChasePlayer(ref MingProjectile p)
        {
            float timeAlive = MingTime.Time - p.StartTime;

            // Spawn fast -> stop -> start engines
            const float SpawnTime = 1.0f;

            float moveSpeed = 0.0f;
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

            // this assumes player transform was set as CustomData when spawning the projectile
            var desiredDir = (Vector2)((Transform)p.CustomData).position - p.ActualPos;
            p.Velocity.x += (desiredDir.x - p.Velocity.x) * MingTime.DeltaTime * turnPower;
            p.Velocity.y += (desiredDir.y - p.Velocity.y) * MingTime.DeltaTime * turnPower;
            p.Velocity = p.Velocity.normalized;

            p.ActualPos += p.Velocity * MingTime.DeltaTime * moveSpeed * p.Speed;
            p.RotationDegrees = Mathf.Atan2(p.Velocity.x, p.Velocity.y) * Mathf.Rad2Deg;

            // return false on destroy
            return true;
        }

        public static bool UpdateProjectile2(ref MingProjectile p)
        {
            p.ActualPos += p.Velocity * MingTime.DeltaTime;
            float dist = (p.ActualPos - p.StartPos).sqrMagnitude;
            if (dist > p.MaxDist * p.MaxDist)
                return false;

            p.Velocity += p.Velocity * 1.5f * Time.deltaTime;
            // return false on destroy
            return true;
        }
    }
}
