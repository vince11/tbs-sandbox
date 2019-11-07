using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private GameState currentState;

    public void Start()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        Selector selector = FindObjectOfType<Selector>();

        currentState = new SelectionState(selector, gridManager);

    }

    public void Update()
    {
        if (currentState != null)
        {
            HandleInput();
            Execute();
        }
    }

    private void HandleInput()
    {
        GameState state = currentState.HandleInput();
        if (state != null)
        {
            currentState = state;
        }
    }

    private void Execute()
    {
        currentState.Execute();
    }
}
