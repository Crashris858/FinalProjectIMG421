using UnityEngine;

[System.Serializable]
public class ItemData : MonoBehaviour
{
    public string ItemName;
    public int ItemID; 
    public void FixedUpdate()
    {
        //rotate 
        this.transform.RotateAround(transform.position,Vector3.up, 1f);
    }

    public void OnInteracted()
    {
         Destroy(this.gameObject);
    }

}
