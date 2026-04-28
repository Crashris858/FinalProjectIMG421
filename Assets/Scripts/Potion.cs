using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class Potion
{
    public string potionName;
    public string description;
    public float effectDuration = 10f; 
    public QualityLevel quality;
    public Color liquidColor; 

    public enum QualityLevel{ Poor, Good, Perfect }

    public Potion(string name, QualityLevel quality)
    {
        this.potionName = name;
        this.quality = quality;
    }

    public abstract void ApplyEffect(PlayerMain player);

}

public class AntiGravityPotion : Potion
{
    public AntiGravityPotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Grants temporary anti-gravity effect, allowing you to float.";
        this.liquidColor = Color.cyan;
    }
    public override void ApplyEffect(PlayerMain player)
    {
        Debug.Log("Applied Anti-Gravity Potion effect.");
        if (player != null)
        {
            // Turn off player gravity 
            player.CharacterRB.useGravity = false;
            //apply an upward velocity for "anti-gravity effect" 
            player.CharacterRB.AddForce(Vector3.up*player.JumpForce, ForceMode.VelocityChange);
            //run coroutine (must pass something with monobehaviour for time)
            player.StartCoroutine(RemoveEffect(player));
        }
        
    }
    public IEnumerator RemoveEffect(PlayerMain player)
    {
        if (player != null)
        {
            //wait for effect dureaiton 
            yield return new WaitForSeconds(effectDuration);
            // Remove the anti-gravity effect from the player
            player.CharacterRB.useGravity=true; 
        }
    }
}

public class FireResistancePotion : Potion
{
    public FireResistancePotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Grants temporary immunity to fire damage.";
        this.liquidColor = Color.red;
    }
    public override void ApplyEffect(PlayerMain player)
    {
        Debug.Log("Applied Fire Resistance Potion effect.");
    }
    public IEnumerator RemoveEffect(PlayerMain player)
    {
        if (player != null)
        {
            //wait for effect dureaiton 
            yield return new WaitForSeconds(effectDuration);
        
        }
    }
}

public class SpeedPotion : Potion
{
    public float speedBoost = 3f; 
    public SpeedPotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Increases movement speed for a short duration.";
        this.liquidColor = Color.yellow;
    }
    public override void ApplyEffect(PlayerMain player)
    {
        if(player!=null)
        {
            //apply speed boost 
            player.Speed*=speedBoost; 
            //start counter routine 
            player.StartCoroutine(RemoveEffect(player));

        }
    }
    public IEnumerator RemoveEffect(PlayerMain player)
    {
        if (player != null)
        {
            //await duration 
            yield return new WaitForSeconds(effectDuration);
            //reset
            player.Speed/=speedBoost; 
        }
    }
}

public class FreezePotion : Potion
{
    public FreezePotion(string name, QualityLevel quality) : base(name, quality)
    {
        this.description = "Can be used to freeze water for a short duration.";
        this.liquidColor = Color.blue;
    }
    public override void ApplyEffect(PlayerMain player)
    {
        Debug.Log("Applied Freeze Potion effect.");
    }
    public IEnumerator RemoveEffect(PlayerMain player)
    {
        if (player != null)
        {
            //wait for effect dureaiton 
            yield return new WaitForSeconds(effectDuration);

        }
    }
}