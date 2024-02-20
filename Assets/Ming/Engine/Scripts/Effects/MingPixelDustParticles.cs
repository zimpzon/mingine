using Ming;
using System;
using UnityEngine;

public class MingPixelDustParticles : MingBehaviour
{
    public static void Trigger(Vector3 position, Color color, int amount)
    {
        if (TriggerAction == null)
        {
            Debug.LogWarning($"No active {nameof(MingPixelDustParticles)} found, add it to the scene");
        }

        TriggerAction?.Invoke(position, color, amount);
    }

    private static Action<Vector3, Color, int> TriggerAction;

    private void Awake()
    {
        TriggerAction = OnTrigger;
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnDestroy()
    {
        TriggerAction = null;
    }

    private ParticleSystem _particleSystem;

    private void OnTrigger(Vector3 position, Color color, int amount)
    {
        _particleSystem.transform.position = position;
        var main = _particleSystem.main;
        main.startColor = color;
        _particleSystem.Emit(amount);
    }
}
