using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
