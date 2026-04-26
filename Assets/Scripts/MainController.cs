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
        Debug.Log($"Brewed potion with: {BrewingData.Slot2} and {BrewingData.Slot3}");
        Debug.Log($"Quality: {BrewingData.QualityPercent}%");

        // reset data
        BrewingData.Slot2 = "";
        BrewingData.Slot3 = "";
        BrewingData.QualityPercent = 0;
        GetComponent<IngredientSelector>().ResetSelections();
    }
}
