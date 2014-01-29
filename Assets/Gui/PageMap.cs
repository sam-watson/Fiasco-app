using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageMap : MonoBehaviour {
	
	public UIAnchor head;
	public UIAnchor body;
	public UIAnchor foot;
	public UISprite background;
	public UIPanel backPanel;
	public UIPanel forePanel;
	
	protected PageMap parentPage;
	//private List<PageMap> childPages = new List<PageMap>();	//needed?
	protected Transform trans;
	
	public virtual void Awake() {
		trans = transform;
	}
	
	public virtual void Start() {
		parentPage = NGUITools.FindInParents<PageMap>(trans.parent.gameObject);
		if (parentPage == null) {
			Debug.Log(gameObject.name + " has NO parent page");
		} else Debug.Log(gameObject.name + " has parent page " + parentPage.name);
		if (background == null) {
			background = GetAnchor(UIAnchor.Side.Center, backPanel.gameObject).GetComponent<UISprite>();
		}
		CorrectAllOffsets(backPanel.gameObject);
		CorrectAllOffsets(forePanel.gameObject);
		AdjustDepths();
	}
	
	public virtual GameObject AddContent(UIAnchor anchor, GameObject prefab) {
		var content = NGUITools.AddChild(anchor.gameObject, prefab);
		if (anchor == GetAnchor(anchor.side)) {
			CorrectOffsets(anchor);
		}
		return content;
	}
	
	public virtual UILabel AddLabel(UIAnchor anchor, GameObject prefab) {
		var label = AddContent(anchor, prefab).GetComponentInChildren<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
		label.transform.localPosition = Vector3.zero;
		return label;
	}
	
	protected void CorrectAllOffsets(GameObject rootObject) {
		foreach (var anchor in rootObject.GetComponentsInChildren<UIAnchor>()) {
			if (anchor.transform.parent.gameObject == rootObject) {
				CorrectOffsets(anchor);
			}
		}
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
		/* Frame buttons et al should be in front of all else
		 * Backgrounds behind
		 * Body and scrolling contents somewhere between
		 */
		NGUITools.PushBack(backPanel.gameObject);
		NGUITools.BringForward(forePanel.gameObject);
		if (parentPage != null) {
			parentPage.AdjustDepths();
		}
	}
	
	public UIAnchor GetAnchor(UIAnchor.Side side) {
		return GetAnchor(side, this.forePanel.gameObject);
	}
	
	/// <summary>
	/// Gets the first relevant anchor directly beneath game object.
	/// </summary>
	public UIAnchor GetAnchor(UIAnchor.Side side, GameObject go) {
		var anchors = go.GetComponentsInChildren<UIAnchor>();
		foreach (var anchor in anchors) {
			if (anchor.side == side
				&& anchor.transform.parent.gameObject == go) {
				return anchor;
			}
		}
		return null;
	}
	
	public virtual Transform GetTrans(UIAnchor angkor) {
		return angkor.transform;
	}
}