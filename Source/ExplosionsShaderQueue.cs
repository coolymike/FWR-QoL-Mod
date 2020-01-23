using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ExplosionsShaderQueue : MonoBehaviour
{
	// Token: 0x06000063 RID: 99 RVA: 0x00009424 File Offset: 0x00007624
	private void Start()
	{
		this.rend = base.GetComponent<Renderer>();
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue += this.AddQueue;
			return;
		}
		base.Invoke("SetProjectorQueue", 0.1f);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00002683 File Offset: 0x00000883
	private void SetProjectorQueue()
	{
		base.GetComponent<Projector>().material.renderQueue += this.AddQueue;
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000026A2 File Offset: 0x000008A2
	private void OnDisable()
	{
		if (this.rend != null)
		{
			this.rend.sharedMaterial.renderQueue = -1;
		}
	}

	// Token: 0x04000091 RID: 145
	public int AddQueue = 1;

	// Token: 0x04000092 RID: 146
	private Renderer rend;
}
