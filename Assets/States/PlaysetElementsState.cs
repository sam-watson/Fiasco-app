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
		var frame = pageMap.body.GetComponentInChildren<UIGrid>();
		frame.cellWidth = Screen.width;
		subPages = new List<ElementsSubPage>(frame.GetComponentsInChildren<ElementsSubPage>());
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
			foreach (var element in elements) {
				var fab = subPage.AddContent(subPage.body, prefabs.elementsLabel);
				var expLabel = fab.AddComponent<ExpandingButton>();
				fab.AddComponent<UIDragPanelContents>();
				expLabel.LabelText = element.Key;
				expLabel.SetSubText(element.Value);
			}
		}
	}
	
	public void Back() {
		new ViewPlaysetState().Enter(new StateContext(initialContext.playset));
	}
}
