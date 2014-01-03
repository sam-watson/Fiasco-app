using UnityEngine;
using System.Collections;

public class TableBodySubPage : PageMap {

	protected UITable table;
	
	public override void Awake () {
		base.Awake ();
		table = body.gameObject.GetComponentInChildren<UITable>();
	}
	
	public override UILabel AddLabel (UIAnchor anchor, GameObject prefab)
	{
		GameObject placement;
		if (anchor == body) {
			placement = table.gameObject;
		} else placement = anchor.gameObject;
		var label = NGUITools.AddChild(placement, prefab).GetComponentInChildren<UILabel>();
		label.pivot = UIWidget.Pivot.Left;
		if (anchor == body) {
			table.Reposition(); //FIXME: called too much?
		} else CorrectOffsets(anchor);
		label.name = "a"; //prevent reordering
		return label;
	}
}
