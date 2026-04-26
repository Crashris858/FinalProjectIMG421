using UnityEngine;

public class MissBoundary : MonoBehaviour
{
    public PotionManager potionManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            potionManager.Miss();
            Destroy(other.gameObject);
            // Debug.Log("Note Missed!");
        }
    }
}
