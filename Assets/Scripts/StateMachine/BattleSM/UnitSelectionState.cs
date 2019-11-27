public class UnitSelectionState : BattleState
{
    public override void OnGridMovement(int index)
    {
        if(index != currentIndex)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            currentIndex = index;
        }
        
    }

    public override void OnSelect()
    {
        if(Grid.nodes[currentIndex].unit != null)
        {
            SelectedNode = Grid.nodes[currentIndex];
            bsm.ChangeState<UnitSelectedState>();
        }
    }
}
