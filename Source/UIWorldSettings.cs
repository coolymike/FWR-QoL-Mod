using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BE RID: 190
public class UIWorldSettings : MonoBehaviour
{
	// Token: 0x060003BF RID: 959 RVA: 0x000052E6 File Offset: 0x000034E6
	private void Start()
	{
		this.UpdateSettings();
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00016B1C File Offset: 0x00014D1C
	private void UpdateSettings()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		this.colorCodeToggle.SetIsOnWithoutNotify(WorldData.instance.data.settings.ColorCodeRagdolls);
		this.dontTouchGroundToggle.SetIsOnWithoutNotify(WorldData.instance.data.settings.RagdollOnGroundTouch);
		this.lockSpawnToggle.SetIsOnWithoutNotify(WorldData.instance.data.settings.lockSpawn);
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00016B94 File Offset: 0x00014D94
	public void ApplySettings()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		WorldData.instance.data.settings.ColorCodeRagdolls = this.colorCodeToggle.isOn;
		WorldData.instance.data.settings.RagdollOnGroundTouch = this.dontTouchGroundToggle.isOn;
		WorldData.instance.data.settings.lockSpawn = this.lockSpawnToggle.isOn;
	}

	// Token: 0x04000522 RID: 1314
	public Toggle colorCodeToggle;

	// Token: 0x04000523 RID: 1315
	public Toggle dontTouchGroundToggle;

	// Token: 0x04000524 RID: 1316
	public Toggle lockSpawnToggle;
}
