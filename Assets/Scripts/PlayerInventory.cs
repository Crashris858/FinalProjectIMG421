using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> ownedIngredients = new List<string>();

    void Awake()
    {
        // temporary for testing
        ownedIngredients.Add("Water");
        ownedIngredients.Add("Tulip");
        ownedIngredients.Add("Rose");
        ownedIngredients.Add("Sugar");
        ownedIngredients.Add("Salt");
    }
}
