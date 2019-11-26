using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private int x, z, index;
    private GridManager grid;

    public delegate void GridMovement(int index);
    public delegate void Select();
    public delegate void Cancel();

    public GridMovement onGridMovement;
    public Select onSelect;
    public Cancel onCancel;

    public void Start()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public void Update()
    {
        if (onGridMovement != null) OnGridMovement();
        if (onSelect != null) OnSelect();
        if (onCancel != null) OnCancel();
    }

    private void OnGridMovement()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null && hit.transform.tag == "Grid")
            {
                x = Mathf.FloorToInt(hit.point.x / grid.nodeSize);
                z = Mathf.FloorToInt(hit.point.z / grid.nodeSize);

                if (x >= 0 && x < grid.width && z >= 0 && z < grid.height)
                {
                    index = z * grid.width + x;
                    onGridMovement(index);
                }
                
            }
        }
    }

    private void OnSelect()
    {
        if (Input.GetMouseButtonDown(0)) onSelect();
    }

    private void OnCancel()
    {
        if (Input.GetMouseButtonDown(1)) onCancel();
    }

}
