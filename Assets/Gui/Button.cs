﻿using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	private UIButton uiButton;
	private UILabel uiLabel;
	private Transform trans;
	
	private EventDelegate _onClick;
	public EventDelegate OnClick {
		get {
			return _onClick;
		}
		set {
			if (_onClick != null) {
				uiButton.onClick.Remove(_onClick);
			}
			_onClick = value;
			uiButton.onClick.Add(_onClick);
		}
	}
	
	private bool _isEnabled = true;
	public bool IsEnabled {
		get {
			return _isEnabled;
		}
		set {
			_isEnabled = value;
			if (uiButton != null) {
				uiButton.isEnabled = value;
			}
		}
	}
	
	private string _labelText;
	public string LabelText {
		get {
			return _labelText;
		}
		set {
			_labelText = value;
			if (uiLabel != null) {
				uiLabel.text = value;
			}
		}
	}
	// A: button spawner and setup class
	//*B: spawn button prefab and add/find button setup class
	
	void Awake() {
		trans = transform;
		trans.localPosition = new Vector3(0, 0, 0);
		trans.localScale = new Vector3(1, 1, 1);
		uiButton = GetComponentInChildren<UIButton>();
		uiLabel = GetComponentInChildren<UILabel>();
		IsEnabled = IsEnabled;
		LabelText = LabelText;
	}
}
