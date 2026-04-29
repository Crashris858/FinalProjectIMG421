using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;

public class Flame : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform Flamepoint;
    [Header("Set Dynamically")]
    public BoxCollider FlameCollider =null;  
    public void Start()
    {
        FlameCollider=GetComponent<BoxCollider>(); 
    }
    public void OnTriggerEnter(Collider collision)
    {
        //if collider is player 
        if(collision.gameObject.CompareTag("Player"));
        {
            PlayerMain player = collision.gameObject.GetComponent<PlayerMain>(); 
            print("player collected");
            //double check for component 
            if(player!=null)
            {
                print("player recongized");

                //edge case: in flames 
                if(!player.FireResist)
                {
                    //transport player to given point 
                    player.transform.position=Flamepoint.position;
                }
                //else let player pass 
            }
        }
    }
}
