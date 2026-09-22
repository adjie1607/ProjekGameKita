using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
    }

    void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        AudioListener.volume = volume;

        volumeSlider.value = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip music)
    {
        bgmSource.clip = music;
        bgmSource.Play();
    }


}
