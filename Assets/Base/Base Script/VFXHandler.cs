using UnityEngine;
using UnityEngine.Serialization;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem powerUpParticle;
    [SerializeField] private ParticleSystem powerUpIndicatorParticle;
    [FormerlySerializedAs("playerMovement")] [SerializeField] private Player player;

    private void Start()
    {
        player.onVFXTriggered += StartAnimation;
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.onVFXTriggered -= StartAnimation;
        }
    }

    private void StartAnimation()
    {
        powerUpParticle.Play();
        powerUpIndicatorParticle.Play();

    }
}
