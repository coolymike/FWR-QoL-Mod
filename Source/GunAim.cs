using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class GunAim : MonoBehaviour
{
	// Token: 0x0600001A RID: 26 RVA: 0x000021F5 File Offset: 0x000003F5
	private void Start()
	{
		this.parentCamera = base.GetComponentInParent<Camera>();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000081C8 File Offset: 0x000063C8
	private void Update()
	{
		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;
		if (x <= (float)this.borderLeft || x >= (float)(Screen.width - this.borderRight) || y <= (float)this.borderBottom || y >= (float)(Screen.height - this.borderTop))
		{
			this.isOutOfBounds = true;
		}
		else
		{
			this.isOutOfBounds = false;
		}
		if (!this.isOutOfBounds)
		{
			base.transform.LookAt(this.parentCamera.ScreenToWorldPoint(new Vector3(x, y, 5f)));
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002203 File Offset: 0x00000403
	public bool GetIsOutOfBounds()
	{
		return this.isOutOfBounds;
	}

	// Token: 0x04000011 RID: 17
	public int borderLeft;

	// Token: 0x04000012 RID: 18
	public int borderRight;

	// Token: 0x04000013 RID: 19
	public int borderTop;

	// Token: 0x04000014 RID: 20
	public int borderBottom;

	// Token: 0x04000015 RID: 21
	private Camera parentCamera;

	// Token: 0x04000016 RID: 22
	private bool isOutOfBounds;
}
