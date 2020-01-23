using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class ExplosionsLightCurves : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x0000255B File Offset: 0x0000075B
	private void Awake()
	{
		this.lightSource = base.GetComponent<Light>();
		this.lightSource.intensity = this.LightCurve.Evaluate(0f);
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00002584 File Offset: 0x00000784
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00009034 File Offset: 0x00007234
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float intensity = this.LightCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphIntensityMultiplier;
			this.lightSource.intensity = intensity;
		}
		if (num >= this.GraphTimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x0400006D RID: 109
	public AnimationCurve LightCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x0400006E RID: 110
	public float GraphTimeMultiplier = 1f;

	// Token: 0x0400006F RID: 111
	public float GraphIntensityMultiplier = 1f;

	// Token: 0x04000070 RID: 112
	private bool canUpdate;

	// Token: 0x04000071 RID: 113
	private float startTime;

	// Token: 0x04000072 RID: 114
	private Light lightSource;
}
