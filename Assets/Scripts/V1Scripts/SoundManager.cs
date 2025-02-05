using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] List<AudioClip> backgroundMusic = new List<AudioClip>();
    Scene activeScene;
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        activeScene = SceneManager.GetActiveScene();
        ChangeMusic(0);
    }

    public void ChangeMusic(int i)
    {
        audioSource.clip = backgroundMusic[i];
        audioSource.Play();
    }
}
