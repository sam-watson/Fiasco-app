using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageMap : MonoBehaviour {
	
	//anchors
	public GameObject head;
	public GameObject body;
	public GameObject foot;
	public GameObject topLeft;
	public GameObject topRight;
	public GameObject bottomLeft;
	public GameObject bottomRight;
	
	public GameObject subPage;
	
	void Awake() {
		var anchors = gameObject.GetComponentsInChildren<UIAnchor>();
		var trans = transform;
		Debug.Log("Adjusting anchors");
		foreach (var anchor in anchors) {
			if (anchor.transform.parent == trans && anchor.gameObject != body) {
				CorrectOffsets(anchor);
			}
		}
	}
	
	public UILabel AddLabel(GameObject labelPrefab, GameObject anchor) {
		var label = NGUITools.AddChild(labelPrefab, anchor).GetComponent<UILabel>();
		if (anchor != body) {
			CorrectOffsets(anchor.GetComponent<UIAnchor>());
		}
		return label;
	}
	
	private void CorrectOffsets(UIAnchor anchor) {
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
	
	public void ClearAll() {}
}

public class PlaysetViewerSubPage {
	
	void Awake() {
		
	}
	
}
