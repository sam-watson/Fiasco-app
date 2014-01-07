using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaysetElementsState : State {
	
	private Playset playset;
	private List<ElementsSubPage> subPages;

	public override void Enter (StateContext context)
	{
		base.Enter (context);
		SetMenuPanel(context.manager.elementsPanel);
		playset = context.playset;
		var title = pageMap.AddLabel(pageMap.head, context.manager.prefabs.styledLabel);
		title.text = playset.name;
		title.effectStyle = UILabel.Effect.Outline;
		title.color = Color.red;
		title.fontSize = 26;
		var grid = pageMap.body.GetComponentInChildren<UIGrid>();
		grid.cellWidth = Screen.width;
		subPages = new List<ElementsSubPage>(grid.GetComponentsInChildren<ElementsSubPage>());
		if (subPages.Count == 0) { //or should it be if <4?
			for (int i=0; i<4; i++) {
				subPages.Add(NGUITools.AddChild(grid.gameObject, context.manager.prefabs.elementsSubPage).GetComponent<ElementsSubPage>());
			}
		}
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
			var elements = playset.elements.GetElements(elementType);
			subPage.SetElements(elements);
		}
	}
	
	public void Back() {
		new ViewPlaysetState().Enter(new StateContext(initialContext.playset));
	}
}
