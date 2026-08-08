using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Other")]
    [SerializeField] AudioMixer mixer;

    [Header("Music")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip musicClip;

    [Header("SFX")]
    [SerializeField] AudioSource sFXSource;
    [SerializeField] public AudioClip buttonClip;
    [SerializeField] public AudioClip baahClip;
    [SerializeField] public AudioClip jumpClip;
    [SerializeField] public AudioClip resultClip;
    [SerializeField] public AudioClip invertClip;
    [SerializeField] public AudioClip walkClip; // this one left
    [SerializeField] public AudioClip scoreClip;
    [SerializeField] public AudioClip transitionClip;

    bool playingWalk;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(musicClip);
    }

    private void Update()
    {
        UpdateVolume();
    }

    void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sFXClip)
    {
        if (sFXClip != null)
        {
            sFXSource.PlayOneShot(sFXClip);
        }
    }

    public void PlaySFXPitched(AudioClip sFXClip)
    {
        if (sFXClip != null)
        {
            float ogPitch = sFXSource.pitch;
            sFXSource.pitch = Random.Range(sFXSource.pitch - 1, sFXSource.pitch + 1);

            sFXSource.PlayOneShot(sFXClip);

            sFXSource.pitch = ogPitch;
        }
    }

    public IEnumerator PlaySFXDelayed(AudioClip sFXClip, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (sFXClip != null)
        {
            sFXSource.PlayOneShot(sFXClip);
        }
    }

    public IEnumerator WalkLoop()
    {
        if (!playingWalk)
        {
            playingWalk = true;

            if (walkClip != null)
            {
                PlaySFX(walkClip);
                yield return new WaitForSeconds(walkClip.length);
            }
            else
            {
                yield return new WaitUntil(() => !playingWalk);
            }

            playingWalk = false;
        }
    }

    void UpdateVolume()
    {
        mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat("Master", 1)) * 20);
        mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("Music", 1)) * 20);
        mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFX", 1)) * 20);
    }
}
