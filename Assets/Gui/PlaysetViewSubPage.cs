using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetViewSubPage : TableBodySubPage {
	
	public override void Start() {
		base.Start();
		PositionStuff();
	}
	
	private void PositionStuff() {
		var backButton = parentPage.GetAnchor(UIAnchor.Side.TopLeft);
		//place header right of back button
		head.pixelOffset.x += backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
		//place body below header
		body.pixelOffset.y += head.pixelOffset.y *2;
		table.padding = new Vector2(10, 5);
	}
}

