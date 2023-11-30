using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioClip[] boatSinkClip;
    [SerializeField] AudioClip[] buyItemClips;
    [SerializeField] AudioClip[] caughtFishClip;

    [SerializeField] AudioClip fishAttachedClip;
    [SerializeField] AudioClip fishingClip;
    [SerializeField] AudioClip fishEscaped;

    [SerializeField] AudioSource themeMusicSoucre;

    public static AudioManager Instance;

    private AudioSource soundEffectSource;

    private bool _playSoundEffect = true;
    private void Awake()
    {
        soundEffectSource = GetComponent<AudioSource>();
        Instance = this;
    }


    public void PlayClip(Clip clip)
    {
        if (!_playSoundEffect) return;
        switch (clip)
        {
            case Clip.Sink:
                StopThemeMusic();
                soundEffectSource.PlayOneShot(boatSinkClip[Random.Range(0, boatSinkClip.Length)]);
                break;
            case Clip.Buy:
                soundEffectSource.PlayOneShot(buyItemClips[Random.Range(0, buyItemClips.Length)]);
                break;
            case Clip.Caught:
                soundEffectSource.PlayOneShot(caughtFishClip[Random.Range(0, caughtFishClip.Length)]);
                break;
            case Clip.Fishing:
                soundEffectSource.PlayOneShot(fishingClip);
                break;
            case Clip.FishTouch:
                if (fishAttachedClip)
                    soundEffectSource.PlayOneShot(fishAttachedClip);
                break;
            case Clip.Escaped:
                if (fishEscaped)
                    soundEffectSource.PlayOneShot(fishEscaped);
                break;
            default: break;
        }
    }

    public void SetMasterVolume(float vol)
    {
        soundEffectSource.volume = vol;
        themeMusicSoucre.volume = vol * .75f;
    }

    public void PlayThemeMusic()
    {
        themeMusicSoucre.Play();
    }

    private void StopThemeMusic()
    {
        themeMusicSoucre.Stop();
    }

    public void ToggleSoundEffect(bool toggleValue)
    {
        _playSoundEffect = toggleValue;
    }

    public void ToggleThemeMusic(bool toggleValueTrue)
    {
        if (toggleValueTrue)
        {
            PlayThemeMusic();
        } else
        {
            StopThemeMusic();
        }
    }
}


public enum Clip
{
    Sink,
    Buy,
    Caught,
    FishTouch,
    Fishing,
    Escaped
}
