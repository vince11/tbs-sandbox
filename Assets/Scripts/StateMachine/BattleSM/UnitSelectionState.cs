using UnityEngine;

public class UnitSelectionState : State
{
    private BattleStateMachine bsm;
    private Vector3 mouse;

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
        if (Input.GetMouseButtonDown(0))
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(mouse.x / bsm.grid.nodeSize);
            int y = Mathf.FloorToInt(mouse.y / bsm.grid.nodeSize);
            if(x >= 0 && x < bsm.grid.width && y >= 0 && y < bsm.grid.height)
            {
                int index = y * bsm.grid.width + x;
                bsm.selector.MoveTo(bsm.grid.nodes[index].worldPos);
            }

        }

        //bsm.ChangeState<UnitSelectedState>();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
