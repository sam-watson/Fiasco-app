using UnityEngine;
using System.Collections;

public class ElementsSubPage : TableBodySubPage {
	
	void Start() {
		PositionStuff();
	}
	
	private void PositionStuff() {
		var pageFrame = NGUITools.FindInParents<PageMap>(gameObject.transform.parent.gameObject);
		var backButton = pageFrame.GetAnchor(UIAnchor.Side.TopLeft);
		//place header below back button
		head.pixelOffset.y += backButton.pixelOffset.y * 2;
		//place body below header
		body.pixelOffset.y += head.pixelOffset.y * 2;
		table.padding = new Vector2(10, 5);
	}
}