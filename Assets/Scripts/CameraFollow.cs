using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float speed = 0.025f;
    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 newPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPos, speed);
    }

}
