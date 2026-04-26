using UnityEngine;

public class FallingNote : MonoBehaviour
{
    [Header("Note Settings")]
    public float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime, Space.World);
    }
}
