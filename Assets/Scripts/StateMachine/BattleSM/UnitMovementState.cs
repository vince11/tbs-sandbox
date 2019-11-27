using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementState : BattleState
{
    public bool destinationReached;

    public override void Enter()
    {
        destinationReached = false;
        List<Node> path = Grid.GetPath(DestinationNode);
        StartCoroutine(MoveAlongPath(SelectedNode.unit, path));
    }

    public override void Execute()
    {
        if (destinationReached) bsm.ChangeState<ChooseActionState>();
    }

    public override void Exit()
    {

    }

    private IEnumerator MoveAlongPath(Unit unit, List<Node> path)
    {
        int index = 1;
        Vector3 currentDestination = path[index].worldPos;
        currentDestination = new Vector3(currentDestination.x, unit.transform.position.y, currentDestination.z);
        while (index < path.Count)
        {
            unit.transform.position = Vector3.MoveTowards(unit.transform.position, currentDestination, 10f * Time.deltaTime);
            if (unit.transform.position == currentDestination)
            {
                index++;
                if (index < path.Count)
                {
                    currentDestination = path[index].worldPos;
                    currentDestination = new Vector3(currentDestination.x, unit.transform.position.y, currentDestination.z);
                }
            }

            yield return null;
        }

        destinationReached = true;
    }
}
