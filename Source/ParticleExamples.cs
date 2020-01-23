using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[Serializable]
public class ParticleExamples
{
	// Token: 0x04000028 RID: 40
	public string title;

	// Token: 0x04000029 RID: 41
	[TextArea]
	public string description;

	// Token: 0x0400002A RID: 42
	public bool isWeaponEffect;

	// Token: 0x0400002B RID: 43
	public GameObject particleSystemGO;

	// Token: 0x0400002C RID: 44
	public Vector3 particlePosition;

	// Token: 0x0400002D RID: 45
	public Vector3 particleRotation;
}
