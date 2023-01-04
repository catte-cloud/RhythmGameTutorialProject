using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    //THANK YOU CHATGPT I LOVE YOU ???
    
    // The time when the note was instantiated
    double timeInstantiated;
    
    // The time at which the note should be played
    public float assignedTime;
    
    // The length of the note in seconds (for hold notes only)
    public float length;
    
    // Whether the note is a hold note
    public bool isHoldNote;
    
    // Whether the hold note is currently being held
    public bool isHolding;
    
    // The number of sprites to use for the hold note
    public int holdSprites;
    
    // The array of sprites for the hold note
    public Sprite[] holdSpriteArray;
    
    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        
        // Check if the note has passed its assigned time
        if (timeSinceInstantiated > assignedTime)
        {
            // If the note is a hold note
            if (isHoldNote)
            {
                // Calculate how much time has passed since the note should have been played
                float t = (float)((timeSinceInstantiated - assignedTime) / (SongManager.Instance.noteTime * 2));
                
                // Check if the note has passed its end time
                if (t > length)
                {
                    // If the note is not being held, destroy it
                    if (!isHolding)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    // Calculate the index of the sprite to use
                    int spriteIndex = (int)(t / length * holdSprites);
                    
                    // Set the sprite renderer's sprite to the appropriate sprite
                    GetComponent<SpriteRenderer>().sprite = holdSpriteArray[spriteIndex];
                    
                    transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t); 
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                // If the note is not a hold note, destroy it
                Destroy(gameObject);
            }
        }
    }
}
