using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotbarManager : MonoBehaviour
{
    [Header("UI References")]
    public RectTransform selectionHighlight;
    public HotbarSlot[] slots;

    [Header("Settings")]
    public float moveSpeed = 10f;
    
    private int _currentIndex = 0;
    private Vector3 _targetPosition;

    void Start()
    {
        if (slots.Length > 0)
        {
            selectionHighlight.GetComponent<Image>().enabled = false;
            UpdateSlotVisuals();
        }
    }

    void Update()
    {
        HandleInput();

        selectionHighlight.position = Vector3.Lerp(selectionHighlight.position, _targetPosition, Time.deltaTime * moveSpeed);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(4);
    }

    private void SelectSlot(int index)
    {
        _currentIndex = index;
        _targetPosition = slots[index].rectTransform.position;

        selectionHighlight.GetComponent<Image>().enabled = true;

        // This line tells the Player which potion is now "active"
        PlayerMain.Instance.activeSlotIndex = index; 

        UpdateSlotVisuals();
    }

    public void UpdateSlotVisuals()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Potion potionInSlot = PlayerMain.Instance.potionBelt[i];

            if (potionInSlot != null)
            {
                // We have a potion! Show the icon.
                slots[i].iconImage.color = PlayerMain.Instance.potionBelt[i].liquidColor; 
                
                // Note: You'll eventually want a 'Sprite' property on your Potion class
                // slots[i].iconImage.sprite = potionInSlot.potionIcon; 
            }
            else
            {
                // Empty slot: Hide the icon
                slots[i].iconImage.color = new Color(0, 0, 0, 0); 
            }
        }
    }
}

[System.Serializable]
public class HotbarSlot
{
    public RectTransform rectTransform;
    public Image iconImage;
    public TextMeshProUGUI keyText;
}