using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public GameObject mainUI;

    public void StartRhythmGame()
    {
        mainUI.SetActive(false);
        SceneManager.LoadScene("_RhythmGameScene", LoadSceneMode.Additive);
    }

    public void Update()
    {
        if (BrewingData.returning)
        {
            mainUI.SetActive(true);
            BrewingData.returning = false;
            ShowBrewingResults();
        }
    }

    public void ShowBrewingResults()
    {
        //Debug.Log($"Brewed potion with: {BrewingData.Slot2} and {BrewingData.Slot3}");
        //Debug.Log($"Quality: {BrewingData.QualityPercent}%");

        // get the quality
        Potion.QualityLevel quality = (BrewingData.QualityPercent >= 80) ? Potion.QualityLevel.Perfect : 
                                      (BrewingData.QualityPercent >= 60) ? Potion.QualityLevel.Good : 
                                       Potion.QualityLevel.Poor;

        Potion brewedPotion = null;

        // determine potion type based on ingredients
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
            //brewedPotion.ApplyEffect();
        }

        // reset data
        BrewingData.Slot2 = "";
        BrewingData.Slot3 = "";
        BrewingData.QualityPercent = 0;
        GetComponent<IngredientSelector>().ResetSelections();
    }
}
