using Ming.Projectiles;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public MingProjectileManager ProjectileManager;
    public MingProjectile Projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProjectileManager.SpawnProjectile(ref Projectile);
        }
    }
}
