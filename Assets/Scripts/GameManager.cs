using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // scripts
    public static GameManager instance;
    CameraController cameraController;
    private Ghost[] ghosts;

    //UI
    [SerializeField] TMP_Text stopWatchTimer;


    // Class Variables
    bool stopWatchTimerActive = false;
    Rigidbody rb;
    AudioSource audioSource;
    AudioResource clip;
    [SerializeField] float speed;
    int score = 5;
    int health = 3;
    bool isAlive = true;
    bool hasKey = true;
    bool hasWatch = false;
    Renderer rend;
    Color ogColor;

    [SerializeField] List<AudioClip> clips = new List<AudioClip>();


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


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>(); 
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        ogColor = rend.material.color;
        ghosts = FindObjectsByType<Ghost>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (hasWatch)
            {
                StartCoroutine(TemporaryStop());
            }
        }
    }


    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }



    IEnumerator TemporaryStop()
    {
        stopWatchTimerActive = true;

        foreach (Ghost ghost in ghosts)
        {
            ghost.StopMovement();
        }

        stopWatchTimer.gameObject.SetActive(true);
        float countdown = 4f;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            stopWatchTimer.text = countdown.ToString("F1");
            yield return null;
        }
        stopWatchTimer.gameObject.SetActive(false);

        foreach (Ghost ghost in ghosts)
        {
            ghost.ResumeMovement();
        }
        stopWatchTimerActive = false;
    }


    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Bone_Pickup":
                score++;
                Destroy(other.gameObject);
                audioSource.clip = clips[10];
                audioSource.Play();
                break;
            case "Ghost":
                if (isAlive)
                {
                    health--;
                    Debug.Log(health);
                    if (health == 0)
                    {
                        GameOver();
                    }
                    audioSource.clip = clips[6];
                    audioSource.Play();
                }
                break;
            case "Health_Up":
                if (health < 3)
                {
                    health++;
                    Destroy(other.gameObject);
                    audioSource.clip = clips[1];
                    audioSource.Play();
                }
                break;
            case "Death":
                //camera stops following the player if they leave the bounds or fall out
                health = 0;
                GameOver();
                cameraController.followPlayer = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Destroy(gameObject, 2f);
                audioSource.clip = clips[6];
                audioSource.Play();
                break;
            case "Lock":
                if (hasKey)
                {
                    hasKey = false;
                    Destroy(other.gameObject);
                    audioSource.clip = clips[4];
                    audioSource.Play();
                }
                break;
            case "Key":
                hasKey = true;
                Destroy(other.gameObject);
                audioSource.clip = clips[0];
                audioSource.Play();
                break;
            case "Left_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 7f);
                GameObject.Find("Main Camera").transform.position = new Vector3(-135.7f, 24f, 15f);
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(90, 0, 0);
                cameraController.followPlayer = false;
                audioSource.clip = clips[11];
                audioSource.Play();
                break;
            case "Right_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 114f);
                GameObject.Find("Main Camera").transform.position = new Vector3(-135.7f, 24f, 121.71f);
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(90, 0, 0);
                cameraController.followPlayer = false;
                audioSource.clip = clips[11];
                audioSource.Play();
                break;
            case "Left_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(14f, 4f, 56.85f);
                GameObject.Find("Main Camera").transform.position = new Vector3(14f, 12f, 50f);
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(45, 0, 0);
                cameraController.followPlayer = true;
                audioSource.clip = clips[11];
                audioSource.Play();
                break;
            case "Right_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(35f, 4f, 56.8f);
                GameObject.Find("Main Camera").transform.position = new Vector3(35f, 12f, 56.8f);
                GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(45, 0, 0);
                cameraController.followPlayer = true;
                audioSource.clip = clips[11];
                audioSource.Play();
                break;
            case "Tombstone_TP":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-3f, 0.6f, 3.5f);
                break;
            case "Stop_Watch":
                hasWatch = true;
                Destroy(other.gameObject);
                audioSource.clip = clips[7];
                audioSource.Play();
                break;
            case "Bear_Trap":
                audioSource.clip = clips[5];
                audioSource.Play();
                break;
            case "Slime":
                GetComponent<Renderer>().material.color = Color.green;
                Invoke("ResetColor", 4);
                audioSource.clip = clips[2];
                audioSource.Play();
                break;
            case "Next_Level":
                if (score == 5)
                {
                    SceneManager.LoadScene("LevelTwo");
                    gameObject.transform.position = new Vector3(5.97991943f, 0.855957031f, -18.7200012f);
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                else
                {
                    Debug.Log("I Need To Find My Other Bones Before Leaving");
                }
                break;

        }
    }


    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Slime":
                rb.AddForce(-rb.linearVelocity.normalized * 4.8f, ForceMode.Acceleration);
                break;
            case "Spider_Web":
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 8f, rb.linearVelocity.z);
                audioSource.clip = clips[3];
                audioSource.Play();
                break;
        }
    }


    void GameOver()
    {
        isAlive = false;
        speed = 0;
    }

    void ResetColor()
    {
        rend.material.color = ogColor;
    }
}
