using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine.Timeline;

public class PotionManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public TextMeshProUGUI countdownText;
    private int totalNotes;
    private int notesHit = 0;
    private int notesProcessed = 0;
    
    public void Awake()
    {
        // automatically count notes from the timeline
        var timelineAsset = timeline.playableAsset as TimelineAsset;
        totalNotes = 0;

        foreach (var track in timelineAsset.GetOutputTracks())
        {
            foreach (var marker in track.GetMarkers())
            {
                if (marker is SignalEmitter) totalNotes++;
            }
        }

        //Debug.Log("Total Notes: " + totalNotes);
    }

    public void Start()
    {
        timeline.Stop();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO!";
        
        timeline.Play();
        
        yield return new WaitForSeconds(1);
        countdownText.text = "";
    }

    public void Hit()
    {
        notesHit++;
        noteProcessed();
    }

    public void Miss()
    {
        noteProcessed();
    }

    void noteProcessed()
    {
        notesProcessed++;

        // only calculate quality after all notes have been processed to avoid premature results
        if(notesProcessed >= totalNotes)
        {
            CalculateQuality();
        }
    }

    void CalculateQuality()
    {
        float percentHit = (float)notesHit / totalNotes * 100;
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
