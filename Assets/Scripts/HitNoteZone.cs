using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public KeyCode hitKey;
    public PotionManager potionManager;
    public ParticleSystem successParticlePrefab;

    private List<GameObject> notesInZone = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            if (notesInZone.Count > 0)
            {
                GameObject noteToDestroy = notesInZone[0];
                
                if (noteToDestroy != null)
                {
                    notesInZone.RemoveAt(0);
                    
                    if (successParticlePrefab != null)
                    {
                        ParticleSystem particles = Instantiate(successParticlePrefab, noteToDestroy.transform.position, Quaternion.identity);
                        particles.Play();
                        Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constant);
                    }
                    
                    potionManager.Hit();
                    Destroy(noteToDestroy);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Note"))
        {
            notesInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(notesInZone.Contains(other.gameObject))
        {
            notesInZone.Remove(other.gameObject);
        }
    }

}
