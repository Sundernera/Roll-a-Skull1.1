using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] GameObject player;
    public Vector3 offset;
    public bool followPlayer = true;

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
        offset = transform.position - player.transform.position; 
    }

    void Update()
    {
        if (followPlayer && player != null)
        {
            transform.position = player.transform.position + offset;
        
        }
    }
}
