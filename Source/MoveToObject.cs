using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class MoveToObject : MonoBehaviour
{
	// Token: 0x06000467 RID: 1127 RVA: 0x00005A5D File Offset: 0x00003C5D
	private void Update()
	{
		if (this.updateOn == MoveToObject.update.Update)
		{
			this.SetPosition();
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00005A6D File Offset: 0x00003C6D
	private void LateUpdate()
	{
		if (this.updateOn == MoveToObject.update.LateUpdate)
		{
			this.SetPosition();
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00005A7E File Offset: 0x00003C7E
	private void FixedUpdate()
	{
		if (this.updateOn == MoveToObject.update.FixedUpdate)
		{
			this.SetPosition();
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00018694 File Offset: 0x00016894
	private void SetPosition()
	{
		if (this.from == null || this.to == null)
		{
			return;
		}
		this.from.position = new Vector3(this.posX ? this.to.position.x : this.from.position.x, this.posY ? this.to.position.y : this.from.position.y, this.posZ ? this.to.position.z : this.from.position.z);
		this.from.eulerAngles = new Vector3(this.rotX ? this.to.eulerAngles.x : this.from.eulerAngles.x, this.rotY ? this.to.eulerAngles.y : this.from.eulerAngles.y, this.rotZ ? this.to.eulerAngles.z : this.from.eulerAngles.z);
	}

	// Token: 0x040005CE RID: 1486
	public Transform from;

	// Token: 0x040005CF RID: 1487
	public Transform to;

	// Token: 0x040005D0 RID: 1488
	public MoveToObject.update updateOn;

	// Token: 0x040005D1 RID: 1489
	public bool posX;

	// Token: 0x040005D2 RID: 1490
	public bool posY;

	// Token: 0x040005D3 RID: 1491
	public bool posZ;

	// Token: 0x040005D4 RID: 1492
	public bool rotX;

	// Token: 0x040005D5 RID: 1493
	public bool rotY;

	// Token: 0x040005D6 RID: 1494
	public bool rotZ;

	// Token: 0x020000DE RID: 222
	public enum update
	{
		// Token: 0x040005D8 RID: 1496
		Update,
		// Token: 0x040005D9 RID: 1497
		LateUpdate,
		// Token: 0x040005DA RID: 1498
		FixedUpdate
	}
}
