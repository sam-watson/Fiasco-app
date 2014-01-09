using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NumberedLabel : MonoBehaviour {
	
	public UILabel numberLabel;
	public UILabel uiLabel;
	
	protected Transform trans;
	
	private int _number;
	public int Number {
		get {
			int n;
			if (numberLabel != null && int.TryParse(numberLabel.text, out n)) {
				_number = n;
			}
			return _number;
		}
		set {
			_number = value;
			if (numberLabel != null) {
				numberLabel.text = _number.ToString() + " ";
			}
		}
	}
	
	private string _labelText;
	public string LabelText {
		get {
			if (uiLabel != null) {
				_labelText = uiLabel.text;
			}
			return _labelText;
		}
		set {
			_labelText = value;
			if (uiLabel != null) {
				uiLabel.text = value;
			}
		}
	}
	
	void Awake() {
		trans = transform;
		trans.localPosition = new Vector3(0, 0, 0);
		trans.localScale = new Vector3(1, 1, 1);
		var labelList = new List<UILabel>(GetComponentsInChildren<UILabel>());
		if (uiLabel == null) {
			uiLabel = labelList.Find(lbl => lbl.transform.parent == trans);
		}
		if (numberLabel == null) { //FIXME: some inefficiency/redundancy in this
			numberLabel = labelList.Find(lbl => lbl.transform.parent != trans);
		}
		LabelText = LabelText;
		Number = Number;
	}
}