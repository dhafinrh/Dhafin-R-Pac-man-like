using UnityEngine;
using UnityEngine.Serialization;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem powerUpParticle;
    [SerializeField] private ParticleSystem powerUpIndicatorParticle;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.onVFXTriggered += StartAnimation;
    }

    private void OnDestroy()
    {
        if (playerMovement != null)
        {
            playerMovement.onVFXTriggered -= StartAnimation;
        }
    }

    private void StartAnimation()
    {
        powerUpParticle.Play();
        powerUpIndicatorParticle.Play();

    }
}
