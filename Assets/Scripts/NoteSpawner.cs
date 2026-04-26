using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;

    public void SpawnNote()
    {
        Instantiate(notePrefab, transform.position, notePrefab.transform.rotation);
    }
}
