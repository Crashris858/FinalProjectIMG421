using UnityEngine;

public class MissBoundary : MonoBehaviour
{
    public PotionManager potionManager;
    public ParticleSystem failParticlePrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if (failParticlePrefab != null)
            {
                ParticleSystem particles = Instantiate(failParticlePrefab, other.transform.position, Quaternion.identity);
                particles.Play();
                Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constant);
            }
            
            potionManager.Miss();
            Destroy(other.gameObject);
        }
    }
}
