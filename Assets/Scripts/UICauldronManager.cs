using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UICauldronManager : MonoBehaviour
{
    [Header("Setup")]
    public PlayerInventory inventory;
    public GameObject buttonPrefab;
    
    [Header("Containers")]
    public Transform containerIn1;
    public Transform containerIn2;

    public void RefreshUI()
    {
        // 1. Clear existing buttons
        foreach (Transform child in containerIn1) Destroy(child.gameObject);
        foreach (Transform child in containerIn2) Destroy(child.gameObject);

        //2. Spawn buttons based on inventory
        foreach (String itemName in inventory.ownedIngredients)
        {
            bool isSlot2 = (itemName == "Tulip" || itemName == "Rose");
            bool isSlot3 = (itemName == "Sugar" || itemName == "Salt");

            Transform target = isSlot2 ? containerIn1 : (isSlot3 ? containerIn2 : null);

            if (target != null)
            {
                GameObject btnObj = Instantiate(buttonPrefab, target);
                btnObj.GetComponentInChildren<TextMeshProUGUI>().text = itemName;

                string capturedName = itemName;
                bool slotRef = isSlot2; 

                btnObj.GetComponent<Button>().onClick.AddListener(() => {
                    SelectIngredient(capturedName, slotRef);
                });
            }
        }
    }

    void SelectIngredient(string name, bool isSlot2)
    {
        if (isSlot2) BrewingData.Slot2 = name;
        else BrewingData.Slot3 = name;

        Debug.Log($"Recipe Update: Slot {(isSlot2 ? "2" : "3")} is now {name}");
    }
}

