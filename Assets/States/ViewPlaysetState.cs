using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewPlaysetState : State {
	
	public Playset queuedPlayset;
	private static TweenPosition playsetPanel;
	private static Dictionary<string, PlaysetViewSubPage> subPages;
	
	private static bool setUp = false;

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.viewerPanel);
		//instantiate a new page with page map = template is setup there
		// template includes header and footer elements and body structure
		var playset = context.playset;
		var playsets = context.manager.Playsets;
		var viewIndex = playsets.IndexOf(playset);
		Debug.Log("Starting "+ playset.name + "view-state, index " + viewIndex);
		SetUpButtons();
		if (!setUp) {
			playsetPanel = pageMap.body.GetComponentInChildren<TweenPosition>();
			SetUpPlaysets(context);
		}
		var curPos = new Vector3(-viewIndex*Screen.width, 0, 0);
		TweenPosition.Begin(playsetPanel.gameObject, 0, curPos);
		ShowInfo(subPages[playset.name], true, 1f);
	}
	
	public override void Exit ()
	{
		ShowInfo(subPages[initialContext.playset.name], false, 0);
		base.Exit ();
	}
	
	private void SetUpButtons() {
		//FIXME: reset by new states, but still probably not good to have persistent elements referencing this object
		pageMap.GetAnchor(UIAnchor.Side.TopLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(Back);
		pageMap.GetAnchor(UIAnchor.Side.BottomLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(PrevPlayset);
		pageMap.GetAnchor(UIAnchor.Side.BottomRight).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(NextPlayset);
	}
	
	private void SetUpPlaysets(StateContext context) {
		//playsets
		var playsets = context.manager.Playsets;
		var viewIndex = playsets.IndexOf(context.playset);
		subPages = new Dictionary<string, PlaysetViewSubPage>();
		for (int i=0; i<playsets.Count; i++) {
			var playset = playsets[ i ];
			var subPage = NGUITools.AddChild(playsetPanel.gameObject, context.manager.prefabs.playsetSubPage).transform;
			subPage.localPosition = new Vector3(i*Screen.width, 0, 0);
			var subPageMap = subPage.gameObject.GetComponent<PlaysetViewSubPage>();
			SetUpSubPage(subPageMap, playset);
			subPages.Add(playset.name, subPageMap);
			ShowInfo(subPageMap, false, 0);
		}
		setUp = true;
	}
	
	private void SetUpSubPage(PlaysetViewSubPage subPage, Playset playset) {
		subPage.name = playset.name;
		Debug.Log("Setting up "+ playset.name);
		var hitchcockLabel = initialContext.manager.prefabs.styledLabel;
		var otherLabel = initialContext.manager.prefabs.plainLabel;
		//background
		subPage.background.spriteName = "bg " + playset.name;
		//header
		var title = AddLabel(subPage, subPage.head, hitchcockLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		title.fontSize = 26;
		//body
		var body = subPage.body;
		var subtitle = AddLabel(subPage, body, hitchcockLabel);
		subtitle.text = playset.info.subtitle;
		subtitle.fontStyle = FontStyle.Bold;
		var summary = AddLabel(subPage, body, otherLabel);
		summary.text = playset.info.summary;
		var movienight = AddLabel(subPage, body, hitchcockLabel);
		movienight.text = "Movie Night:";
		movienight.fontStyle = FontStyle.Bold;
		var movies = AddLabel(subPage, body, otherLabel);
		movies.text = playset.info.movienight;
		movies.fontStyle = FontStyle.Italic;
		var credits = AddLabel(subPage, body, otherLabel);
		credits.text = "CREDITS: " + playset.info.credits;
	}
	
	private UILabel AddLabel(PageMap page, UIAnchor anchor, GameObject prefab) {
		return page.AddLabel(anchor, prefab);
	}
	
	private void ShowInfo(PlaysetViewSubPage subPage, bool on, float speed) {
		float bgAlpha = 1f;
		float txtAlpha = 0f;
		if (on) {
			bgAlpha = 0.2f;
			txtAlpha = 1f;
		}
		TweenAlpha.Begin(subPage.background.gameObject, speed, bgAlpha);
		TweenAlpha.Begin(subPage.body.GetComponentInChildren<UIPanel>().gameObject, speed, txtAlpha);
		TweenAlpha.Begin(subPage.head.gameObject, speed, txtAlpha);
		TweenAlpha.Begin(subPage.foot.gameObject, speed, txtAlpha);
	}
	
	public void Back() {
		new BrowsePlaysetsState().Enter(new StateContext());
	}
	
	public void PrevPlayset() {
		ChangePlayset(-1);
	}
	
	public void NextPlayset() {
		ChangePlayset(1);
	}
	
	private void ChangePlayset(int screenTween) {
		var pageBody = pageMap.body.gameObject;
		var playsets = initialContext.manager.Playsets;
		var viewIndex = playsets.IndexOf(initialContext.playset);
		//var loop = (playsets.Count-1)==0 ? 1 : (playsets.Count-1);
		var newPs = Mathf.Clamp(screenTween + viewIndex, 0, playsets.Count-1);
		queuedPlayset = playsets[ newPs ];
		Debug.Log("Queue up "+ queuedPlayset + ", index " + newPs);
		screenTween = queuedPlayset==initialContext.playset ? 0 : screenTween;
		var tweener = pageBody.GetComponentInChildren<UITweener>();
		tweener.onFinished = new List<EventDelegate>(){new EventDelegate(NewState)};
		var tweenerPos = tweener.transform.localPosition;
		var dest = new Vector3(tweenerPos.x-screenTween*Screen.width, tweenerPos.y, tweenerPos.z);
		Debug.Log("Move " + screenTween + ": "+ screenTween*Screen.width + ", from " + tweenerPos.x + "to "+ dest.x);
		TweenPosition.Begin(tweener.gameObject, 1f, dest);
	}
	
	public void NewState() {
		new ViewPlaysetState().Enter(new StateContext(queuedPlayset));
	}
}