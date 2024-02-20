using Ming;
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
        TextProjectileCount.SetText("Projectile count: {0}", ProjectileManager.ActiveProjectiles);
    }
}
