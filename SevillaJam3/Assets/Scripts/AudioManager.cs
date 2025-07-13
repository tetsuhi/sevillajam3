using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    private AudioSource mAudioSource;

    public AudioClip click;
    public AudioClip activarMinijuego;
    public AudioClip empezarMinijuego;
    public AudioClip pato;
    public AudioClip rafagaViento;
    public AudioClip caida;
    public AudioClip colocarCosaBien;
    public AudioClip colocarCosaMal;
    public AudioClip generador;
    public AudioClip cosaBien;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        mAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LoadVolume();
    }

    private void LoadVolume()
    {
        float musicVolume = PlayerPrefs.HasKey("Music") ? PlayerPrefs.GetFloat("Music") : 1f;
        float soundVolume = PlayerPrefs.HasKey("Sound") ? PlayerPrefs.GetFloat("Sound") : 1f;

        musicMixer.SetFloat("Volume", Mathf.Log10(musicVolume) * 20);
        soundMixer.SetFloat("Volume", Mathf.Log10(soundVolume) * 20);
    }

    public void SetMusic(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        musicMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }

    public void SetSound(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1f);
        soundMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Sound", volume);
        PlayerPrefs.Save();
    }

    public void PlayClick()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(click);
    }    
    public void PlayActivarMinijuego()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(activarMinijuego);
    }
    public void PlayEmpezarMinijuego()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(empezarMinijuego);
    }
    public void PlayPato()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(pato);
    }
    public void PlayGenerador()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(generador);
    }
    public void PlayRafagaViento()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(rafagaViento);
    }
    public void PlayColocarCosaBien()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(colocarCosaBien);
    }
    public void PlayCaida()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(caida);
    }
    public void PlayColocarCosaMal()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(colocarCosaMal);
    }
    public void PlayCosaBien()
    {
        mAudioSource.pitch = Random.Range(0.8f, 1.2f);
        mAudioSource.PlayOneShot(cosaBien);
    }
}
