using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class water : MonoBehaviour
{
    public BoxCollider waterCollider =null; 
    
    public Material IceMaterial; 
    public Material WaterMaterial; 
    private Renderer WaterRenderer; 
    // Start is called before the first frame update
    void Start()
    {
        waterCollider = GetComponent<BoxCollider>(); 
        WaterRenderer=GetComponent<Renderer>();
        WaterRenderer.material=WaterMaterial;
        
    }

    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.CompareTag("IcePotion"))
        {
            //get potion duration 
            ThrowablePotion CurrentPotion = other.gameObject.GetComponent<ThrowablePotion>(); 
            if(CurrentPotion!=null)
            {
                //turn trigger to collider 
                waterCollider.isTrigger=false; 
                WaterRenderer.material=IceMaterial;
                Invoke("IceDefrost",CurrentPotion.effectDuration);  
                //destroy potion 
                Destroy(other);
            }
        }   
    }

    public void IceDefrost()
    {
        //turn on trigger 
        waterCollider.isTrigger=true; 
        //switch material 
        WaterRenderer.material=WaterMaterial;
    }
}