using UnityEngine;
using System.Collections;

public class TableBodySubPage : PageMap {
	
	protected UIDraggablePanel dragPanel;
	protected UIPanel panel;
	protected UITable table;
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
		panel.clipSoftness = new Vector2(1f, 10f);
		var clipHeight = Screen.height - headerMargin - footerMargin;
		var clipMid = -(headerMargin + clipHeight/2f);
		panel.clipRange = new Vector4(Screen.width/2f, clipMid, Screen.width, clipHeight);
		RepositScrollPanel();
	}
	
	protected void RepositScrollPanel() {
		var toPos = new Vector3(0f, -headerMargin, 0f);
		SpringPanel.Begin(panel.gameObject, toPos, 10f);
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
	
	public override Transform GetTrans (UIAnchor angkor)
	{
		if (angkor == head) return head.transform;
		if (angkor == body) return table.transform;
		if (angkor == foot) return foot.transform;
		return base.GetTrans (angkor);
	}
}
