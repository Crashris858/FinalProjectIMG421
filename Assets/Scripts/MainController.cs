using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject mainCamera;
    private PlayerMain _player;
    private PlayerCamera _playerCam;
    private Cauldron _currentCauldron;

    public void Start()
    {
        _player = FindObjectOfType<PlayerMain>();
        _playerCam = mainCamera.GetComponent<PlayerCamera>();
        _currentCauldron = FindObjectOfType<Cauldron>();
    }

    public void StartRhythmGame()
    {
        mainUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _player.canMove = false;
        _playerCam.canMove = false;

        SceneManager.LoadScene("_RhythmGameScene", LoadSceneMode.Additive);
    }

    public void Update()
    {
        if (BrewingData.returning)
        {
            _currentCauldron.CloseCauldronUI();
            mainUI.SetActive(true);

            _playerCam.canMove = true;
            _player.canMove = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            BrewingData.returning = false;
            ShowBrewingResults();
        }
    }

    public void ShowBrewingResults()
    {
        // get the quality
        Potion.QualityLevel quality = (BrewingData.QualityPercent >= 80) ? Potion.QualityLevel.Perfect : 
                                      (BrewingData.QualityPercent >= 60) ? Potion.QualityLevel.Good : 
                                       Potion.QualityLevel.Poor;

        Potion brewedPotion = null;

        // determine potion type based on ingredients
            // NOTE: MIGHT CHANGE HOW INGREDIENTS ARE STORED/REFERENCED LATER 
        if(BrewingData.Slot2 == "Tulip" && BrewingData.Slot3 == "Sugar")
        {
            brewedPotion = new AntiGravityPotion("Anti-Gravity Potion", quality);
        }
        else if(BrewingData.Slot2 == "Tulip" && BrewingData.Slot3 == "Salt")
        {
            brewedPotion = new FireResistancePotion("Fire Resistance Potion", quality);
        }
        else if(BrewingData.Slot2 == "Rose" && BrewingData.Slot3 == "Sugar")
        {
            brewedPotion = new SpeedPotion("Speed Potion", quality);
        }
        else if(BrewingData.Slot2 == "Rose" && BrewingData.Slot3 == "Salt")
        {
            brewedPotion = new FreezePotion("Freeze Potion", quality);
        }
        else
        {
            Debug.Log("STRANGE potion made with: " + BrewingData.Slot2 + " and " + BrewingData.Slot3);
        }

        if(brewedPotion != null)
        {
            Debug.Log($"Brewed {brewedPotion.potionName}");
            Debug.Log($"Quality: {brewedPotion.quality} ({BrewingData.QualityPercent}%)");
            Debug.Log($"Description: {brewedPotion.description}");

            _player.CurrentPotion = brewedPotion;
            if(!string.IsNullOrEmpty(BrewingData.Slot1))
                _player.Inventory.ownedIngredients.Remove(BrewingData.Slot1);
            if(!string.IsNullOrEmpty(BrewingData.Slot2))
                _player.Inventory.ownedIngredients.Remove(BrewingData.Slot2);
            if(!string.IsNullOrEmpty(BrewingData.Slot3))
                _player.Inventory.ownedIngredients.Remove(BrewingData.Slot3);
        }

        // reset brewing data
        BrewingData.Slot1 = "";
        BrewingData.Slot2 = "";
        BrewingData.Slot3 = "";
        BrewingData.QualityPercent = 0;
        GetComponent<IngredientSelector>().ResetSelections();
    }
}
