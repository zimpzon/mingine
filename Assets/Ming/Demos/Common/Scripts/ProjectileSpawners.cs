using Ming.Projectiles;
using UnityEngine;

namespace Ming.Demos.Common
{
    public static class ProjectileSpawners
    {
        public static void SpawnCircle(MingProjectileBlueprint blueprint, Vector2 origin, float radius, int count, float speed, MingProjectileManager manager, MingProjectile.UpdateProjectileCallback updateFunc)
        {
            var projectile = new MingProjectile();
            projectile.ApplyBlueprint(blueprint);

            for (int i = 0; i < count; ++i)
            {
                float angleDegrees = (360.0f / count) * i;
                float angle = angleDegrees * Mathf.Deg2Rad;
                var dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 0.5f;
                var pos = origin + dir * radius;

                projectile.Position = pos;
                projectile.RotationDegrees = angleDegrees;
                projectile.Velocity = dir * speed;
                projectile.UpdateProjectile = updateFunc;

                manager.SpawnProjectile(ref projectile);
            }
        }

        // TODO: factory something? Param in an object?
        public static void SpawnSingle(MingProjectileBlueprint blueprint, Vector2 origin, Vector2 dir, float speed, MingProjectileManager manager, MingProjectile.UpdateProjectileCallback updateFunc)
        {
            var projectile = new MingProjectile();
            projectile.ApplyBlueprint(blueprint);

            projectile.Position = origin;
            projectile.RotationDegrees = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            projectile.Velocity = dir * speed;
            projectile.UpdateProjectile = updateFunc;

            manager.SpawnProjectile(ref projectile);
        }

        public static void SpawnPattern(MingProjectileBlueprint blueprint, Vector2 origin, Vector2 velocity, string[] pattern, MingProjectileManager manager)
        {
            var projectile = new MingProjectile();
            projectile.ApplyBlueprint(blueprint);

            foreach (var pos in ProjectilePatterns.PatternPositions(pattern, 0.5f))
            {
                projectile.Velocity = velocity;
                projectile.UpdateProjectile = ProjectileUpdaters.BasicMove;
            }
        }
    }
}
