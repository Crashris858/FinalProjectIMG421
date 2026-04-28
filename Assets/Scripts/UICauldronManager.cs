using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UICauldronManager : MonoBehaviour
{
    [Header("Setup")]
    public PlayerInventory inventory;
    public GameObject buttonPrefab;
    public Button startButton;
    
    [Header("Containers")]
    public Transform containerIn1;
    public Transform containerIn2;
    public Transform containerIn3;

    public void Start()
    {
        startButton.interactable = false;
    }

    public void RefreshUI()
    {
        // 1. Clear existing buttons
        foreach (Transform child in containerIn1) Destroy(child.gameObject);
        foreach (Transform child in containerIn2) Destroy(child.gameObject);
        foreach (Transform child in containerIn3) Destroy(child.gameObject);

        // stores the name and counts of each ingredient
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach(string item in inventory.ownedIngredients)
        {
            if(counts.ContainsKey(item)) counts[item]++;
            else counts.Add(item, 1);
        }

        foreach(var entry in counts)
        {
            string itemName = entry.Key;
            int itemCount = entry.Value;

            int slotIndex = GetIngredientSlot(itemName);
            Transform target = GetTargetContainer(slotIndex);

            if(target != null)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, target);
                buttonObj.transform.localScale = Vector3.one;

                string displayLabel = itemCount > 1 ? $"{itemName} (x{itemCount})" : itemName;
                buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = displayLabel;

                string capturedName = itemName;
                int capturedSlot = slotIndex;

                buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectIngredient(capturedName, capturedSlot));
            }
        }
    }

    private int GetIngredientSlot(string name)
    {
        if(name == "Water") return 1;
        if(name == "Tulip" || name == "Rose") return 2;
        if(name == "Sugar" || name == "Salt") return 3;
        return 0;
    }

    private Transform GetTargetContainer(int slot)
    {
        if(slot == 1) return containerIn1;
        if(slot== 2) return containerIn2;
        if(slot == 3) return containerIn3;
        return null;
    }

    void SelectIngredient(string name, int slotNumber)
    {
        if (slotNumber == 1) BrewingData.Slot1 = name;
        else if (slotNumber == 2) BrewingData.Slot2 = name;
        else if (slotNumber == 3) BrewingData.Slot3 = name;

        //Debug.Log($"Recipe Update: Slot {slotNumber} is now {name}.");
        ValidateRecipe();
    }

    void ValidateRecipe()
    {
        bool isValid = !string.IsNullOrEmpty(BrewingData.Slot1) && 
                       !string.IsNullOrEmpty(BrewingData.Slot2) && 
                       !string.IsNullOrEmpty(BrewingData.Slot3);

        startButton.interactable = isValid;
    }
}

