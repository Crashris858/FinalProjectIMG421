using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;

    public void SpawnNote()
    {
        Instantiate(notePrefab, transform.position, Quaternion.identity);
    }
}
