using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    [System.NonSerialized]
    public GridManager grid;

    [System.NonSerialized]
    public UnitManager unitManager;

    [System.NonSerialized]
    public InputManager inputManager;

    [System.NonSerialized]
    public Node selectedNode;

    [System.NonSerialized]
    public Node destinationNode;

    [System.NonSerialized]
    public ForecastUI forecastUI;

    [System.NonSerialized]
    public CombatManager combatManager;

    public GameObject actionMenu;

    public void Start()
    {
        BattleStart();
    }

    private void BattleStart()
    {
        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();
        inputManager = FindObjectOfType<InputManager>();
        forecastUI = FindObjectOfType<ForecastUI>();
        combatManager = FindObjectOfType<CombatManager>();

        actionMenu.SetActive(false);
        forecastUI.gameObject.SetActive(false);

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
