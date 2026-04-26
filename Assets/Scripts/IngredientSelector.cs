using UnityEngine;

public class IngredientSelector : MonoBehaviour
{
    private string ingredientA = "";
    private string ingredientB = "";

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
        if(ingredientA != "" && ingredientB != "")
        {
            Debug.Log("Starting brewing with: " + ingredientA + " and " + ingredientB);
            
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
        ingredientA = "";
        ingredientB = "";
        Debug.Log("Ingredient selections cleared.");
    }
}
