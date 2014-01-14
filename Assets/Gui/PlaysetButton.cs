using UnityEngine;
using System.Collections;

public class PlaysetButton : Button {
	
	private Playset _playset;
	public Playset Playset {
		get {
			return _playset;
		}
		set {
			_playset = value;
			LabelText = value.name;
			OnClick = new EventDelegate(GoToPlaysetView);
		}
	}
	
	private void GoToPlaysetView() {
		new PlaysetInfoState().Enter(new StateContext(Playset));
	}
}
