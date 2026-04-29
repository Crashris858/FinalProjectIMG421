using UnityEngine;

public class IngredientSelector : MonoBehaviour
{

    private string ingredientBase = "";
    private string ingredientA = "";
    private string ingredientB = "";

    public void SelectIngredientBase(string ingredient)
    {
        ingredientBase = ingredient;
        Debug.Log("Base Slot Filled with: " + ingredientBase);
    }

    public void SelectIngredientA(string ingredient)
    {
        ingredientA = ingredient;
        Debug.Log("Slot A Filled with: " + ingredientA);
    }

    public void SelectIngredientB(string ingredient)
    {
        ingredientB = ingredient;
        Debug.Log("Slot B Filled with: " + ingredientB);
    }

    public void StartBrewing()
    {
        if(ingredientBase != "" && ingredientA != "" && ingredientB != "")
        {
            Debug.Log("Starting brewing with: " + ingredientBase + ", " + ingredientA + ", " + ingredientB);
            
            BrewingData.Slot1 = ingredientBase;
            BrewingData.Slot2 = ingredientA;
            BrewingData.Slot3 = ingredientB;

            GetComponent<MainController>().StartRhythmGame();
        }
        else
        {
            Debug.Log("Select both ingredients before brewing.");
        }
    }

    public void ResetSelections()
    {
        ingredientBase = "";
        ingredientA = "";
        ingredientB = "";
        //Debug.Log("Ingredient selections cleared.");
    }
}
