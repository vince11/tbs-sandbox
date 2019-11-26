using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public GridManager grid;
    public UnitManager unitManager;

    public void Start()
    {
        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
