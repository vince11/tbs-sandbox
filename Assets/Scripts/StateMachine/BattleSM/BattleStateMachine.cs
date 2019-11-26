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
        StartCoroutine(BattleStart(.5f));
    }

    private IEnumerator BattleStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();
        inputManager = FindObjectOfType<InputManager>();

        battleMenu = GameObject.Find("BattleMenu").GetComponent<RectTransform>();
        battleMenu.gameObject.SetActive(false);

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
