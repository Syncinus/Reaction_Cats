using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class Sound : MonoBehaviour {

    public AudioClip sound;

	// Update is called once per frame
    public void PlaySound()
    {
            gameObject.AddComponent<AudioSource>();
            AudioSource source = gameObject.GetComponent<AudioSource>();
            source.clip = sound;
            source.PlayOneShot(sound);
            source.playOnAwake = false;
    }
}
