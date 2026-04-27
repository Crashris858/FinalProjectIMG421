using UnityEngine;

[System.Serializable]
public abstract class Potion
{
    public string potionName;
    public string description;
    public QualityLevel quality;
    public Color liquidColor;

    public enum QualityLevel{ Poor, Good, Perfect }

    public Potion(string name, QualityLevel quality)
    {
        this.potionName = name;
        this.quality = quality;
    }

    public abstract void ApplyEffect();
}

public class AntiGravityPotion : Potion
{
    public AntiGravityPotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Grants temporary anti-gravity effect, allowing you to float.";
        this.liquidColor = Color.cyan;
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Anti-Gravity Potion effect.");
    }
}

public class FireResistancePotion : Potion
{
    public FireResistancePotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Grants temporary immunity to fire damage.";
        this.liquidColor = Color.red;
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Fire Resistance Potion effect.");
    }
}

public class SpeedPotion : Potion
{
    public SpeedPotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Increases movement speed for a short duration.";
        this.liquidColor = Color.yellow;
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Speed Potion effect.");
    }
}

public class FreezePotion : Potion
{
    public FreezePotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Can be used to freeze water for a short duration.";
        this.liquidColor = Color.blue;
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Freeze Potion effect.");
    }
}