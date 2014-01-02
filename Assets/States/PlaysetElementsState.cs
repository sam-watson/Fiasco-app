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
		var frame = pageMap.body.GetComponentInChildren<UIGrid>();
		frame.cellWidth = Screen.width;
		subPages = new List<ElementsSubPage>(frame.GetComponentsInChildren<ElementsSubPage>());
		SetUpButtons();
		SetUpContents();
	}
	
	private void SetUpButtons() {
		pageMap.GetAnchor(UIAnchor.Side.TopLeft).GetComponentInChildren<Button>()
			.OnClick = new EventDelegate(Back);
		// TODO: swipe callback and stuff
	}
	
	private void SetUpContents() {
		var prefabs = initialContext.manager.prefabs;
		for (int i=0; i<4; i++) {
			var subPage = subPages[i];
			var elementType = (PlaysetElements.ElementType)i;
			var topLabel = subPage.AddLabel(subPage.head, prefabs.styledLabel);
			topLabel.text = elementType.ToString();
			var elements = playset.elements.GetElements(elementType);
			foreach (var element in elements) {
				var elemLabel = subPage.AddLabel(subPage.body, prefabs.elementsLabel);
				elemLabel.text = element.Key;
				//add sublabels via custom script
			}
		}
	}
	
	public void Back() {
		new ViewPlaysetState().Enter(new StateContext(initialContext.playset));
	}
}
