using UnityEngine;

public class SoundManagerV2 : MonoBehaviour
{
    public static SoundManagerV2 instance;

    [SerializeField] AudioSource music;
    [SerializeField] AudioSource sounds;
    [SerializeField] public AudioClip[] audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        PlayMusic(audioClips[0]);
    }


    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.loop = true;
        music.Play();
    }


    public void PlaySfX(AudioClip clip)
    {
        sounds.PlayOneShot(clip);
    }
}
