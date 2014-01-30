using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetInfoSubPage : TableBodySubPage {
	
	public Playset playset;
	private bool repos;
	
	public override void Start() {
		base.Start();
		PositionStuff();
		if (playset != null) {
			SetUpContents();
		}
	}
	
	void Update() {
		if (repos) {
			table.Reposition();
			repos = false;
		}
	}
	
	private void PositionStuff() {
		var backButton = parentPage.GetAnchor(UIAnchor.Side.TopLeft);
		//place header right of back button
		head.pixelOffset.x = backButton.pixelOffset.x * 2;
		head.pixelOffset.y = backButton.pixelOffset.y;
		head.relativeOffset.x = 0.05f;
		table.padding = new Vector2(10, 5);
		SetUpScrollPanel(Mathf.Abs(backButton.pixelOffset.y * 2f));
	}
	
	private void SetUpContents() {
		name = playset.name;
		Debug.Log("Setting up "+ playset.name);
		var manager = StateManager.Instance;
		var hitchcockLabel = manager.prefabs.styledLabel;
		var plainLabel = manager.prefabs.plainLabel;
		//background
		background.spriteName = "bg " + playset.name;
		//header
		var title = AddLabel(head, hitchcockLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		title.fontSize = 26;
		//body
		var subtitle = AddLabel(body, hitchcockLabel);
		subtitle.text = playset.info.subtitle;
		subtitle.fontSize = 22;
		var summary = AddLabel(body, plainLabel);
		summary.text = playset.info.summary;
		summary.fontSize = 22;
		var movienight = AddLabel(body, hitchcockLabel);
		movienight.text = "Movie Night:";
		movienight.fontSize = 22;
		var movies = AddLabel(body, plainLabel);
		movies.text = playset.info.movienight;
		movies.fontSize = 22;
		movies.fontStyle = FontStyle.Italic;
		var credits = AddLabel(body, plainLabel);
		credits.text = "CREDITS: " + playset.info.credits;
		credits.fontSize = 22;
		repos = true;
	}
}

