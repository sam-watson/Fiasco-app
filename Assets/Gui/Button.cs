using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public UILabel uiLabel;
	
	protected Transform trans;
	protected UIButton uiButton;
	protected UIStretch uiStretch;
	
	private bool screenStretch;
	private StretchType stretchType = StretchType.None;
	private Vector2 stretchValues;
	
	public enum StretchType {
		None,
		Pixel,
		Relative
	}
	
	private EventDelegate _onClick;
	public EventDelegate OnClick {
		get {
			return _onClick;
		}
		set {
			if (_onClick != null && uiButton != null) {
				uiButton.onClick.Remove(_onClick);
			}
			_onClick = value;
			if (uiButton != null) {
				uiButton.onClick.Add(_onClick);
			}
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
	
	// A: button spawner and setup class
	//*B: spawn button prefab and add/find button setup class
	
	protected virtual void Awake() {
		trans = transform;
		trans.localPosition = new Vector3(0, 0, 0);
		trans.localScale = new Vector3(1, 1, 1);
		uiButton = GetComponentInChildren<UIButton>();
		var uiSprite = GetComponentInChildren<UISprite>();
		uiSprite.depth = 1;
		if (uiLabel == null) {
			uiLabel = GetComponentInChildren<UILabel>();
		}
		uiLabel.depth = 5;
		IsEnabled = IsEnabled;
		LabelText = LabelText;
		OnClick = OnClick;
		uiStretch = GetComponentInChildren<UIStretch>();
		if (uiStretch != null) {
			SetStretch(screenStretch, stretchType, stretchValues);
		}
	}
	
	public void SetStretch(bool screen, StretchType offsetType, Vector2 offsetValues) {
		screenStretch = screen;
		stretchType = offsetType;
		stretchValues = offsetValues;
		if (uiStretch == null) return;
		uiStretch.container = screen ? null : uiLabel.gameObject;
		switch (offsetType) {
		case StretchType.Pixel:
			uiStretch.borderPadding = offsetValues;
			uiStretch.relativeSize = Vector2.one;
			break;
		case StretchType.Relative:
			uiStretch.relativeSize = offsetValues;
			break;
		default:
			break;
		}
		return;
	}
}
