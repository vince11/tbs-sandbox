using UnityEngine;

public class UnitSelectionState : State
{
    private BattleStateMachine bsm;

    private Vector3 mouse;
    private Ray ray;
    private RaycastHit hit;
    private int x, y, z, index;


    public void Start()
    {
        bsm = FindObjectOfType<BattleStateMachine>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null && hit.transform.tag == "Grid")
            {
                x = Mathf.FloorToInt(hit.point.x / bsm.grid.nodeSize);
                z = Mathf.FloorToInt(hit.point.z / bsm.grid.nodeSize);
                if (x >= 0 && x < bsm.grid.width && z >= 0 && z < bsm.grid.height)
                {
                    index = z * bsm.grid.width + x;
                    bsm.grid.selector.MoveTo(bsm.grid.nodes[index].worldPos);
                }
            }
        }

        if (Input.GetMouseButtonDown(0)) Debug.Log(index);


        //bsm.ChangeState<UnitSelectedState>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void OrthoModeSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            x = Mathf.FloorToInt(mouse.x / bsm.grid.nodeSize);
            y = Mathf.FloorToInt(mouse.y / bsm.grid.nodeSize);
            Debug.Log(mouse);
            if (x >= 0 && x < bsm.grid.width && y >= 0 && y < bsm.grid.height)
            {
                index = y * bsm.grid.width + x;
                bsm.grid.selector.MoveTo(bsm.grid.nodes[index].worldPos);
            }

        }
    }
}
