public class UnitSelectionState : BattleState
{
    public override void OnMouseMovement(int index)
    {
        if (index != Selector.index)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            Selector.index = index;

            DisplayUnitHUD(Grid.nodes[index].unit);
        }
    }

    public override void OnSelect()
    {
        if(Grid.nodes[Selector.index].unit != null)
        {
            SelectedNode = Grid.nodes[Selector.index];
            bsm.ChangeState<UnitSelectedState>();
        }
    }
}
