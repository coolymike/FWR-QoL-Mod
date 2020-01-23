using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class IgnorePlayerControllerCollision : MonoBehaviour
{
	// Token: 0x06000457 RID: 1111 RVA: 0x000184D8 File Offset: 0x000166D8
	private void Start()
	{
		this.colliders = base.GetComponentsInChildren<Collider>();
		for (int i = 0; i < this.colliders.Length; i++)
		{
			Physics.IgnoreCollision(this.colliders[i], PlayerSettings.instance.playerController.GetComponent<Collider>());
		}
	}

	// Token: 0x040005C0 RID: 1472
	private Collider[] colliders;
}
