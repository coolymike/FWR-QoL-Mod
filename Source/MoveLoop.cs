using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class MoveLoop : MonoBehaviour
{
	// Token: 0x06000461 RID: 1121 RVA: 0x000059DB File Offset: 0x00003BDB
	private void Start()
	{
		if (this.startOnEnd)
		{
			this.startPosition = base.transform.localPosition + this.toLocalPosition;
			return;
		}
		this.startPosition = base.transform.localPosition;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00005A13 File Offset: 0x00003C13
	private void OnEnable()
	{
		this.sinTime = this.timeOffset;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00005A21 File Offset: 0x00003C21
	private void FixedUpdate()
	{
		this.Move();
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x000185A4 File Offset: 0x000167A4
	private void Move()
	{
		this.sinTime += Time.deltaTime * (float)this.speed;
		this.sinValue = new Vector3(this.To01(Mathf.Sin(this.sinTime * this.axisSpeedOffset.x)) * this.toLocalPosition.x, this.To01(Mathf.Sin(this.sinTime * this.axisSpeedOffset.y)) * this.toLocalPosition.y, this.To01(Mathf.Sin(this.sinTime * this.axisSpeedOffset.z)) * this.toLocalPosition.z);
		if (this.startOnEnd)
		{
			base.transform.localPosition = this.startPosition - this.sinValue;
			return;
		}
		base.transform.localPosition = this.startPosition + this.sinValue;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00005A29 File Offset: 0x00003C29
	private float To01(float value)
	{
		return (value + 1f) * 0.5f;
	}

	// Token: 0x040005C6 RID: 1478
	private Vector3 startPosition;

	// Token: 0x040005C7 RID: 1479
	public Vector3 toLocalPosition = Vector3.up;

	// Token: 0x040005C8 RID: 1480
	[Space]
	public Vector3 axisSpeedOffset = Vector3.one;

	// Token: 0x040005C9 RID: 1481
	public float timeOffset;

	// Token: 0x040005CA RID: 1482
	public bool startOnEnd;

	// Token: 0x040005CB RID: 1483
	[Range(1f, 100f)]
	public int speed = 1;

	// Token: 0x040005CC RID: 1484
	private float sinTime;

	// Token: 0x040005CD RID: 1485
	private Vector3 sinValue;
}
