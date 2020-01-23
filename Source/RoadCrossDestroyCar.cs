using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class RoadCrossDestroyCar : MonoBehaviour
{
	// Token: 0x06000424 RID: 1060 RVA: 0x00005787 File Offset: 0x00003987
	private void OnTriggerEnter(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Vehicle))
		{
			other.transform.root.gameObject.SetActive(false);
		}
	}
}
