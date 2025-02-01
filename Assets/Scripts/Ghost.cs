using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float rotationSpeed = 50;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        rotationSpeed = 0;
    }

    public void ResumeMovement()
    {
        rotationSpeed = 50;
    }
}
