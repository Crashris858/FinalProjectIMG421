using UnityEngine;

[System.Serializable]
public abstract class Potion
{
    public string potionName;
    public string description;
    public QualityLevel quality;
    public Color liquidColor;

    public enum QualityLevel{ Poor, Good, Perfect }

    public Potion(string name, QualityLevel quality, Color liquidColor)
    {
        this.potionName = name;
        this.quality = quality;
        this.liquidColor = liquidColor;
    }

    public abstract void ApplyEffect();
}

public class AntiGravityPotion : Potion
{
    public AntiGravityPotion(string name, QualityLevel quality, Color color) : base(name, quality, color)
    {
        this.description = "Grants temporary anti-gravity effect, allowing you to float.";
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Anti-Gravity Potion effect.");
    }
}

public class FireResistancePotion : Potion
{
    public FireResistancePotion(string name, QualityLevel quality, Color color) : base(name, quality, color)
    {
        this.description = "Grants temporary immunity to fire damage.";
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Fire Resistance Potion effect.");
    }
}

public class SpeedPotion : Potion
{
    public SpeedPotion(string name, QualityLevel quality, Color color) : base(name, quality, color)
    {
        this.description = "Increases movement speed for a short duration.";
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Speed Potion effect.");
    }
}

public class FreezePotion : Potion
{
    public FreezePotion(string name, QualityLevel quality, Color color) : base(name, quality, color)
    {
        this.description = "Can be used to freeze water for a short duration.";
    }
    public override void ApplyEffect()
    {
        Debug.Log("Applied Freeze Potion effect.");
    }
}