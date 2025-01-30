using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Vector3 offset;
    public bool followPlayer = true;

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
