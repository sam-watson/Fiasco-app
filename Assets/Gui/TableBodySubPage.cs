using UnityEngine;
using System.Collections;

public class TableBodySubPage : PageMap {

	protected UITable table;
	
	public override void Awake () {
		base.Awake ();
		table = body.gameObject.GetComponentInChildren<UITable>();
	}
	
	public override GameObject AddContent (UIAnchor anchor, GameObject prefab)
	{
		GameObject placement;
		if (anchor == body) {
			placement = table.gameObject;
		} else placement = anchor.gameObject;
		var content = NGUITools.AddChild(placement, prefab);
		content.name = "Content-" + prefab.name;
		if (anchor == body) {
			table.Reposition(); //FIXME: called too much?
		} else CorrectOffsets(anchor);
		return content;
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		var label = AddContent(anchor, prefab).GetComponentInChildren<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
		return label;
	}
}
