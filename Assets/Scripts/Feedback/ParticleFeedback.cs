using UnityEngine;

public class ParticleFeedback : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void SpawnParticle()
    {
        Instantiate(_particle, transform.position, Quaternion.identity);
    }
}
