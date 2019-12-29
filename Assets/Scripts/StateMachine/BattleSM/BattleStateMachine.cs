using System.Collections;
using System.Collections.Generic;

public class BattleStateMachine : StateMachine
{
    [System.NonSerialized]
    public GridManager grid;

    [System.NonSerialized]
    public UnitManager unitManager;

    [System.NonSerialized]
    public InputManager inputManager;

    [System.NonSerialized]
    public CombatManager combatManager;

    [System.NonSerialized]
    public UIManager uiManager;

    [System.NonSerialized]
    public Node selectedNode;

    [System.NonSerialized]
    public Node destinationNode;

    [System.NonSerialized]
    public List<CombatData> combatDatas;

    public void Start()
    {
        BattleStart();
    }

    private void BattleStart()
    {
        grid = FindObjectOfType<GridManager>();
        unitManager = FindObjectOfType<UnitManager>();
        inputManager = FindObjectOfType<InputManager>();
        combatManager = FindObjectOfType<CombatManager>();
        uiManager = FindObjectOfType<UIManager>();
        
        uiManager.HideAllUI();

        grid.PlaceUnits(unitManager.playerUnits);

        ChangeState<UnitSelectionState>();
    }
}
