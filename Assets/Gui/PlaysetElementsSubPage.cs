﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetElementsSubPage : ScrollBodySubPage {
	
	private List<ExpandingButton> elementLabels = new List<ExpandingButton>();
	private Dictionary<string, List<string>> elements;
	
	public override void Start() {
		base.Start();
		PositionStuff();
		SetUpContents();
	}
	
	private void PositionStuff() {
		var backButton = parentPage.GetAnchor(UIAnchor.Side.TopLeft);
		//place header below back button
		head.pixelOffset.y = backButton.pixelOffset.y * 2.5f;
		head.relativeOffset.x = -0.05f;
		table.padding = new Vector2(10, 5);
		table.sorted = true;
		SetUpScrollPanel(Mathf.Abs(backButton.pixelOffset.y * 2f));
	}
	
	private void SetUpContents() {
		var tableTrans = table.transform;
		var elementsPrefab = StateManager.Instance.prefabs.elementsLabel;
		while (tableTrans.childCount < 6) {
			var elementLabel = AddContent(body, elementsPrefab).GetComponent<ExpandingButton>();
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
			var expLabelLabel = expLabel.GetComponentInChildren<NumberedLabel>();
			expLabelLabel.LabelText = elementList.Key;
			expLabelLabel.Number = ++i; //increment
			expLabel.name = i.ToString();
			expLabel.SetSubText(elementList.Value);
			expLabel.SetStretch(true, Button.StretchType.Pixel, table.padding*2f);
		}
		ScrollToTop();
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		if (anchor == head) {
			var label = anchor.GetComponentInChildren<UILabel>();
			if (label == null) {
				label = base.AddLabel (anchor, prefab);
				label.pivot = UIWidget.Pivot.Right;
				label.transform.localPosition = Vector3.zero;
			}
			return label;
		}
		return base.AddLabel(anchor, prefab);
	}
}