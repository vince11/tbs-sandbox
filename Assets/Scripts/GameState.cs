public abstract class GameState
{
    public Selector selector;
    public GridManager gridManager;

    public GameState(Selector selector, GridManager gridManager)
    {
        this.selector = selector;
        this.gridManager = gridManager;
    }

    public abstract GameState HandleInput();
    public abstract void Execute();
}
