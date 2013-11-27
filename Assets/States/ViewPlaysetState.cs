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
		var pageMap = menuPanel.GetComponent<PageMap>();
		var table = pageMap.body.GetComponentInChildren<UITablePositioned>();
		// need- back button, playset info, swipe nav to other playsets
		var playset = context.playset;
		Debug.Log(playset.name);
		//header
		var title = AddLabel(pageMap.head, context.manager.playsetLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		//body
		var subtitle = AddLabel(table.gameObject, context.manager.playsetLabel);
		subtitle.text = playset.info.subtitle;
		subtitle.fontStyle = FontStyle.Bold;
		var summary = AddLabel(table.gameObject, context.manager.playsetLabel);
		summary.text = playset.info.summary;
		var movienight = AddLabel(table.gameObject, context.manager.playsetLabel);
		movienight.text = "Movie Night:";
		movienight.fontStyle = FontStyle.Bold;
		var movies = AddLabel(table.gameObject, context.manager.playsetLabel);
		movies.text = playset.info.movienight;
		movies.fontStyle = FontStyle.Italic;
		var credits = AddLabel(table.gameObject, context.manager.playsetLabel);
		credits.text = "CREDITS: " + playset.info.credits;
		table.Reposition();
	}
	
	public override void Exit ()
	{
		base.Exit ();
		var pageMap = menuPanel.GetComponent<PageMap>();
		pageMap.ClearAll();
	}
	
	private UILabel AddLabel(GameObject parent, GameObject labelPrefab) {
		return NGUITools.AddChild(parent, labelPrefab).GetComponent<UILabel>();
	}
	
	private UILabel AddLabel(GameObject parent, GameObject labelPrefab, UIWidget.Pivot pivot) {
		var label = AddLabel(parent, labelPrefab);
		label.pivot = pivot;
		return label;
	}
}