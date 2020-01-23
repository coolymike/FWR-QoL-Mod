using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class CameraZoom : MonoBehaviour
{
	// Token: 0x0600008B RID: 139 RVA: 0x000028AA File Offset: 0x00000AAA
	private void Awake()
	{
		this.camera = base.GetComponent<Camera>();
		this.startFocalLength = this.camera.focalLength;
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00009F08 File Offset: 0x00008108
	private void Update()
	{
		if (InputManager.CameraZoom() && !LevelManager.BuildModeOn)
		{
			this.camera.focalLength = Mathf.Lerp(this.camera.focalLength, this.startFocalLength * this.zoomMultiplier, this.zoomTime * Time.deltaTime);
			return;
		}
		this.camera.focalLength = Mathf.Lerp(this.camera.focalLength, this.startFocalLength, this.zoomTime * Time.deltaTime);
	}

	// Token: 0x04000108 RID: 264
	private Camera camera;

	// Token: 0x04000109 RID: 265
	private float startFocalLength;

	// Token: 0x0400010A RID: 266
	private float zoomMultiplier = 2f;

	// Token: 0x0400010B RID: 267
	private float zoomTime = 6f;
}
