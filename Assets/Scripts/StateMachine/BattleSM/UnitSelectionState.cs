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
                UIManager.unitHUD.SetActive(true);
                UIManager.UpdateUnitHUD(Grid.nodes[index].unit);
            }
            else UIManager.unitHUD.SetActive(false);
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
