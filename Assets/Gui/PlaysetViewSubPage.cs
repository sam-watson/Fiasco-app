using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetViewSubPage : TableBodySubPage {
	
	void Start() {
		PositionStuff();
	}
	
	private void PositionStuff() {
		var pageFrame = NGUITools.FindInParents<PageMap>(gameObject.transform.parent.gameObject);
		var backButton = pageFrame.GetAnchor(UIAnchor.Side.TopLeft);
		//place header right of back button
		head.pixelOffset.x += backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
		//place body below header
		body.pixelOffset.y += head.pixelOffset.y *2;
		table.padding = new Vector2(10, 5);
	}
}

