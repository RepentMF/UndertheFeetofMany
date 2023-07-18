using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerController : MonoBehaviour
{
    public AudioSource audio;
    public bool volumeChange;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (volumeChange)
        {
            if (audio.volume > 0f)
            {
                audio.volume -= .02f;
            }
            else
            {
                volumeChange = false;
            }
        }
        else
        {
            if (audio.volume <= 0.45f)
            {
                audio.volume += .02f;
            }
        }
    }
}
