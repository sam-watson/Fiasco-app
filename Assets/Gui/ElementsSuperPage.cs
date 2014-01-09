﻿using UnityEngine;
using System.Collections;

public class ElementsSuperPage : PageMap {

	void Start() {
		PositionStuff();
		SetUpSubPages();
	}
	
	private void PositionStuff() {
		var backButton = GetAnchor(UIAnchor.Side.TopLeft);
		//place header right of back button  (same as PlaysetViewSP)
		head.pixelOffset.x += backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
	}
	
	private void SetUpSubPages() {
		var grid = body.GetComponentInChildren<UIGrid>();
		var gridTrans = grid.transform;
		while (gridTrans.childCount < 4) {
			NGUITools.AddChild(grid.gameObject, StateManager.Instance.prefabs.elementsSubPage);
		}
	}
}