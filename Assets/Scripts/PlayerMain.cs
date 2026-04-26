using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMain : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float Speed=10f;
    public float JumpForce=10f; 
    public float PlayerDrag = 5f; 
    public KeyCode JumpButton = KeyCode.Space;
    public Transform Orientation; 
    public float playerHeight; 
    public LayerMask WhatIsGround; 


    [Header("Set Dynamically")]
    public bool IsJumping=false; 
    public bool IsRunning; 
    public Rigidbody CharacterRB; 
    public bool grounded =true; 
    float Vertical;
    float Horizontal;


    // Start is called before the first frame update
    void Start()
    {
        //pull components 
        CharacterRB=GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //input:jump 
        if(Input.GetKey(JumpButton)&&grounded)
        {
            //Changestate: airborne
            jump(); 
        }
        //state: Walking 
        MovePlayer();
    }

    void Update()
    {
        GroundCheck();
        GetInput(); 
    }

    //func:Input 
    //desc: reads the input from the player for movement 
    private void GetInput()
    {
         Vertical=Input.GetAxis("Vertical");
         Horizontal=Input.GetAxis("Horizontal");
    }


    //func: MovePlayer 
    // Desc: Handles inputs for player movement
    void MovePlayer()
    {

        //set move vector (sum of cross of each direction)
        Vector3 MoveDirection=Orientation.forward*Vertical+Orientation.right*Horizontal; 

        //move using RB pyshics 
        CharacterRB.AddForce(MoveDirection.normalized*Speed*10f, ForceMode.Force);
    }

    //func: Jump
    //desc: allows the player to jump 
    private void jump()
    {
        //set y velocity to base zero 
        CharacterRB.velocity = new Vector3(CharacterRB.velocity.x, 0, CharacterRB.velocity.z);

        //add upward force(impulse)
        CharacterRB.AddForce(transform.up*JumpForce, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        //use a raycast to check for ground 
        grounded=Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f+0.2f,WhatIsGround);

        //change drag depending on jumping 
        if(grounded)
        {
            CharacterRB.drag=PlayerDrag; 
        }
        else
        {
            CharacterRB.drag=0; 
        }
    }

}
