using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class MouseControl : MonoBehaviour
{
	// Token: 0x0600045E RID: 1118 RVA: 0x000059B8 File Offset: 0x00003BB8
	public static void HideMouse(bool hide)
	{
		Cursor.visible = !hide;
		if (hide)
		{
			Cursor.lockState = CursorLockMode.Locked;
			return;
		}
		Cursor.lockState = CursorLockMode.None;
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000059D3 File Offset: 0x00003BD3
	private void Awake()
	{
		MouseControl.HideMouse(true);
	}
}
