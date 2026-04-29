using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowablePotion : MonoBehaviour
{
    //i know 5 line script 
    public float effectDuration = 0f; 
    public float LifeTime =20f; 
    public Renderer ThrowableRender = null;

    void Awake()
    {
        //pull renderer
        ThrowableRender=GetComponent<Renderer>(); 
        Invoke("DestroyPotion", LifeTime);
    }

    private void DestroyPotion()
    {
        Destroy(this);
    }
}
