using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandbookManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject handbookPanel;
    public PlayerInventory inventory;
    public GameObject entryPrefab;

    [Header("Containers")]
    public Transform inventoryContainer;
    public Transform indexContainer;

    private bool isOpen;

    void Start()
{
    if (handbookPanel != null)
    {
        handbookPanel.SetActive(false);
    }
    
    isOpen = false;
}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleHandbook();
        }
    }

    public void ToggleHandbook()
    {
        isOpen = !isOpen;
        handbookPanel.SetActive(isOpen);

        if(isOpen)
        {
            ShowInventoryTab();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            PlayerMain.Instance.canMove = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerMain.Instance.canMove = true;
        }
    }

    public void ShowInventoryTab()
    {
        inventoryContainer.gameObject.SetActive(true);
        indexContainer.gameObject.SetActive(false);
        RefreshInventory();
    }

    public void ShowIndexTab()
    {
        inventoryContainer.gameObject.SetActive(false);
        indexContainer.gameObject.SetActive(true);
        RefreshIndex();
    }

    void RefreshInventory()
    {
        foreach (Transform child in inventoryContainer) Destroy(child.gameObject);

        // Reusing your dictionary logic
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach (string item in inventory.ownedIngredients)
        {
            if (counts.ContainsKey(item)) counts[item]++;
            else counts.Add(item, 1);
        }

        foreach (var entry in counts)
        {
            GameObject obj = Instantiate(entryPrefab, inventoryContainer);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.Key} x{entry.Value}";
        }
    }

    void RefreshIndex()
    {
        foreach (Transform child in indexContainer) Destroy(child.gameObject);

        foreach (string potionName in BrewingData.DiscoveredPotions)
        {
            GameObject obj = Instantiate(entryPrefab, indexContainer);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = potionName;
            
            // logic for clicks go here
        }
    }
}
