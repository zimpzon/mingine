using Ming.Demos.Common;
using Ming.Projectiles;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public MingProjectileManager ProjectileManager;
    public MingProjectileBlueprint PlayerBulletBlueprint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // can updaters be scriptable objects?
            ProjectileSpawners.SpawnSingle(PlayerBulletBlueprint, transform.position, Random.insideUnitCircle.normalized, 1, ProjectileManager, ProjectileUpdaters.BasicMove);
        }
    }
}
