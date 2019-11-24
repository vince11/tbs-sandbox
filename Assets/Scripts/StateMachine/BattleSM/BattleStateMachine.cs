using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public void Start()
    {
        ChangeState<UnitSelectionState>();
    }
}
