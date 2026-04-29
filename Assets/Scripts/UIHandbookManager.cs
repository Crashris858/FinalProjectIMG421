using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIHandbookManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject hotbar;
    public GameObject handbookPanel;
    public PlayerInventory inventory;
    public GameObject entryPrefab;
    public PlayerCamera playerCam;

    [Header("Containers")]
    public Transform inventoryContainer;
    public Transform indexContainer;

    [Header("Tab Visuals")]
    public Image inventoryTabImage;
    public Image indexTabImage;
    public Color activeColor;
    public Color inactiveColor;

    [Header("Potion Detail Panel")]
    public GameObject detailsPanel;
    public TextMeshProUGUI detailsName;
    public TextMeshProUGUI detailsDescription;

    // all the possible potions for the index
    public List<string> allPossiblePotions = new List<string> 
    { 
        "Anti-Gravity Potion", 
        "Fire Resistance Potion", 
        "Speed Potion", 
        "Freeze Potion" 
    };

    private bool isOpen;

    void Start()
    {
        // by default hides the handbook
        if (handbookPanel != null)
        {
            handbookPanel.SetActive(false);
        }
        
        isOpen = false;
    }

    void Update()
    {
        // F to open handbook
        if(Input.GetKeyDown(KeyCode.F))
        {
            ToggleHandbook();
        }
    }

    // opens the handbook UI
    public void ToggleHandbook()
    {
        isOpen = !isOpen;
        handbookPanel.SetActive(isOpen);

        if(isOpen)
        {
            FindObjectOfType<Cauldron>()?.CloseCauldronUI();
            ShowInventoryTab();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            playerCam.canMove = false;
            PlayerMain.Instance.canMove = false;
            hotbar.SetActive(false);
        }
        else
        {
            detailsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerMain.Instance.canMove = true;
            playerCam.canMove = true;
            hotbar.SetActive(true);
        }
    }

    // switches to inventory tab and updates visuals
    public void ShowInventoryTab()
    {
        inventoryContainer.gameObject.SetActive(true);
        indexContainer.gameObject.SetActive(false);
        detailsPanel.SetActive(false);

        inventoryTabImage.color = activeColor;
        indexTabImage.color = inactiveColor;

        RefreshInventory();
    }

    // switches to index tab and updates visuals
    public void ShowIndexTab()
    {
        inventoryContainer.gameObject.SetActive(false);
        indexContainer.gameObject.SetActive(true);

        inventoryTabImage.color = inactiveColor;
        indexTabImage.color = activeColor;

        RefreshIndex();
    }

    // handles the inventory tab
    void RefreshInventory()
    {
        // clears old entries
        foreach (Transform child in inventoryContainer) Destroy(child.gameObject);

        // dictionary that stores (name:count)
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach (string item in inventory.ownedIngredients)
        {
            if (counts.ContainsKey(item)) counts[item]++;
            else counts.Add(item, 1);
        }

        // displays all ingredients with counts inside the inventory
        foreach (var entry in counts)
        {
            GameObject obj = Instantiate(entryPrefab, inventoryContainer);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{entry.Key} x{entry.Value}";
            obj.GetComponent<Button>().enabled = false;
        }
    }

    // handles the index tab
    void RefreshIndex()
    {
        // clears old entries
        foreach (Transform child in indexContainer) Destroy(child.gameObject);

        // displays all possible potions, including undiscovered
        foreach (string potionName in allPossiblePotions)
        {
            GameObject obj = Instantiate(entryPrefab, indexContainer);
            TextMeshProUGUI label = obj.GetComponentInChildren<TextMeshProUGUI>();
            Button btn = obj.GetComponent<Button>();

            // check if the potion has been discovered yet
            if (BrewingData.DiscoveredPotions.Contains(potionName))
            {
                label.text = potionName;
                switch (potionName)
                {
                case "Anti-Gravity Potion":
                label.color=Color.cyan;
                break;
                case  "Fire Resistance Potion":
                label.color=Color.red;
                break;
                case "Speed Potion":
                label.color=Color.yellow;
                break;
                case "Freeze Potion":
                label.color=Color.blue;
                break; 
                };
                btn.interactable = true;
                btn.onClick.AddListener(() => ShowPotionDetails(potionName));
            }
            else
            {
                label.text = "???";
                btn.interactable = false; 
            }
        }
    }

    // gets the name and potion description
    public void ShowPotionDetails(string name)
    {
        detailsPanel.SetActive(true);
        detailsName.text = name;
        detailsDescription.text = GetDescriptionByName(name);
    }

    string GetDescriptionByName(string name)
    {
        if (name.Contains("Anti-Gravity")) return "Grants temporary anti-gravity effect, allowing you to float.";
        if (name.Contains("Speed")) return "Increases movement speed for a short duration.";
        if (name.Contains("Fire Resistance")) return "Grants temporary immunity to fire damage.";
        if (name.Contains("Freeze")) return "Can be used to freeze water for a short duration.";
        return "Unknown potion.";
    }
}
