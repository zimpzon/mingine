using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileSpawners
    {
        public static void SpawnCircle(MingProjectileBlueprint blueprint, Vector2 origin, float radius, int count, float speed, MingProjectileManager manager, MingProjectile.TickDelegate updateFunc)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(blueprint);

            for (int i = 0; i < count; ++i)
            {
                float angleDegrees = (360.0f / count) * i;
                float angle = angleDegrees * Mathf.Deg2Rad;
                var dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.5f;
                var pos = origin + dir * radius;

                proto.Idx = i;
                proto.StartPos = pos;
                proto.Origin = pos;
                proto.Position = pos;
                proto.RotationDegrees = angleDegrees;
                proto.Speed = speed;
                proto.Velocity = dir * speed;
                proto.UpdateCallback = updateFunc;

                manager.SpawnProjectile(ref proto);
            }
        }

        // TODO: factory something? Param in an object?
        public static void SpawnSingle(MingProjectileBlueprint blueprint, Vector2 origin, Vector2 dir, float speed, MingProjectileManager manager, MingProjectile.TickDelegate updateFunc)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(blueprint);

            proto.StartPos = origin;
            proto.Origin = origin;
            proto.Position = origin;
            //proto.RotationDegrees = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            proto.Speed = speed;
            proto.Velocity = dir * speed;
            proto.UpdateCallback = updateFunc;

            manager.SpawnProjectile(ref proto);
        }

        public static void SpawnPattern(MingProjectileBlueprint blueprint, Vector2 origin, Vector2 velocity, string[] pattern, MingProjectileManager manager)
        {
            var proto = new MingProjectile();
            proto.ApplyBlueprint(blueprint);

            foreach (var pos in ProjectilePatterns.PatternPositions(pattern, 0.5f))
            {
                proto.Origin = origin;
                proto.StartPos = origin + pos;
                proto.OriginOffset = origin + pos;
                proto.Velocity = velocity;
                proto.UpdateCallback = ProjectileUpdaters.BasicMove;
                proto.Position = proto.StartPos;
            }
        }
    }
}
