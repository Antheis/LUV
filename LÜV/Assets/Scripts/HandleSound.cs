using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSound : MonoBehaviour
{
    public Button sound_button;
    public Button music_button;

    public AudioSource sound;
    public AudioSource music;

    public Sprite sound_sprite;
    public Sprite nosound_sprite;
    public Sprite music_sprite;
    public Sprite nomusic_sprite;

    private bool toggle_sound = true;
    private bool toggle_music = true;

    public void toggleSound()
    {
        toggle_sound = !toggle_sound;
        sound_button.GetComponent<Image>().sprite = toggle_sound ? sound_sprite : nosound_sprite;
        sound.volume = toggle_sound ? 1.0f : 0.0f;
    }

    public void toggleMusic()
    {
        toggle_music = !toggle_music;
        music_button.GetComponent<Image>().sprite = toggle_music ? music_sprite : nomusic_sprite;
        music.volume = toggle_music ? 1.0f : 0.0f;
    }
}
