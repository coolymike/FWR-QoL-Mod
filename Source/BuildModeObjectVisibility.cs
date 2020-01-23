using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class BuildModeObjectVisibility : MonoBehaviour
{
	// Token: 0x060004D8 RID: 1240 RVA: 0x0001A12C File Offset: 0x0001832C
	private void Start()
	{
		this.buildObject = base.gameObject.transform.root.GetComponent<BuildItemContainer>();
		if (this.buildObject != null && !this.buildObject.isSelectedObject)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000658 RID: 1624
	private BuildItemContainer buildObject;
}
