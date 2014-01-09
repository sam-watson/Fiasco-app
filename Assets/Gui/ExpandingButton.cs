using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExpandingButton : Button {
	
	private GameObject subPrefab;
	private UITable subTable;
	private List<string> subText;
	private TweenScale tween;
	private Transform tweenTrans;
	
	protected override void Awake ()
	{
		base.Awake ();
		tween = gameObject.GetComponentInChildren<TweenScale>();
		tweenTrans = tween.transform;
		tween.updateTable = true;
		uiLabel.pivot = UIWidget.Pivot.Left;
	}
	
	void Start() {
		OnClick = new EventDelegate(TweenThatThing);
		if (subText != null) {
			SetSubText(subText, subPrefab);
		}
		TweenScale.Begin(tween.gameObject, 0f, new Vector3(1f, 0.01f, 0));
		tween.gameObject.SetActive(false);
	}
	
	public void SetSubText(List<string> textList) {
		SetSubText(textList, StateManager.Instance.prefabs.diceLabel);
	}
	
	public void SetSubText(List<string> textList, GameObject prefab) {
		subText = textList;
		subPrefab = prefab;
		if (tweenTrans != null) {
			subTable = tweenTrans.GetComponentInChildren<UITable>();
			var tableTrans = subTable.transform;
			NumberedLabel subLabel;
			int i = 0;
			foreach (var text in subText) {
				if ( i < tableTrans.childCount) {
					subLabel = tableTrans.GetChild(i).GetComponentInChildren<NumberedLabel>();
				} else {
					subLabel = NGUITools.AddChild(tableTrans.gameObject, subPrefab).GetComponentInChildren<NumberedLabel>();
					subLabel.uiLabel.pivot = UIWidget.Pivot.Left;
				}
				i++;
				subLabel.LabelText = text;
				subLabel.Number = i;
			}
			while ( i < tableTrans.childCount) {
				Debug.Log("culling extra subLabels");
				Object.Destroy(tableTrans.GetChild(i).gameObject);
				i++;
			}
			subTable.Reposition();
		}
	}
	
	public void TweenThatThing() {
		bool alreadyOpen = tween.gameObject.activeSelf;
		float scaleTo = alreadyOpen ? 0.01f : 1f;
		TweenScale.Begin(tweenTrans.gameObject, 0.2f, new Vector3(1f, scaleTo, 0));
		if (alreadyOpen) {
			tween.onFinished.Add(new EventDelegate(TweenToggleOff));
		} else {
			tween.gameObject.SetActive(true);
		}
	}
	
	public void TweenToggleOff() {
		tween.gameObject.SetActive(false);
		tween.onFinished.Clear(); //assumes no other behavior given to tween
	}
}
