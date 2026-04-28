using UnityEngine;

public class MissBoundary : MonoBehaviour
{
    public PotionManager potionManager;
    public ParticleSystem failParticlePrefab;

    // checks if a note enters the miss boundary, if so it calls the miss function in potion manager and destroys the note
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
