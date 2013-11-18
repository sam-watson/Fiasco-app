using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrowsePlaysetsState : State {
	
	public override void Enter (StateContext context)
	{
		base.Enter (context);
		menuPanel = context.manager.playsetsMenu;
		menuPanel.SetActive(true);
		//generate buttons linking to playset viewer state
		var table = menuPanel.GetComponentInChildren<UITable>();
		//get playset info from service (context)
		foreach (Playset playset in context.playsets.playsets) {
			var buttonObj = (GameObject) Object.Instantiate(context.manager.playsetButton);
			buttonObj.transform.parent = table.transform;
			var button = buttonObj.AddComponent<Button>();
			button.LabelText = playset.name;
			button.OnClick = new EventDelegate(GoToViewPlaysetState);
		}
		table.Reposition();
	}
	
	public override void Exit ()
	{
		base.Exit ();
	}
	
	private void GoToViewPlaysetState() {
		new ViewPlaysetState().Enter(new StateContext());
	}
}
