using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public GridManager grid;
    public UnitManager unitManager;
    public InputManager inputManager;

    public Node selectedNode;
    public Node destinationNode;

    public RectTransform battleMenu;

    public void Start()
    {
        BattleStart();
    }

    private void BattleStart()
    {
        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();
        inputManager = FindObjectOfType<InputManager>();

        battleMenu = GameObject.Find("BattleMenu").GetComponent<RectTransform>();
        battleMenu.gameObject.SetActive(false);

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
