using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[ExecuteInEditMode]
public class ExplosionsParticleSystemScaler : MonoBehaviour
{
	// Token: 0x06000055 RID: 85 RVA: 0x000025D5 File Offset: 0x000007D5
	private void Start()
	{
		this.oldScale = this.particlesScale;
	}

	// Token: 0x04000073 RID: 115
	public float particlesScale = 1f;

	// Token: 0x04000074 RID: 116
	private float oldScale;
}
