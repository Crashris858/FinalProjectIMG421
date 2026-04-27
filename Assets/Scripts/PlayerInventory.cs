using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> inventory = new List<string>();

    void Awake()
    {
        // temporary for testing
        inventory.Add("Water");
        inventory.Add("Tulip");
        inventory.Add("Rose");
        inventory.Add("Sugar");
        inventory.Add("Salt");
    }
}
