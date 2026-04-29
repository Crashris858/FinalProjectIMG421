using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject cauldronCanvas;
    public GameObject interactionPrompt;
    public UICauldronManager uiCauldronManager;
    public GameObject hotbar;

    [Header("Player Settings")]
    public PlayerCamera playerCam;
    private bool isPlayerInRange = false;
    private bool isUiOpen = false;

    [Header("Audio")]
    public AudioSource cauldronBubbling;

    void Start()
    {
        cauldronBubbling = GetComponent<AudioSource>();
        
        // defaults to hide UI and prompt
        isPlayerInRange = false;
        isUiOpen = false;
        
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
            
        if (cauldronCanvas != null)
            cauldronCanvas.SetActive(false);
    }

    void Update()
    {
        // check in range of cauldron
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isUiOpen) OpenCauldronUI();
            else CloseCauldronUI();
        }

        // ESC to close UI
        if (isUiOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCauldronUI();
        }
    }

    public void OpenCauldronUI()
    {
        cauldronBubbling.Play();
        isUiOpen = true;
        cauldronCanvas.SetActive(true);
        interactionPrompt.SetActive(false);

        UIHandbookManager handbook = FindObjectOfType<UIHandbookManager>();
        if(handbook != null && handbook.handbookPanel.activeSelf)
        {
            handbook.ToggleHandbook(); 
        }

        uiCauldronManager.RefreshUI();
        hotbar.SetActive(false);

        // disable player controls
        playerCam.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCauldronUI()
    {
        cauldronBubbling.Stop();
        isUiOpen = false;
        cauldronCanvas.SetActive(false);
        hotbar.SetActive(true);

        if (isPlayerInRange) interactionPrompt.SetActive(true);

        // return player control
        playerCam.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    // ensures prompt and UI are hidden when player leaves range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionPrompt.SetActive(false);
            CloseCauldronUI(); 
        }
    }
}
