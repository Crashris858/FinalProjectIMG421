using UnityEngine;

[System.Serializable]
public class ItemData : MonoBehaviour
{
    public string ItemName;
    public int ItemID; 
    public float RespawnTime= 60f; 
    private MeshRenderer meshRenderer=null; 
    private SphereCollider sphereCollider=null;
    public void Start()
    {
        //get components 
        meshRenderer=GetComponentInChildren<MeshRenderer>();
        sphereCollider=GetComponentInChildren<SphereCollider>();

    }
    public void FixedUpdate()
    {
        //rotate 
        this.transform.RotateAround(transform.position,Vector3.up, 1f);
    }

    public void OnInteracted()
    {
         meshRenderer.enabled=false;
         sphereCollider.enabled=false;
         StartCoroutine(ReEnable());
    }

    public IEnumerator ReEnable()
    {
        yield return new WaitForSeconds(RespawnTime);
        meshRenderer.enabled=true;
        sphereCollider.enabled=true;
    }

}
