using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class ExplosionsScaleCurves : MonoBehaviour
{
	// Token: 0x06000057 RID: 87 RVA: 0x000025F6 File Offset: 0x000007F6
	private void Awake()
	{
		this.t = base.transform;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002604 File Offset: 0x00000804
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.evalX = 0f;
		this.evalY = 0f;
		this.evalZ = 0f;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00009090 File Offset: 0x00007290
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (num <= this.GraphTimeMultiplier.x)
		{
			this.evalX = this.ScaleCurveX.Evaluate(num / this.GraphTimeMultiplier.x) * this.GraphScaleMultiplier.x;
		}
		if (num <= this.GraphTimeMultiplier.y)
		{
			this.evalY = this.ScaleCurveY.Evaluate(num / this.GraphTimeMultiplier.y) * this.GraphScaleMultiplier.y;
		}
		if (num <= this.GraphTimeMultiplier.z)
		{
			this.evalZ = this.ScaleCurveZ.Evaluate(num / this.GraphTimeMultiplier.z) * this.GraphScaleMultiplier.z;
		}
		this.t.localScale = new Vector3(this.evalX, this.evalY, this.evalZ);
	}

	// Token: 0x04000075 RID: 117
	public AnimationCurve ScaleCurveX = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000076 RID: 118
	public AnimationCurve ScaleCurveY = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000077 RID: 119
	public AnimationCurve ScaleCurveZ = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x04000078 RID: 120
	public Vector3 GraphTimeMultiplier = Vector3.one;

	// Token: 0x04000079 RID: 121
	public Vector3 GraphScaleMultiplier = Vector3.one;

	// Token: 0x0400007A RID: 122
	private float startTime;

	// Token: 0x0400007B RID: 123
	private Transform t;

	// Token: 0x0400007C RID: 124
	private float evalX;

	// Token: 0x0400007D RID: 125
	private float evalY;

	// Token: 0x0400007E RID: 126
	private float evalZ;
}
