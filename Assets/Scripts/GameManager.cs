using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    CameraController cameraController;

    Rigidbody rb;
    [SerializeField] float speed;
    int score = 0;
    int health = 3;
    bool isAlive = true;
    bool hasKey = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>(); 
    }

    void Update()
    {
        GameOver();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bone_Pickup"))
        {
            score++;
            Destroy(other.gameObject);
        }
        if (isAlive && other.CompareTag("Ghost"))
        {
            health--;
            Debug.Log(health);
        }
        if (health < 3 && other.CompareTag("Health_Up"))
        {
            health++;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Death"))
        {
            //camera stops following the player if they leave the bounds or fall out
            health = 0;
            cameraController.followPlayer = false;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(gameObject, 2f);
        }
        if (hasKey && other.CompareTag("Lock"))
        {
            hasKey = false;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Left_Door_Enter"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(-135.3f, 0.6f, 7f);
            GameObject.Find("Main Camera").transform.position = new Vector3(-135.7f, 24f, 15f);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(90, 0, 0);
            cameraController.followPlayer = false;
        }
        if (other.CompareTag("Right_Door_Enter"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(-135.3f, 0.6f, 114f);
            GameObject.Find("Main Camera").transform.position = new Vector3(-135.7f, 24f, 121.71f);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(90, 0, 0);
            cameraController.followPlayer = false;
        }
        if (other.CompareTag("Left_Door_Exit"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(14f, 4f, 56.85f);
            GameObject.Find("Main Camera").transform.position = new Vector3(14f, 12f, 50f);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(45, 0, 0);
            cameraController.followPlayer = true;
        }
        if (other.CompareTag("Right_Door_Exit"))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = new Vector3(35f, 4f, 56.8f);
            GameObject.Find("Main Camera").transform.position = new Vector3(35f, 12f, 56.8f);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(45, 0, 0);
            cameraController.followPlayer = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            rb.AddForce(-rb.linearVelocity.normalized * 4.5f, ForceMode.Acceleration);
        }
        if (other.CompareTag("Spider_Web"))
        {
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 8f, rb.linearVelocity.z);
        }
    }

    void GameOver()
    {
        if (health == 0)
        {
            isAlive = false;
            speed = 0;
        }
    }
}
