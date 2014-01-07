using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElementsSubPage : TableBodySubPage {
	
	private List<ExpandingButton> elementLabels = new List<ExpandingButton>();
	private Dictionary<string, List<string>> elements;
	
	void Start() {
		PositionStuff();
		SetUpContents();
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
	
	private void SetUpContents() {
		var tableTrans = table.transform;
		var elementsPrefab = StateManager.Instance.prefabs.elementsLabel;
		while (tableTrans.childCount < 6) {
			var elementLabel = AddContent(body, elementsPrefab).AddComponent<ExpandingButton>();
			elementLabels.Add(elementLabel);
		}
		SetElements(elements);
	}
	
	public void SetElements(Dictionary<string, List<string>> elements) {
		this.elements = elements;
		int i=0;
		if (elementLabels.Count != 6) return;
		foreach (var elementList in elements) {
			var expLabel = elementLabels[i];
			expLabel.LabelText = elementList.Key;
			expLabel.SetSubText(elementList.Value);
			i++;
		}
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		if (anchor == head) {
			var label = anchor.GetComponentInChildren<UILabel>();
			if (label != null) return label;
		}
		return base.AddLabel (anchor, prefab);
	}
}