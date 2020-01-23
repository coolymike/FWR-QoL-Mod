using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class UIDisplayHUD : MonoBehaviour
{
	// Token: 0x06000336 RID: 822 RVA: 0x00004D0F File Offset: 0x00002F0F
	private void Start()
	{
		GameData.OnSettingsChanged += this.UpdateSettings;
		this.UpdateSettings();
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00004D28 File Offset: 0x00002F28
	private void OnDestroy()
	{
		GameData.OnSettingsChanged -= this.UpdateSettings;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00014E98 File Offset: 0x00013098
	private void UpdateSettings()
	{
		for (int i = 0; i < this.hudElements.Length; i++)
		{
			this.hudElements[i].SetActive(GameData.settings.DisplayHUD);
		}
	}

	// Token: 0x040004A1 RID: 1185
	public GameObject[] hudElements = new GameObject[0];
}
