using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class FinalBuildConditions : MonoBehaviour
{
	// Token: 0x06000442 RID: 1090 RVA: 0x00018278 File Offset: 0x00016478
	private void Awake()
	{
		FinalBuildConditions.Condition condition = this.conditions;
		if (condition == FinalBuildConditions.Condition.DeactivateOnBuild)
		{
			this.DeactivateOnBuild();
		}
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0000589D File Offset: 0x00003A9D
	private void DeactivateOnBuild()
	{
		if (Application.isEditor)
		{
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x040005B1 RID: 1457
	public FinalBuildConditions.Condition conditions;

	// Token: 0x020000D4 RID: 212
	public enum Condition
	{
		// Token: 0x040005B3 RID: 1459
		DoNothing,
		// Token: 0x040005B4 RID: 1460
		DeactivateOnBuild
	}
}
