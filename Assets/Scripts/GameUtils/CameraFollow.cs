using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public Transform target;

    public float speed = 0.025f;
    public Vector3 offset;

    private GridManager grid;

    public void Start()
    {
        grid = FindObjectOfType<GridManager>();
    }

    void FixedUpdate()
    {
        Vector3 newPos = grid.selector.transform.position + offset;
        //clip camera position to see at least 4 tiles up front
        newPos.z = Mathf.Clamp(newPos.z, offset.z, (grid.height * grid.nodeSize) - offset.z - grid.nodeSize * 4);
        transform.position = Vector3.Lerp(transform.position, newPos, speed);
    }

}
