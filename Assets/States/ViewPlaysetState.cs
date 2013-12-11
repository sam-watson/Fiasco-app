using UnityEngine;
using System.Collections;

public class ViewPlaysetState : State {

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		menuPanel = context.manager.viewerPanel;
		menuPanel.SetActive(true);
		//instantiate a new page with page map = template is setup there
		// template includes header and footer elements and body structure
		var playset = context.playset;
		Debug.Log(playset.name);
		pageMap = menuPanel.GetComponent<PageMap>();
		SetUp(context);
	}
	
	public override void Exit ()
	{
		base.Exit ();
		var pageMap = menuPanel.GetComponent<PageMap>();
		//pageMap.ClearAll();
	}
	
	private void SetUp(StateContext context) {
		//swipe-drag-nav colliders
		pageMap.GetAnchor(UIAnchor.Side.TopLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(Back);
		pageMap.GetAnchor(UIAnchor.Side.BottomLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(PrevPlayset);
		pageMap.GetAnchor(UIAnchor.Side.BottomRight).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(NextPlayset);
		//playsets
		var playsets = context.manager.Playsets;
		var viewIndex = playsets.IndexOf(context.playset);
		var empty = pageMap.body.transform.childCount == 0;
		for (int i=-1; i<2; i++) {
			var loop = (playsets.Count-1)==0 ? 1 : (playsets.Count-1);
			var playset = playsets[ (i + viewIndex) %  loop ];
			Transform subPage;
			subPage = pageMap.transform.Find(playset.name);
			if (subPage == null) {
				subPage = 
					NGUITools.AddChild(pageMap.body.gameObject, context.manager.prefabs.playsetSubPage).transform;
			}
			subPage.localPosition = new Vector3(i*Screen.width, 0, 0);
			SetUpSubPage(subPage.gameObject.GetComponent<PageMap>(), playset);
		}
	}
	
	private void SetUpSubPage(PageMap subPage, Playset playset) {
		subPage.name = playset.name;
		var hitchcockLabel = initialContext.manager.prefabs.styledLabel;
		var otherLabel = initialContext.manager.prefabs.plainLabel;
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
		var bodyPos = pageBody.transform.position;
		var dest = new Vector3(bodyPos.x+screenTween*Screen.width, bodyPos.y, bodyPos.z);
		TweenPosition.Begin(pageBody, 1f, dest);
	}
}