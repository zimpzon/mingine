using Ming.Projectiles;
using TMPro;
using UnityEngine;

public class DemoMain : MonoBehaviour
{
    public TMP_Text TextProjectileCount;
    public MingProjectileManager ProjectileManager;

    void Start()
    {
        
    }

    void Update()
    {
        TextProjectileCount.text = "Projectile count: " + ProjectileManager.ActiveProjectiles;
    }
}
