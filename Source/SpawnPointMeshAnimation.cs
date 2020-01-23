using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class SpawnPointMeshAnimation : MonoBehaviour
{
	// Token: 0x06000287 RID: 647 RVA: 0x00012AE8 File Offset: 0x00010CE8
	private void Start()
	{
		this.startPosition = base.transform.localPosition;
		this.startScale = base.transform.localScale;
		base.transform.localPosition = new Vector3(this.startPosition.x, 0f, this.startPosition.z);
		base.transform.localScale = new Vector3(this.startScale.x, 0f, this.startScale.z);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00012B70 File Offset: 0x00010D70
	private void Update()
	{
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.startPosition, this.animationSpeed * Time.deltaTime);
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.startScale, this.animationSpeed * Time.deltaTime);
	}

	// Token: 0x040003D3 RID: 979
	public float animationSpeed = 2f;

	// Token: 0x040003D4 RID: 980
	private Vector3 startPosition;

	// Token: 0x040003D5 RID: 981
	private Vector3 startScale;
}
