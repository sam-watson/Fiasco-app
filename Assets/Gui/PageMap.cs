using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageMap : MonoBehaviour {
	
	public UIAnchor head;
	public UIAnchor body;
	public UIAnchor foot;
	public UISprite background;
	public UIPanel framePanel;
	
	private List<UIAnchor> anchors = new List<UIAnchor>();
	private Transform trans;
	
	public virtual void Awake() {
		trans = transform;
	}
	
	public virtual void Start() {
		var frameAnchors = framePanel.GetComponentsInChildren<UIAnchor>(true);
		foreach (var anchor in frameAnchors) {
			anchors.Add(anchor);
			CorrectOffsets(anchor);
		}
		AdjustDepths();
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
	
	public void AdjustDepths() {
		/* Frame buttons should be in front of all else - depth 5
		 * Headers and Footers at 3 or 4
		 * Backgrounds at -1
		 * Body and scrolling contents at 0 to 2
		 * 
		 * A)) Redesign scene to have widget sets separated by panels
		 * 		- Frame elements, bg, header and footer, body
		 */
		NGUITools.PushBack(background.gameObject);
		NGUITools.BringForward(framePanel.gameObject);
		var parentPage = NGUITools.FindInParents<PageMap>(trans.parent.gameObject);
		if (parentPage != null) {
			parentPage.AdjustDepths();
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