using UnityEngine;
using System.Collections;

public class ScrollBodySubPage : PageMap {
	
	protected UIDraggablePanel dragPanel;
	protected UIPanel panel;
	protected UITable table;
	protected float clipHeight;
	protected float clipFade;
	protected float headerMargin;
	protected float footerMargin;
	
	public override void Start () {
		base.Start();
		dragPanel = body.gameObject.GetComponentInChildren<UIDraggablePanel>();
		panel = dragPanel.panel;
		table = panel.GetComponentInChildren<UITable>();
	}
	
	protected void SetUpScrollPanel(float margins) {
		SetUpScrollPanel(margins, margins);
	}
	
	protected void SetUpScrollPanel(float hMargin, float fMargin) {
		headerMargin = hMargin;
		footerMargin = fMargin;
		panel.clipping = UIDrawCall.Clipping.SoftClip;
		clipFade = 10f;
		panel.clipSoftness = new Vector2(1f, clipFade);
		clipHeight = Screen.height - headerMargin - footerMargin;
		var clipMid = -(headerMargin + clipHeight/2f);
		panel.clipRange = new Vector4(Screen.width/2f, clipMid, Screen.width, clipHeight);
		ScrollToTop();
	}
	
	public void ScrollToTop() {
		var toPos = new Vector3(1f, -(headerMargin + clipFade), 0f);
		SpringPanel.Begin(panel.gameObject, toPos, 10f);
	}
	
	public void ScrollToBottom() {
		var extraLength = NGUIMath.CalculateRelativeWidgetBounds(panel.transform).size.y - clipHeight;
		var toPos = new Vector3(1f, (extraLength - headerMargin + clipFade), 0f);
		SpringPanel.Begin(panel.gameObject, toPos, 10f);
	}
	
	public void ConstrainContents() {
		dragPanel.RestrictWithinBounds(false);
	}
	
	public bool IsVisible(Vector3 worldPos) {
		return panel.IsVisible(worldPos);
	}
	
	public override GameObject AddContent (UIAnchor anchor, GameObject prefab)
	{
		if (anchor == body) {
			var content = NGUITools.AddChild(table.gameObject, prefab);
			content.name = "Ordered Table Content";
			return content;
		} else {
			return base.AddContent(anchor, prefab);
		}
	}
}
