using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [Header("UI Refs")]
    public GameObject cauldronCanvas;
    public GameObject interactionPrompt;
    public UICauldronManager uiCauldronManager;

    [Header("Player Settings")]
    public PlayerCamera playerCam;
    private bool isPlayerInRange = false;
    private bool isUiOpen = false;

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
        isUiOpen = true;
        cauldronCanvas.SetActive(true);
        interactionPrompt.SetActive(false);

        uiCauldronManager.RefreshUI();

        // disable player control
        playerCam.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseCauldronUI()
    {
        isUiOpen = false;
        cauldronCanvas.SetActive(false);

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
