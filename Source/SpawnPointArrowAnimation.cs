using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class SpawnPointArrowAnimation : MonoBehaviour
{
	// Token: 0x06000284 RID: 644 RVA: 0x0000431C File Offset: 0x0000251C
	private void Start()
	{
		this.localStartPosition = base.transform.localPosition;
		this.animatedPosition = this.localStartPosition;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x00012A70 File Offset: 0x00010C70
	private void Update()
	{
		this.animatedPosition.y = this.localStartPosition.y + Mathf.Sin(Time.time * 3f) * 0.1f;
		base.transform.localPosition = this.animatedPosition;
		base.transform.Rotate(base.transform.up * 80f * Time.deltaTime, Space.Self);
	}

	// Token: 0x040003D1 RID: 977
	private Vector3 localStartPosition;

	// Token: 0x040003D2 RID: 978
	private Vector3 animatedPosition;
}
