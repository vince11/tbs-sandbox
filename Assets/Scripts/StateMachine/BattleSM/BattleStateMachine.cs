using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public GridManager grid;
    public Selector selector;

    public void Start()
    {
        grid = FindObjectOfType<GridManager>();
        selector = FindObjectOfType<Selector>();
        ChangeState<UnitSelectionState>();
    }
}
