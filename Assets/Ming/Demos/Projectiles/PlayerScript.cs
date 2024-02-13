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
        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.red);

        bool res = Physics2D.OverlapCircle(transform.position, 0.4f, PlayerBulletCollisionLayerMask);
        Debug.Log(res);

        if (Input.GetKey(KeyCode.E))
        {
            for (int i = 0; i < 50; i++)
            {
                // can updaters be scriptable objects?
                ProjectileSpawners.SpawnSingle(
                    PlayerBulletBlueprint,
                    transform.position,
                    Random.insideUnitCircle.normalized,
                    speed: 1,
                    ProjectileManager,
                    ProjectileUpdaters.BasicMove);
            }
        }
    }
}
