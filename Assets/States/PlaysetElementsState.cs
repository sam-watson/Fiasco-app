using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetElementsState : State {
	
	private Playset playset;
	private List<ElementsSubPage> subPages;
	private UITweener tweener;
	private Transform tweenerTrans;

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.elementsPanel);
		tweener = pageMap.body.GetComponentInChildren<UITweener>();
		tweenerTrans = tweener.transform;
		playset = context.playset;
		var title = pageMap.AddLabel(pageMap.head, context.manager.prefabs.styledLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		title.fontSize = 26;
		var grid = pageMap.body.GetComponentInChildren<UIGrid>();
		grid.cellWidth = Screen.width;
		subPages = new List<ElementsSubPage>(grid.GetComponentsInChildren<ElementsSubPage>());
		while (subPages.Count < 4) {
			subPages.Add(
				NGUITools.AddChild(grid.gameObject, context.manager.prefabs.elementsSubPage).GetComponent<ElementsSubPage>());
		}
		grid.Reposition();
		SetUpButtons();
		SetUpContents();
	}
	
	//TODO: clear contents or rewrite contents
	//TODO: subElement table set up via prefab script
	//TODO: improve layout and positioning
	//TODO: element navigation buttons
	//TODO: dice symbols
	
	private void SetUpButtons() {
		pageMap.GetAnchor(UIAnchor.Side.TopLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(Back);
		pageMap.GetAnchor(UIAnchor.Side.BottomLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(PrevElementSet);
		pageMap.GetAnchor(UIAnchor.Side.BottomRight).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(NextElementSet);
		// TODO: swipe colliders, callback and stuff
	}
	
	private void SetUpContents() {
		var prefabs = initialContext.manager.prefabs;
		for (int i=0; i<4; i++) {
			var subPage = subPages[i];
			var elementType = (PlaysetElements.ElementType)i;
			var topLabel = subPage.AddLabel(subPage.head, prefabs.styledLabel);
			topLabel.text = elementType.ToString();
			topLabel.pivot = UIWidget.Pivot.Center;
			topLabel.effectStyle = UILabel.Effect.Shadow;
			topLabel.effectColor = Color.red;
			var elements = playset.elements.GetElements(elementType);
			subPage.SetElements(elements);
		}
	}
	
	public void Back() {
		new ViewPlaysetState().Enter(new StateContext(initialContext.playset));
	}
	
	public void PrevElementSet() {
		ScrollElementSet(-1);
	}
	
	public void NextElementSet() {
		ScrollElementSet(1);
	}
	
	private void ScrollElementSet(int direction) {
		ScrollElementSet(direction, 1f);
	}
	
	private void ScrollElementSet(int direction, float time) {
		var tweenerPos = tweenerTrans.localPosition;
		var destX = Mathf.Clamp( GetCurrentMenuPos()+direction, 0, 3 ) * -Screen.width;
		TweenPosition.Begin(tweener.gameObject, time, new Vector3(destX, tweenerPos.y, tweenerPos.z));
	}
	
	private int GetCurrentMenuPos() {
		return System.Convert.ToInt32(-tweenerTrans.localPosition.x / Screen.width);
	}
}
