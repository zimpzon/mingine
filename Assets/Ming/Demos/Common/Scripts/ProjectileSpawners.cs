using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileSpawners
    {
        public static void SpawnCircle(MingProjectileBlueprint desc, Vector2 origin, float radius, int count, float speed, MingProjectileManager manager, MingProjectile.TickDelegate updateFunc)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(desc);

            for (int i = 0; i < count; ++i)
            {
                float angleDegrees = (360.0f / count) * i;
                float angle = angleDegrees * Mathf.Deg2Rad;
                var dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.5f;
                var pos = origin + dir * radius;

                proto.Idx = i;
                proto.StartPos = pos;
                proto.Origin = pos;
                proto.ActualPos = pos;
                proto.RotationDegrees = angleDegrees;
                proto.Speed = speed;
                proto.Velocity = dir * speed;
                proto.UpdateCallback = updateFunc;

                manager.SpawnProjectile(ref proto);
            }
        }

        public static void SpawnSingle(MingProjectileBlueprint desc, Vector2 origin, Vector2 dir, float speed, MingProjectileManager manager, MingProjectile.TickDelegate updateFunc)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(desc);

            proto.StartPos = origin;
            proto.Origin = origin;
            proto.ActualPos = origin;
            proto.RotationDegrees = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            proto.Speed = speed;
            proto.Velocity = dir * speed;
            proto.UpdateCallback = updateFunc;

            manager.SpawnProjectile(ref proto);
        }

        public static void SpawnPattern(MingProjectileBlueprint desc, Vector2 origin, Vector2 velocity, string[] pattern, MingProjectileManager manager)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(desc);

            foreach (var pos in ProjectilePatterns.PatternPositions(pattern, 0.5f))
            {
                proto.Origin = origin;
                proto.StartPos = origin + pos;
                proto.OriginOffset = origin + pos;
                proto.Velocity = velocity;
                proto.UpdateCallback = ProjectileUpdaters.BasicMove;
                proto.ActualPos = proto.StartPos;
            }
        }
    }
}
