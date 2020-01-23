using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class ExplosionsFPS : MonoBehaviour
{
	// Token: 0x06000046 RID: 70 RVA: 0x00002475 File Offset: 0x00000675
	private void Awake()
	{
		this.guiStyleHeader.fontSize = 14;
		this.guiStyleHeader.normal.textColor = new Color(1f, 1f, 1f);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000024A8 File Offset: 0x000006A8
	private void OnGUI()
	{
		GUI.Label(new Rect(0f, 0f, 30f, 30f), "FPS: " + (int)this.fps, this.guiStyleHeader);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00008E78 File Offset: 0x00007078
	private void Update()
	{
		this.timeleft -= Time.deltaTime;
		this.frames++;
		if ((double)this.timeleft <= 0.0)
		{
			this.fps = (float)this.frames;
			this.timeleft = 1f;
			this.frames = 0;
		}
	}

	// Token: 0x04000060 RID: 96
	private readonly GUIStyle guiStyleHeader = new GUIStyle();

	// Token: 0x04000061 RID: 97
	private float timeleft;

	// Token: 0x04000062 RID: 98
	private float fps;

	// Token: 0x04000063 RID: 99
	private int frames;
}
