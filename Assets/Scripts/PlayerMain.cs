using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static PlayerMain Instance { get; private set; }

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

    [Header("Set in Inspector")]
    public float Speed = 15f;
    public float MaxSpeed = 20f; 
    public float JumpForce=30f; 
    public float PlayerDrag = 5f; 
    public KeyCode JumpButton = KeyCode.Space;
    public Transform Orientation; 
    public float playerHeight; 
    public LayerMask WhatIsGround; 
    private float DetectionDistace = 100f; 
    public Camera CharacterCamera; 


    [Header("Set Dynamically")]
    public bool IsJumping = false; 
    public bool IsRunning; 
    public Rigidbody CharacterRB; 
    public bool grounded = true; 
    public PlayerInventory Inventory; 
    float Vertical;
    float Horizontal;

    [Header("Inventory")]
    public Potion[] potionBelt = new Potion[5]; 
    public int activeSlotIndex = 0;

    public bool canMove = true;


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

        if(Input.GetKeyDown(JumpButton)) IsJumping=true;

        // input: use potion 
        if(Input.GetMouseButtonDown(0))
        {
            Potion activePotion = potionBelt[activeSlotIndex];

            if(activePotion != null)
            {
                activePotion.ApplyEffect(this); 
                potionBelt[activeSlotIndex] = null;

                UIHotbarManager hotbar = FindObjectOfType<UIHotbarManager>();
                if(hotbar != null) hotbar.UpdateSlotVisuals();
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

    public void AddPotionToBelt(Potion newPotion)
    {
        for (int i = 0; i < potionBelt.Length; i++)
        {
            if (potionBelt[i] == null)
            {
                potionBelt[i] = newPotion;
                Debug.Log($"Stored {newPotion.potionName} in slot {i + 1}");
                
                FindObjectOfType<UIHotbarManager>().UpdateSlotVisuals();
                return;
            }
        }
        Debug.Log("Potion belt is full!");
    }
}
