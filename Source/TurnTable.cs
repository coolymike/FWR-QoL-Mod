using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class TurnTable : MonoBehaviour
{
	// Token: 0x0600049C RID: 1180 RVA: 0x00005CAF File Offset: 0x00003EAF
	private void Start()
	{
		this.startRotation = base.transform.eulerAngles;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000190F8 File Offset: 0x000172F8
	private void Update()
	{
		this.sinWave = Mathf.Sin(Time.time * this.speed);
		if (this.flipFlop)
		{
			base.transform.rotation = Quaternion.Euler(this.startRotation.x, this.startRotation.y + this.sinWave * this.flipFlopAmount, this.startRotation.z);
			return;
		}
		base.transform.Rotate(0f, this.speed * Time.deltaTime, 0f);
	}

	// Token: 0x04000618 RID: 1560
	public float speed = 100f;

	// Token: 0x04000619 RID: 1561
	public bool flipFlop;

	// Token: 0x0400061A RID: 1562
	public float flipFlopAmount = 45f;

	// Token: 0x0400061B RID: 1563
	private Vector3 startRotation;

	// Token: 0x0400061C RID: 1564
	private float sinWave;
}
