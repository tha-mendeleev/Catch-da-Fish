using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioClip[] boatSinkClip;
    [SerializeField] AudioClip[] buyItemClips;
    [SerializeField] AudioClip[] caughtFishClip;

    [SerializeField] AudioClip fishAttachedClip;
    [SerializeField] AudioClip fishingClip;
    [SerializeField] AudioClip fishEscaped;
    public static AudioManager Instance;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlayClip(Clip clip)
    {
        switch (clip)
        {
            case Clip.Sink:
                audioSource.PlayOneShot(boatSinkClip[Random.Range(0, boatSinkClip.Length)]);
                break;
            case Clip.Buy:
                audioSource.PlayOneShot(buyItemClips[Random.Range(0, buyItemClips.Length)]);
                break;
            case Clip.Caught:
                audioSource.PlayOneShot(caughtFishClip[Random.Range(0, caughtFishClip.Length)]);
                break;
            case Clip.Fishing:
                audioSource.PlayOneShot(fishingClip);
                break;
            case Clip.FishTouch:
                if (fishAttachedClip)
                    audioSource.PlayOneShot(fishAttachedClip);
                break;
            case Clip.Escaped:
                if (fishEscaped)
                    audioSource.PlayOneShot(fishEscaped);
                break;
            default: break;
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
