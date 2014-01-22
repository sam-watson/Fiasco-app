using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlaysetElementsState : State {
	
	private Playset playset;
	private List<PlaysetElementsSubPage> subPages;
	private List<UIToggle> navButtons;
	private UITweener tweener;
	private Transform tweenerTrans;

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.elementsPanel);
		tweener = pageMap.body.GetComponentInChildren<UITweener>();
		tweenerTrans = tweener.transform;
		tweenerTrans.localPosition = Vector3.zero;
		playset = context.playset;
		var title = pageMap.AddLabel(pageMap.head, context.manager.prefabs.styledLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		title.fontSize = 26;
		var grid = pageMap.body.GetComponentInChildren<UIGrid>();
		grid.cellWidth = Screen.width;
		subPages = new List<PlaysetElementsSubPage>(grid.GetComponentsInChildren<PlaysetElementsSubPage>());
		while (subPages.Count < 4) {
			subPages.Add(
				NGUITools.AddChild(grid.gameObject, context.manager.prefabs.elementsSubPage).GetComponent<PlaysetElementsSubPage>());
		}
		grid.Reposition();
		SetUpButtons();
		SetUpContents();
	}
	
	private void SetUpButtons() {
		pageMap.GetAnchor(UIAnchor.Side.TopLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(Back);
		pageMap.GetAnchor(UIAnchor.Side.BottomLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(PrevElementSet);
		pageMap.GetAnchor(UIAnchor.Side.BottomRight).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(NextElementSet);
		navButtons = pageMap.GetAnchor(UIAnchor.Side.Bottom).GetComponentsInChildren<UIToggle>()
			.OrderBy(t => t.gameObject.name).ToList();
		foreach (var toggle in navButtons) {
			EventDelegate.Add(toggle.onChange, NavToElementSet);
		}
		navButtons[0].value = true;
	}
	
	private void SetUpContents() {
		var prefabs = initialContext.manager.prefabs;
		for (int i=0; i<4; i++) {
			var subPage = subPages[i];
			var elementType = (PlaysetElements.ElementType)i;
			var topLabel = subPage.AddLabel(subPage.head, prefabs.styledLabel);
			topLabel.text = elementType.ToString();
			topLabel.effectStyle = UILabel.Effect.Shadow;
			topLabel.effectColor = Color.red;
			var elements = playset.elements.GetElements(elementType);
			subPage.SetElements(elements);
			subPage.RespectBounds();
		}
	}
	
	public void Back() {
		new PlaysetInfoState().Enter(new StateContext(initialContext.playset));
	}
	
	public void PrevElementSet() {
		//ScrollElementSet(-1);
		navButtons[ Mathf.Clamp(GetCurrentMenuPos()-1, 0, 3) ].value = true;
	}
	
	public void NextElementSet() {
		//ScrollElementSet(1);
		navButtons[ Mathf.Clamp(GetCurrentMenuPos()+1, 0, 3) ].value = true;
	}
	
	public void NavToElementSet() {
		int toPage = navButtons.IndexOf( UIToggle.current );
		int moveBy = toPage - GetCurrentMenuPos();
		float time = 0.2f;
		ScrollElementSet( moveBy, time * Mathf.Abs(moveBy) );
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
