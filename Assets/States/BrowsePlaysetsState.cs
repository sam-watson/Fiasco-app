using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrowsePlaysetsState : State {
	
	public override void Enter (StateContext context)
	{
		base.Enter (context);
		menuPanel = context.manager.browserPanel;
		menuPanel.SetActive(true);
		//generate buttons linking to playset viewer state
		var grid = menuPanel.GetComponentInChildren<UIGrid>();
		//get playset info from service (context)
		foreach (Playset playset in context.manager.Playsets) {
			var buttonObj = (GameObject) Object.Instantiate(context.manager.prefabs.playsetButton);
			var buttonTrans = buttonObj.transform;
			buttonTrans.parent = grid.transform;
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
