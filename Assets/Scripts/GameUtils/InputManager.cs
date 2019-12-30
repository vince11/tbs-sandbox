using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private int x, z, index;
    private GridManager grid;

    public delegate void MouseMovement(int index);
    public delegate void Select();
    public delegate void Cancel();
    public delegate void Return();
    public delegate void Escape();

    public MouseMovement onMouseMovement;
    public Select onSelect;
    public Cancel onCancel;
    public Return onReturn;
    public Escape onEscape;

    public void Start()
    {
        grid = FindObjectOfType<GridManager>();
    }

    public void Update()
    {
        if (onMouseMovement != null) OnMouseMovement();
        if (onSelect != null && Input.GetMouseButtonDown(0) && index != -1) onSelect();
        if (onCancel != null && Input.GetMouseButtonDown(1)) onCancel();
        if (Input.GetKeyDown(KeyCode.Escape)) onEscape();
        if (Input.GetKeyDown(KeyCode.Return)) onReturn();
    }

    private void OnMouseMovement()
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
                    onMouseMovement(index);
                }

            }
        }
        else index = -1;
    }
}
