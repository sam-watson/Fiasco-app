using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrowsePlaysetsState : State {
	
	public override void Enter (StateContext context)
	{
		base.Enter (context);
		menuPanel = context.manager.browserPanel;
		menuPanel.SetActive(true);
		//get playset info from service (context)
		var playsets = context.manager.Playsets;
		//generate buttons linking to playset viewer state
		var grid = menuPanel.GetComponentInChildren<UIGrid>();
		var gridTrans = grid.transform;
		int i = 0;
		foreach (Playset playset in playsets) {
			GameObject buttonObj;
			if ( i < gridTrans.childCount) {
				buttonObj = gridTrans.GetChild(i).gameObject;
				i++;
			} else {
				buttonObj = (GameObject) Object.Instantiate(context.manager.prefabs.playsetButton);
			}
			var buttonTrans = buttonObj.transform;
			buttonTrans.parent = gridTrans;
			grid.cellHeight = NGUIMath.CalculateAbsoluteWidgetBounds(buttonTrans).size.y +5;
			buttonObj.AddComponent<UIDragPanelContents>();
			var button = buttonObj.AddComponent<PlaysetButton>();
			button.Playset = playset;
		}
		grid.Reposition();
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
	
	private void GoToViewPlaysetState() {
		new ViewPlaysetState().Enter(new StateContext());
	}
}
