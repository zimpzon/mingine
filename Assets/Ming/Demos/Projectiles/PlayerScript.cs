using Ming.Demos.Common;
using Ming.Projectiles;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public MingProjectileManager ProjectileManager;
    public LayerMask PlayerBulletCollisionLayerMask;
    public MingProjectileBlueprint PlayerBulletBlueprint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // can updaters be scriptable objects?
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
