using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageMap : MonoBehaviour {
	
	public UIAnchor head;
	public UIAnchor body;
	public UIAnchor foot;
	public UISprite background;
	
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
	
	public virtual GameObject AddContent(UIAnchor anchor, GameObject prefab) {
		var content = NGUITools.AddChild(anchor.gameObject, prefab);
		if (anchor != body) {
			CorrectOffsets(anchor);
		}
		return content;
	}
	
	public virtual UILabel AddLabel(UIAnchor anchor, GameObject prefab) {
		var label = AddContent(anchor, prefab).GetComponent<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
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
	
	/// <summary>
	/// Gets the anchor. Won't return header, footer, or body anchors.
	/// </summary>
	public UIAnchor GetAnchor(UIAnchor.Side side) {
		foreach (var anchor in anchors) {
			if (anchor.side == side && anchor!=head && anchor!=foot)
				return anchor;
		}
		return null;
	}
	
	public virtual Transform GetTrans(UIAnchor angkor) {
		return angkor.transform;
	}
}