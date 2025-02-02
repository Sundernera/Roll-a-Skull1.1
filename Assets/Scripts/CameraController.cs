using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    GameManager gameManager;
    [SerializeField] GameObject player;
    public Vector3 offset;
    public bool followPlayer = true;
    Vector3 lastPos;

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
        player = GameObject.Find("Player Variant");
        gameManager = player.GetComponent<GameManager>();
        offset = transform.position - player.transform.position;
        lastPos = transform.position;
    }

    void Update()
    {
        if (followPlayer && player != null)
        {
            transform.position = player.transform.position + offset;
        }
        if (!gameManager.isAlive)
        {
            transform.position = lastPos;
        }
    }
}
