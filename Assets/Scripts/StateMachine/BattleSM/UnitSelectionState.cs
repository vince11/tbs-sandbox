public class UnitSelectionState : BattleState
{
    public override void OnGridMovement(int index)
    {
        if(index != currentIndex)
        {
            Selector.MoveTo(Grid.nodes[index].worldPos);
            currentIndex = index;

            if (Grid.nodes[index].unit != null)
            {
                UnitHUD.gameObject.SetActive(true);
                UnitHUD.UpdateHUD(Grid.nodes[index].unit);
            }
            else UnitHUD.gameObject.SetActive(false);
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
