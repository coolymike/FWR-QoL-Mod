using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class ExplosionsDeactivateRendererByTime : MonoBehaviour
{
	// Token: 0x0600004D RID: 77 RVA: 0x0000250D File Offset: 0x0000070D
	private void Awake()
	{
		this.rend = base.GetComponent<Renderer>();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000251B File Offset: 0x0000071B
	private void DeactivateRenderer()
	{
		this.rend.enabled = false;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002529 File Offset: 0x00000729
	private void OnEnable()
	{
		this.rend.enabled = true;
		base.Invoke("DeactivateRenderer", this.TimeDelay);
	}

	// Token: 0x0400006B RID: 107
	public float TimeDelay = 1f;

	// Token: 0x0400006C RID: 108
	private Renderer rend;
}
