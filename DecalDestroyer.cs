using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class DecalDestroyer : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002114 File Offset: 0x00000314
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(this.lifeTime);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000001 RID: 1
	public float lifeTime = 5f;
}
