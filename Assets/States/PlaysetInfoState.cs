using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetInfoState : State {
	
	public Playset queuedPlayset;
	
	private static TweenPosition playsetPanel;
	private static Dictionary<string, PlaysetInfoSubPage> subPages;
	private static bool setUp = false;

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.viewerPanel);
		var playset = context.playset;
		var playsets = context.manager.Playsets;
		var viewIndex = playsets.IndexOf(playset);
		Debug.Log("Starting "+ playset.name + "view-state, index " + viewIndex);
		SetUpButtons();
		if (!setUp) {
			SetUpPlaysets(context); }
		var curPos = new Vector3(-viewIndex*Screen.width, 0, 0);
		TweenPosition.Begin(playsetPanel.gameObject, 0, curPos);
		ShowInfo(subPages[playset.name], true, 0.5f);
	}
	
	public override void Exit ()
	{
		ShowInfo(subPages[initialContext.playset.name], false, 0);
		base.Exit ();
	}
	
	private void SetUpButtons() {
		//FIXME: reset by new states, but still probably not good to have persistent elements referencing this object
		var ank = pageMap.GetAnchor(UIAnchor.Side.TopLeft);
		var but = ank.GetComponentInChildren<Button>();
		but.OnClick = new EventDelegate(Back);
		pageMap.GetAnchor(UIAnchor.Side.BottomLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(PrevPlayset);
		pageMap.GetAnchor(UIAnchor.Side.BottomRight).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(NextPlayset);
		pageMap.GetAnchor(UIAnchor.Side.Bottom).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(ViewDetails);
	}
	
	private void SetUpPlaysets(StateContext context) {
		playsetPanel = pageMap.body.GetComponentInChildren<TweenPosition>();
		var playsets = context.manager.Playsets;
		subPages = new Dictionary<string, PlaysetInfoSubPage>();
		for (int i=0; i<playsets.Count; i++) {
			var playset = playsets[ i ];
			var subPage = NGUITools.AddChild(playsetPanel.gameObject, context.manager.prefabs.playsetSubPage).transform;
			subPage.localPosition = new Vector3(i*Screen.width, 0, 0);
			var subPageMap = subPage.GetComponent<PlaysetInfoSubPage>();
			subPageMap.playset = playset;
			subPages.Add(playset.name, subPageMap);
			ShowInfo(subPageMap, false, 0);
		}
		setUp = true;
	}
	
	private void ShowInfo(PlaysetInfoSubPage subPage, bool on, float speed) {
		float bgAlpha = 1f;
		float txtAlpha = 0f;
		if (on) {
			bgAlpha = 0.15f;
			txtAlpha = 1f;
		}
		TweenAlpha.Begin(subPage.background.gameObject, speed, bgAlpha);
		TweenAlpha.Begin(subPage.body.GetComponentInChildren<UIPanel>().gameObject, speed, txtAlpha);
		TweenAlpha.Begin(subPage.head.gameObject, speed, txtAlpha);
		//TweenAlpha.Begin(subPage.foot.gameObject, speed, txtAlpha);
	}
	
	public void Back() {
		new PlaysetsMenuState().Enter(new StateContext());
	}
	
	public void PrevPlayset() {
		ChangePlayset(-1);
	}
	
	public void NextPlayset() {
		ChangePlayset(1);
	}
	
	private void ChangePlayset(int screenTween) {
		var pageBody = pageMap.body.gameObject;
		var tweener = pageBody.GetComponentInChildren<UITweener>();
		var playsets = initialContext.manager.Playsets;
		var viewIndex = playsets.IndexOf(initialContext.playset);
		var newPs = Mathf.Clamp(screenTween + viewIndex, 0, playsets.Count-1);
		queuedPlayset = playsets[ newPs ];
		//Debug.Log("Queue up "+ queuedPlayset + ", index " + newPs);
		screenTween = queuedPlayset==initialContext.playset ? 0 : screenTween;
		tweener.onFinished = (screenTween == 0) ?
			new List<EventDelegate>() : new List<EventDelegate>(){new EventDelegate(NewState)};
		var tweenerPos = tweener.transform.localPosition;
		var dest = new Vector3(tweenerPos.x-screenTween*Screen.width, tweenerPos.y, tweenerPos.z);
		//Debug.Log("Move " + screenTween + ": "+ screenTween*Screen.width + ", from " + tweenerPos.x + "to "+ dest.x);
		TweenPosition.Begin(tweener.gameObject, 0.5f, dest);
	}
	
	public void NewState() {
		new PlaysetInfoState().Enter(new StateContext(queuedPlayset));
	}
	
	private void ViewDetails() {
		new PlaysetElementsState().Enter(new StateContext(initialContext.playset));
	}
}