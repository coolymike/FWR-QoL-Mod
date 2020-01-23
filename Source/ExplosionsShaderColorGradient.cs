using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class ExplosionsShaderColorGradient : MonoBehaviour
{
	// Token: 0x0600005B RID: 91 RVA: 0x000091FC File Offset: 0x000073FC
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
		this.oldColor = this.matInstance.GetColor(this.propertyID);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002632 File Offset: 0x00000832
	private void OnEnable()
	{
		this.startTime = Time.time;
		this.canUpdate = true;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00009290 File Offset: 0x00007490
	private void Update()
	{
		float num = Time.time - this.startTime;
		if (this.canUpdate)
		{
			Color a = this.Color.Evaluate(num / this.TimeMultiplier);
			this.matInstance.SetColor(this.propertyID, a * this.oldColor);
		}
		if (num >= this.TimeMultiplier)
		{
			this.canUpdate = false;
		}
	}

	// Token: 0x0400007F RID: 127
	public string ShaderProperty = "_TintColor";

	// Token: 0x04000080 RID: 128
	public int MaterialID;

	// Token: 0x04000081 RID: 129
	public Gradient Color = new Gradient();

	// Token: 0x04000082 RID: 130
	public float TimeMultiplier = 1f;

	// Token: 0x04000083 RID: 131
	private bool canUpdate;

	// Token: 0x04000084 RID: 132
	private Material matInstance;

	// Token: 0x04000085 RID: 133
	private int propertyID;

	// Token: 0x04000086 RID: 134
	private float startTime;

	// Token: 0x04000087 RID: 135
	private Color oldColor;
}
