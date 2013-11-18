using UnityEngine;
using System.Collections;

public class WaitingState : State {

	public override void Enter (StateContext context)
	{
		base.Enter (context);
//		menuPanel = context.manager.logoPanel;
//		menuPanel.SetActive(true);
		context.manager.WaitForIt(2f, new EventDelegate(GoToBrowsePlaysets));
	}
	
	private void GoToBrowsePlaysets() {
		new BrowsePlaysetsState().Enter(new StateContext());
	}
}
