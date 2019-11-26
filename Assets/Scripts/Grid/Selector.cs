using UnityEngine;

public class Selector : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
