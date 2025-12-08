using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}
