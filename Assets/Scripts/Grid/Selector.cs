using UnityEngine;

public class Selector : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }

    private void FollowMouseOnGrid()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null && hit.transform.tag == "Grid")
            {
                
            }
        }
    }
}
