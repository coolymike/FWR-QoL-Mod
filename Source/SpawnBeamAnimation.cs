using System;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class SpawnBeamAnimation : MonoBehaviour
{
	// Token: 0x06000499 RID: 1177 RVA: 0x00018F94 File Offset: 0x00017194
	private void Start()
	{
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		base.transform.localScale = new Vector3(0f, base.transform.root.position.y + 9.6f, 0f);
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001900C File Offset: 0x0001720C
	private void Update()
	{
		this.sinSmoothTime = Mathf.Lerp(this.sinSmoothTime, 2f, 1f * Time.deltaTime);
		this.sinTime = Mathf.Lerp(this.sinTime, 3.14159274f, this.sinSmoothTime * Time.deltaTime);
		this.forwardTime = Mathf.Lerp(this.forwardTime, 1f, Time.deltaTime);
		base.transform.localScale = new Vector3(Mathf.Lerp(base.transform.localScale.x, 5f, 1f * Time.deltaTime), Mathf.Lerp(base.transform.localScale.y, 0f, this.sinSmoothTime * Time.deltaTime), Mathf.Lerp(base.transform.localScale.x, 5f, 1f * Time.deltaTime));
	}

	// Token: 0x04000614 RID: 1556
	private float sinSmoothTime;

	// Token: 0x04000615 RID: 1557
	private float sinTime;

	// Token: 0x04000616 RID: 1558
	private float sin;

	// Token: 0x04000617 RID: 1559
	private float forwardTime;
}
