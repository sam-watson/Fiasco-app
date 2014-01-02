using UnityEngine;
using System.Collections;

public class ExpandingButton : Button {
	
	private Transform tweener;
	private bool open = false;
	
	void Start() {
		tweener = gameObject.GetComponentInChildren<TweenScale>().transform;
		TweenScale.Begin(tweener.gameObject, 0f, new Vector3(1f, 0, 0));
		OnClick = new EventDelegate(TweenThatThing);
	}
	
	public void TweenThatThing() {
		float scaleTo = open ? 1f : 0;
		open = !open;
		TweenScale.Begin(tweener.gameObject, 0.2f, new Vector3(1f, scaleTo, 0));
	}
}
