public static class IngredientData
{
    public enum Category { Base, Ingredient1, Ingredient2 }

    public static Category GetCategory(string name)
    {
        switch (name)
        {
            case "Water": return Category.Base;
            case "Tulip": 
            case "Rose": return Category.Ingredient1;
            case "Sugar": 
            case "Salt": return Category.Ingredient2;
            default: return Category.Ingredient1;
        }
    }
}
