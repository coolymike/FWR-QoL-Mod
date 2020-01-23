using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class ExplosionDemoReactivator : MonoBehaviour
{
	// Token: 0x0600003D RID: 61 RVA: 0x000023DC File Offset: 0x000005DC
	private void Start()
	{
		base.InvokeRepeating("Reactivate", 0f, this.TimeDelayToReactivate);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00008DD4 File Offset: 0x00006FD4
	private void Reactivate()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>())
		{
			transform.gameObject.SetActive(false);
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000058 RID: 88
	public float TimeDelayToReactivate = 3f;
}
