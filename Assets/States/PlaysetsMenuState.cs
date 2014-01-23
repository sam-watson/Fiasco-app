using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetsMenuState : State {
	
	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.browserPanel);
		menuPanel.SetActive(true);
		var playsets = context.manager.Playsets;
		//generate buttons linking to playset viewer states
		var grid = pageMap.body.GetComponentInChildren<UIGrid>();
		var gridTrans = grid.transform;
		int i;
		for (i=0; i < playsets.Count; i++) {
			GameObject buttonObj;
			PlaysetButton button;
			if ( i < gridTrans.childCount) {
				buttonObj = gridTrans.GetChild(i).gameObject;
				button = buttonObj.GetComponent<PlaysetButton>();
			} else {
				buttonObj = (GameObject) Object.Instantiate(context.manager.prefabs.playsetButton);
				button = buttonObj.AddComponent<PlaysetButton>();
				buttonObj.AddComponent<UIDragPanelContents>();
				var buttonTrans = buttonObj.transform;
				buttonTrans.parent = gridTrans;
				buttonTrans.localScale = Vector3.one;
				grid.cellHeight = NGUIMath.CalculateRelativeWidgetBounds(buttonTrans).size.y +5;
			}
			button.Playset = playsets[playsets.Count-i-1];
			button.IsEnabled = true;
		}
		//i = playsets.Count;
		while ( i < gridTrans.childCount) {
			gridTrans.GetChild(i).GetComponentInChildren<Button>().IsEnabled = false;
			i++;
		}
		grid.Reposition();
	}
}