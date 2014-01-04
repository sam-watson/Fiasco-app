using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	protected UIButton uiButton;
	protected UILabel uiLabel;
	protected Transform trans;
	
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
		uiLabel = GetComponentInChildren<UILabel>();
		IsEnabled = IsEnabled;
		LabelText = LabelText;
		OnClick = OnClick;
	}
}
