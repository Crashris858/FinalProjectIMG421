using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    public KeyCode hitKey;
    public PotionManager potionManager;

    private List<GameObject> notesInZone = new List<GameObject>();

    void Update()
    {
        if(Input.GetKeyDown(hitKey))
        {
            if(notesInZone.Count > 0)
            {
                Destroy(notesInZone[0]);
                notesInZone.RemoveAt(0);
                potionManager.Hit();
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
            potionManager.Miss();
            Destroy(other.gameObject);
        }
    }

}
