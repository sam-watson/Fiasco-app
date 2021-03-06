﻿using UnityEngine;
using System.Collections;

public class PlaysetElementsPage : PageMap {

	public override void Start() {
		base.Start();
		PositionStuff();
		SetUpSubPages();
	}
	
	private void PositionStuff() {
		var backButton = GetAnchor(UIAnchor.Side.TopLeft);
		//place header right of back button  (same as PlaysetViewSP)
		head.pixelOffset.x = backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
		head.relativeOffset.x = 0.05f;
	}
	
	private void SetUpSubPages() {
		var grid = body.GetComponentInChildren<UIGrid>();
		var gridTrans = grid.transform;
		while (gridTrans.childCount < 4) {
			NGUITools.AddChild(grid.gameObject, StateManager.Instance.prefabs.elementsSubPage);
		}
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		if (anchor == head) {
			var label = anchor.GetComponentInChildren<UILabel>();
			if (label != null) return label;
		}
		return base.AddLabel (anchor, prefab);
	}
}
