using UnityEngine;

public class PlayerManagerV2 : MonoBehaviour
{
    public static PlayerManagerV2 instance;

    public Rigidbody rb;
    Renderer rend;
    Color ogColor;
    public float speed = 5f;
    public bool isAlive = true;
    bool isFrozen = false;

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
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        ogColor = rend.material.color;
    }


    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontal, 0, vertical);
            rb.AddForce(speed * movement);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bone_Pickup":
                GameManagerV2.instance.score++;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[3]);
                break;
            case "Ghost":
                if (isAlive)
                {
                    GameManagerV2.instance.health--;
                    UIManagerV2.instance.UpdateUI();
                    SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[2]);
                    GameManagerV2.instance.CheckIfAlive();
                }
                break;
            case "Health_Up":
                if (GameManagerV2.instance.health < 3)
                {
                    GameManagerV2.instance.health++;
                    Destroy(other.gameObject);
                    UIManagerV2.instance.UpdateUI();
                    SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[8]);
                }
                break;
            case "Death":
                GameManagerV2.instance.health = 0;
                isAlive = false;
                speed = 0;
                rb.linearVelocity = Vector3.zero;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                GameManagerV2.instance.CheckIfAlive();
                CameraManagerV2.instance.followPlayer = false;
                UIManagerV2.instance.isGameRunning = false;
                UIManagerV2.instance.UpdateUI();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[13]);
                break;
            case "Lock":
                if (GameManagerV2.instance.hasKey)
                {
                    GameManagerV2.instance.hasKey = false;
                    Destroy(other.gameObject);
                    SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[11]);
                    UIManagerV2.instance.UpdateUI();
                }
                else
                {
                    Debug.Log("I need to find a key");
                }
                break;
            case "Key":
                GameManagerV2.instance.hasKey = true;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[7]);
                break;
            case "Left_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 7f);
                CameraManagerV2.instance.followPlayer = false;
                CameraManagerV2.instance.transform.position = new Vector3(-135.7f, 24f, 15f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[6]);
                break;
            case "Right_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 114f);
                CameraManagerV2.instance.followPlayer = false;
                CameraManagerV2.instance.transform.position = new Vector3(-135.7f, 24f, 121.71f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[6]);
                break;
            case "Left_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(14f, 4f, 56.85f);
                CameraManagerV2.instance.followPlayer = true;
                CameraManagerV2.instance.transform.position = new Vector3(14f, 12f, 50f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[6]);
                break;
            case "Right_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(35f, 4f, 56.8f);
                CameraManagerV2.instance.followPlayer = true;
                CameraManagerV2.instance.transform.position = new Vector3(35f, 12f, 56.8f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[6]);
                break;
            case "Tombstone_TP":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-3f, 0.6f, 3.5f);
                break;
            case "Stop_Watch":
                GameManagerV2.instance.hasWatch = true;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[4]);
                break;
            case "Bear_Trap":
                GameManagerV2.instance.health--;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                UIManagerV2.instance.UpdateUI();
                GameManagerV2.instance.CheckIfAlive();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[5]);
                break;
            case "Slime":
                GetComponent<Renderer>().material.color = Color.green;
                rb.AddForce(-rb.linearVelocity.normalized * 50f, ForceMode.Acceleration);
                speed = 2;
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[9]);
                break;
            case "Spider_Web":
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 8f, rb.linearVelocity.z);
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[10]);
                break;
            case "Coffee":
                GameManagerV2.instance.hasCoffee = true;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                SoundManagerV2.instance.PlaySfX(SoundManagerV2.instance.audioClips[8]);
                break;
            case "Next_Level":
                if (GameManagerV2.instance.score == 5)
                {
                    GameManagerV2.instance.HandleScenes("LevelOne");
                }
                else
                {
                    Debug.Log("I need to find more bones");
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Slime":
                speed = 5f;
                ResetColor();
                break;
        }
    }


    void ResetColor()
    {
        rend.material.color = ogColor;
    }


    public void FreezeMovement()
    {
        isFrozen = true;
    }


    public void UnfreezeMovement()
    {
        isFrozen = false;
    }
}
