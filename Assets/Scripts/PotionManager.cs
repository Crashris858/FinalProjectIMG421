using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public int totalNotes;
    private int notesHit;
    private int notesMissed;

    public void Hit()
    {
        notesHit++;
        CheckPotionStatus();
    }

    public void Miss()
    {
        notesMissed++;
        CheckPotionStatus();
    }

    void CheckPotionStatus()
    {
        if(notesHit + notesMissed >= totalNotes)
        {
            CalculateQuality();
        }
    }

    void CalculateQuality()
    {
        float percentHit = ((float)notesHit / totalNotes) * 100;
        if(percentHit >= 80)
        {
            Debug.Log("Potion Quality: Perfect (" + percentHit + "%)");
        }
        else if(percentHit >= 60)
        {
            Debug.Log("Potion Quality: Good (" + percentHit + "%)");
        }
        else
        {
            Debug.Log("Potion Quality: Poor (" + percentHit + "%)");
        }
    }
}
