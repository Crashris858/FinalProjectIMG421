using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<String> ownedIngredients = new List<String>();
    public AudioSource ItemCollected;

    //func: add item 
    //desc: adds item to inventory data structure
    public void AddItem(ItemData CurrentItem)
    {
        ownedIngredients.Add(CurrentItem.ItemName);
        ItemCollected.Play();
    }
}
