using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetViewSubPage : PageMap {
	
	UITable table;
	
	public override void Awake () {
		base.Awake ();
		table = body.gameObject.GetComponentInChildren<UITable>();
	}
	
	void Start() {
		var pageFrame = NGUITools.FindInParents<PageMap>(gameObject.transform.parent.gameObject);
		Debug.Log(pageFrame);
		var backButton = pageFrame.GetAnchor(UIAnchor.Side.TopLeft);
		head.pixelOffset.x += backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
		body.pixelOffset.y += head.pixelOffset.y *2;
		table.padding = new Vector2(10, 5);
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		GameObject placement;
		if (anchor == body) {
			placement = table.gameObject;
		} else placement = anchor.gameObject;
		var label = NGUITools.AddChild(placement, prefab).GetComponent<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
		if (anchor == body) {
			table.Reposition();
		} else CorrectOffsets(anchor);
		label.name = "a"; //prevent reordering
		return label;
	}
}

