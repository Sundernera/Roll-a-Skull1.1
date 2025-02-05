using UnityEngine;

public class PlayerManagerV2 : MonoBehaviour
{
    public static PlayerManagerV2 instance;

    Rigidbody rb;
    Renderer rend;
    Color ogColor;
    float speed = 5f;
    public bool isAlive = true;


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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(speed * movement);
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Bone_Pickup":
                GameManagerV2.instance.score++;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                break;
            case "Ghost":
                if (isAlive)
                {
                    GameManagerV2.instance.health--;
                    UIManagerV2.instance.UpdateUI();
                    if (GameManagerV2.instance.health <= 0)
                    {
                        isAlive = false;
                        speed = 0;
                        rb.linearVelocity = Vector3.zero;
                    }
                }
                break;
            case "Health_Up":
                if (GameManagerV2.instance.health < 3)
                {
                    GameManagerV2.instance.health++;
                    Destroy(other.gameObject);
                    UIManagerV2.instance.UpdateUI();
                }
                break;
            case "Death":
                GameManagerV2.instance.health = 0;
                isAlive = false;
                speed = 0;
                rb.linearVelocity = Vector3.zero;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                UIManagerV2.instance.UpdateUI();
                break;
            case "Lock":
                if (GameManagerV2.instance.hasKey)
                {
                    GameManagerV2.instance.hasKey = false;
                    Destroy(other.gameObject);
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
                break;
            case "Left_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 7f);
                CameraManagerV2.instance.followPlayer = false;
                CameraManagerV2.instance.transform.position = new Vector3(-135.7f, 24f, 15f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case "Right_Door_Enter":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-135.3f, 0.6f, 114f);
                CameraManagerV2.instance.followPlayer = false;
                CameraManagerV2.instance.transform.position = new Vector3(-135.7f, 24f, 121.71f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case "Left_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(14f, 4f, 56.85f);
                CameraManagerV2.instance.followPlayer = true;
                CameraManagerV2.instance.transform.position = new Vector3(14f, 12f, 50f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
                break;
            case "Right_Door_Exit":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(35f, 4f, 56.8f);
                CameraManagerV2.instance.followPlayer = true;
                CameraManagerV2.instance.transform.position = new Vector3(35f, 12f, 56.8f);
                CameraManagerV2.instance.transform.rotation = Quaternion.Euler(45, 0, 0);
                break;
            case "Tombstone_TP":
                rb.linearVelocity = Vector3.zero;
                gameObject.transform.position = new Vector3(-3f, 0.6f, 3.5f);
                break;
            case "Stop_Watch":
                GameManagerV2.instance.hasWatch = true;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                break;
            case "Bear_Trap":
                break;
            case "Slime":
                GetComponent<Renderer>().material.color = Color.green;
                rb.AddForce(-rb.linearVelocity.normalized * 50f, ForceMode.Acceleration);
                speed = 2;
                break;
            case "Spider_Web":
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 8f, rb.linearVelocity.z);
                break;
            case "Coffee":
                GameManagerV2.instance.hasCoffee = true;
                Destroy(other.gameObject);
                UIManagerV2.instance.UpdateUI();
                break;
            case "Next_Level":
                GameManagerV2.instance.HandleScenes("LevelOne");
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
}
