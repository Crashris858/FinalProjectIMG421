using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class PlayerMain : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float Speed=15f;
    public float MaxSpeed=20f; 
    public float JumpForce=30f; 
    public float PlayerDrag = 5f; 
    public KeyCode JumpButton = KeyCode.Space;
    public Transform Orientation; 
    public float playerHeight; 
    public LayerMask WhatIsGround; 
    private float DetectionDistace =100f; 
    public Camera CharacterCamera; 


    [Header("Set Dynamically")]
    public bool IsJumping=false; 
    public bool IsRunning; 
    public Rigidbody CharacterRB; 
    public bool grounded =true; 
    public PlayerInventory Inventory; 
    float Vertical;
    float Horizontal;
    Potion CurrentPotion = new AntiGravityPotion("Gravity", Potion.QualityLevel.Good);


    // Start is called before the first frame update
    void Start()
    {
        //pull components 
        CharacterRB=GetComponent<Rigidbody>();
        Inventory=GetComponent<PlayerInventory>();  
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
        //input: use potion 
        if(Input.GetMouseButtonDown(0))
        {
            if(CurrentPotion!=null)
            {
                CurrentPotion.ApplyEffect(this); 
                CurrentPotion=null; 
            }
        }
        //state: Walking 
        MovePlayer();
        
    }

    void Update()
    {
        ItemCheck();
        GroundCheck();
        GetInput(); 
        SpeedCheck();
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

    //func: speedCheck 
    //desc: takes the player's total speed at the given moment and limits
    private void SpeedCheck()
    {
        //get magnitude of velocity vector 
        Vector3 speedVector = new Vector3(CharacterRB.velocity.x,0f,CharacterRB.velocity.z);

        if(speedVector.magnitude>Speed)
        {
           //normalize 
           Vector3 limitVelocity = speedVector.normalized*Speed;
           CharacterRB.velocity= new Vector3(limitVelocity.x, CharacterRB.velocity.y, limitVelocity.z);
        }
    }

    //func: ItemCeheck
    //desc: checks if an item is in range
    private void ItemCheck()
    {
        //set up ray
        Ray Ray = CharacterCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitObject; 
        if(Physics.Raycast(Ray, out hitObject, DetectionDistace))
        {
        //check if itemdata class
            if(hitObject.collider.tag == "Object")
            {
                //if e press
                if(Input.GetKeyDown(KeyCode.E))
                {
                     print("Key Pressed");
                    //handle item pickup
                    ItemData Item = hitObject.collider.gameObject.GetComponentInParent<ItemData>();
                    if (Item != null)
                    {
                        //copy to the InventoryScript
                        Inventory.AddItem(Item);
                        Item.OnInteracted();
                    }
                }
            }   
        }
    }

}
