using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class ExplosionsShaderFloatCurves : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x000092F4 File Offset: 0x000074F4
	private void Start()
	{
		Material[] materials = base.GetComponent<Renderer>().materials;
		if (this.MaterialID >= materials.Length)
		{
			Debug.Log("ShaderColorGradient: Material ID more than shader materials count.");
		}
		this.matInstance = materials[this.MaterialID];
		if (!this.matInstance.HasProperty(this.ShaderProperty))
		{
			Debug.Log("ShaderColorGradient: Shader not have \"" + this.ShaderProperty + "\" property");
		}
		this.propertyID = Shader.PropertyToID(this.ShaderProperty);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000266F File Offset: 0x0000086F
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00009370 File Offset: 0x00007570
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			float value = this.FloatPropertyCurve.Evaluate(num / this.GraphTimeMultiplier) * this.GraphScaleMultiplier;
			this.matInstance.SetFloat(this.propertyID, value);
		}
		if (num >= this.GraphTimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x04000088 RID: 136
	public string ShaderProperty = "_BumpAmt";

	// Token: 0x04000089 RID: 137
	public int MaterialID;

	// Token: 0x0400008A RID: 138
	public AnimationCurve FloatPropertyCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	// Token: 0x0400008B RID: 139
	public float GraphTimeMultiplier = 1f;

	// Token: 0x0400008C RID: 140
	public float GraphScaleMultiplier = 1f;

	// Token: 0x0400008D RID: 141
	private bool canUpdate;

	// Token: 0x0400008E RID: 142
	private Material matInstance;

	// Token: 0x0400008F RID: 143
	private int propertyID;

	// Token: 0x04000090 RID: 144
	private float startTime;
}
