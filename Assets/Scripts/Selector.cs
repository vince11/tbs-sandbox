using UnityEngine;

public class Selector : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private GridManager gridManager;

    public void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void Update()
    {
        //FollowMouseOnGrid();
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
