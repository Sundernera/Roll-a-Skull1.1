using UnityEngine;

public class CameraManagerV2 : MonoBehaviour
{
    public static CameraManagerV2 instance;

    GameObject player;
    public bool followPlayer = true;
    Vector3 offset;


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
        player = GameObject.Find("PlayerSkull");
        offset = transform.position - player.transform.position;
    }


    private void Update()
    {
        if (followPlayer)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            transform.position = offset;
        }
    }
}
