using UnityEngine;

public class FallingNote : MonoBehaviour
{
    [Header("Note Settings")]
    public float fallSpeed = 5f;

    void Update()
    {
        // using world space due to local prefab rotation
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime, Space.World);
    }
}
