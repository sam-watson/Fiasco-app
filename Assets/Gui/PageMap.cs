using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageMap : MonoBehaviour {
	
	//anchors
	public UIAnchor head;
	public UIAnchor body;
	public UIAnchor foot;
	
	private List<UIAnchor> anchors = new List<UIAnchor>();
	private Transform trans;
	
	
	public virtual void Awake() {
		trans = transform;
		var allAnchors = gameObject.GetComponentsInChildren<UIAnchor>();
		Debug.Log("Adjusting anchors");
		foreach (var anchor in allAnchors) {
			if (anchor.transform.parent == trans && anchor != body) {
				anchors.Add(anchor);
				CorrectOffsets(anchor);
			}
		}
	}
	
	public virtual UILabel AddLabel(UIAnchor anchor, GameObject prefab) {
		var label = NGUITools.AddChild(anchor.gameObject, prefab).GetComponent<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
		if (anchor != body) {
			CorrectOffsets(anchor);
		}
		return label;
	}
	
	protected void CorrectOffsets(UIAnchor anchor) {
		var bounds = NGUIMath.CalculateRelativeWidgetBounds(anchor.transform);
		var anchorPos = anchor.side.ToString();
		if (anchorPos.Contains("Left")) {
			anchor.pixelOffset.x = bounds.extents.x;
		} else if (anchorPos.Contains("Right")) {
			anchor.pixelOffset.x = -bounds.extents.x;
		}
		if (anchorPos.Contains("Top")) {
			anchor.pixelOffset.y = -bounds.extents.y;
		} else if (anchorPos.Contains("Bottom")) {
			anchor.pixelOffset.y = bounds.extents.y;
		}
	}
	
	public UIAnchor GetAnchor(UIAnchor.Side side) {
		foreach (var anchor in anchors) {
			if (anchor.side == side)
				return anchor;
		}
		UIAnchor sorry = null;
		return sorry;
	}
}