using UnityEngine;

public class Selector : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private GridManager gridManager;

    private GameObject currentNodeGO;

    public void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void Update()
    {
        FollowMouseOnGrid();
    }

    private void FollowMouseOnGrid()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null && hit.transform.tag == "Node")
            {
                if (currentNodeGO != hit.transform.gameObject)
                {
                    if(currentNodeGO != null) gridManager.nodesMaterial[currentNodeGO].color = Color.white;
                    currentNodeGO = hit.transform.gameObject;
                    gridManager.nodesMaterial[currentNodeGO].color = Color.red;
                    transform.localPosition = new Vector3(currentNodeGO.transform.localPosition.x, transform.localPosition.y, currentNodeGO.transform.localPosition.z);
                }
                
            }
        }
    }
}
