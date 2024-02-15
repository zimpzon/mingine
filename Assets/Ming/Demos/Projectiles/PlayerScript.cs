using Ming.Demos.Common;
using Ming.Projectiles;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public MingProjectileManager ProjectileManager;
    public MingProjectileBlueprint PlayerBulletBlueprint;

    void ProjectileUpdater(ref MingProjectile projectile)
    {
        projectile.Position += projectile.Velocity * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //var projectile = new MingProjectile();
            //projectile.ApplyBlueprint(PlayerBulletBlueprint);
            //ProjectileManager.SpawnProjectile(ref projectile);

            ProjectileSpawners.SpawnCircle(
                PlayerBulletBlueprint,
                transform.position,
                radius: 1.0f,
                count: 100,
                speed: 3,
                ProjectileManager,
                ProjectileUpdaters.BasicMove);
        }
    }
}
