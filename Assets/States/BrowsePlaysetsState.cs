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
			PlaysetButton button;
			if ( i < gridTrans.childCount) {
				buttonObj = gridTrans.GetChild(i).gameObject;
				button = buttonObj.GetComponent<PlaysetButton>();
				i++;
			} else {
				buttonObj = (GameObject) Object.Instantiate(context.manager.prefabs.playsetButton);
				button = buttonObj.AddComponent<PlaysetButton>();
				buttonObj.AddComponent<UIDragPanelContents>();
				var buttonTrans = buttonObj.transform;
				buttonTrans.parent = gridTrans;
				buttonTrans.localScale = Vector3.one;
				grid.cellHeight = NGUIMath.CalculateAbsoluteWidgetBounds(buttonTrans).size.y +5;
			}
			button.Playset = playset;
			button.IsEnabled = true;
		}
		i = playsets.Count;
		while ( i < gridTrans.childCount) {
			gridTrans.GetChild(i).GetComponentInChildren<Button>().IsEnabled = false;
			i++;
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
