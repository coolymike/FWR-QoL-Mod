using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class ExplosionPlatformActivator : MonoBehaviour
{
	// Token: 0x06000040 RID: 64 RVA: 0x00002407 File Offset: 0x00000607
	private void Start()
	{
		this.currentRepeatTime = this.DefaultRepeatTime;
		base.Invoke("Init", this.TimeDelay);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002426 File Offset: 0x00000626
	private void Init()
	{
		this.canUpdate = true;
		this.Effect.SetActive(true);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00008E10 File Offset: 0x00007010
	private void Update()
	{
		if (!this.canUpdate || this.Effect == null)
		{
			return;
		}
		this.currentTime += Time.deltaTime;
		if (this.currentTime > this.currentRepeatTime)
		{
			this.currentTime = 0f;
			this.Effect.SetActive(false);
			this.Effect.SetActive(true);
		}
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000243B File Offset: 0x0000063B
	private void OnTriggerEnter(Collider coll)
	{
		this.currentRepeatTime = this.NearRepeatTime;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002449 File Offset: 0x00000649
	private void OnTriggerExit(Collider other)
	{
		this.currentRepeatTime = this.DefaultRepeatTime;
	}

	// Token: 0x04000059 RID: 89
	public GameObject Effect;

	// Token: 0x0400005A RID: 90
	public float TimeDelay;

	// Token: 0x0400005B RID: 91
	public float DefaultRepeatTime = 5f;

	// Token: 0x0400005C RID: 92
	public float NearRepeatTime = 3f;

	// Token: 0x0400005D RID: 93
	private float currentTime;

	// Token: 0x0400005E RID: 94
	private float currentRepeatTime;

	// Token: 0x0400005F RID: 95
	private bool canUpdate;
}
