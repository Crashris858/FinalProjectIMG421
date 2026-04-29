using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance { get; private set; }

    [Header("Inventory & UI")]
    public Potion[] potionBelt = new Potion[5]; 
    public int activeSlotIndex = 0;
    private UIHotbarManager _hotbar; // Reference for optimization

    [Header("Movement Settings")]
    public float Speed = 15f;
    public float JumpForce = 30f;
    public float MaxY =40f;  
    public float PlayerDrag = 5f; 
    public Transform Orientation; 
    public float playerHeight; 
    public LayerMask WhatIsGround; 
    public Camera CharacterCamera; 
    public ThrowablePotion ThrowPrefab = null; 
    public float ThrowForce = 40f;

    [Header("Status")]
    public bool IsJumping = false; 
    public Rigidbody CharacterRB; 
    public bool grounded = true; 
    public PlayerInventory Inventory; 
    public bool canMove = true;
    public bool FireResist = false; 

    public AudioSource usePotion;


    private float Vertical;
    private float Horizontal;
    private float DetectionDistace = 100f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //pull components 
        CharacterRB = GetComponent<Rigidbody>();
        Inventory = GetComponent<PlayerInventory>();
        _hotbar = FindObjectOfType<UIHotbarManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //input:jump 
        if(IsJumping && grounded)
        {
            //Change state: airborne
            jump();
            IsJumping = false;
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
        YCheck(); 

        if(Input.GetKeyDown(KeyCode.Space)) IsJumping = true;

        // input: use potion 
        if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Potion activePotion = potionBelt[activeSlotIndex];
                if(activePotion != null)
                {
                    usePotion.Play();
                    activePotion.ApplyEffect(this); 
                    potionBelt[activeSlotIndex] = null;
                    _hotbar.UpdateSlotVisuals();
                }
            }
        }
    }

    //func:Input 
    //desc: reads the input from the player for movement 
    private void GetInput()
    {
         Vertical = Input.GetAxis("Vertical");
         Horizontal = Input.GetAxis("Horizontal");
    }


    //func: MovePlayer 
    // Desc: Handles inputs for player movement
    void MovePlayer()
    {
        if(!canMove) return;

        //set move vector (sum of cross of each direction)
        Vector3 MoveDirection=Orientation.forward * Vertical + Orientation.right * Horizontal; 

        //move using RB pyshics 
        CharacterRB.AddForce(MoveDirection.normalized * Speed * 10f, ForceMode.Force);
    }

    //func: Jump
    //desc: allows the player to jump 
    private void jump()
    {

        //add upward force(impulse)
        CharacterRB.AddForce(transform.up * JumpForce, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        //use a raycast to check for ground 
        grounded=Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);

        //change drag depending on jumping 
        if(grounded)
        {
            CharacterRB.drag = PlayerDrag; 
        }
        else
        {
            CharacterRB.drag = 0; 
        }
    }

    //func: speedCheck 
    //desc: takes the player's total speed at the given moment and limits
    private void SpeedCheck()
    {
        //get magnitude of velocity vector 
        Vector3 speedVector = new Vector3(CharacterRB.velocity.x, 0f, CharacterRB.velocity.z);

        if(speedVector.magnitude>Speed)
        {
           //normalize 
           Vector3 limitVelocity = speedVector.normalized * Speed;
           CharacterRB.velocity= new Vector3(limitVelocity.x, CharacterRB.velocity.y, limitVelocity.z);
        }
    }

    //func: yCheck 
    //desc: locks the players y position 
    private void YCheck()
    {
        //if out of bounds
        if(transform.position.y>MaxY)
        {
            //clamp to max y
            Vector3 pos = transform.position;
            pos.y=MaxY; 
            transform.position=pos; 
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
        //check if item data class
            if(hitObject.collider.tag == "Object")
            {
                //if e press
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //print("Key Pressed");
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

    public void AddPotionToBelt(Potion newPotion)
    {
        for (int i = 0; i < potionBelt.Length; i++)
        {
            if (potionBelt[i] == null)
            {
                potionBelt[i] = newPotion;
                Debug.Log($"Stored {newPotion.potionName} in slot {i + 1}");
                
                _hotbar.UpdateSlotVisuals();
                return;
            }
        }
        Debug.Log("Potion belt is full!");
    }

    //func: throw Potion
    //desc: throws a potion. This is called by other scripts. 
    public void ThrowPotion(Potion currentPotion)
    {
        //instantiate potion object 
        ThrowablePotion ReadyPotion = Instantiate(ThrowPrefab,transform.position, Quaternion.identity);
        ReadyPotion.effectDuration=currentPotion.effectDuration; 
        //find correct tag 
        switch (currentPotion)
        {
            case FreezePotion:
            ReadyPotion.tag="IcePotion";
            ReadyPotion.ThrowableRender.material.color=currentPotion.liquidColor;
            break; 
        }
        //launch in player direction
        ReadyPotion.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
    }
}
