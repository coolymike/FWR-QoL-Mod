using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class FanWind : MonoBehaviour
{
	// Token: 0x06000207 RID: 519 RVA: 0x00003D79 File Offset: 0x00001F79
	private void Start()
	{
		if (this.rotatingObject != null)
		{
			this.rotatingObject.rotationSpeed = this.windForce * -7.3f;
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00003DA0 File Offset: 0x00001FA0
	private void OnTriggerStay(Collider other)
	{
		this.WindPush(other);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x000101EC File Offset: 0x0000E3EC
	private void WindPush(Collider other)
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if ((this.rigidbody = other.GetComponent<Rigidbody>()) == null)
		{
			return;
		}
		this.rigidbody.AddForce(base.transform.forward * this.windForce * this.DistanceStrength(other.transform), ForceMode.Acceleration);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0001024C File Offset: 0x0000E44C
	private float DistanceStrength(Transform tObject)
	{
		return 1f - 0.5f / base.transform.localScale.z * (base.transform.localScale.z - Vector3.Distance(tObject.position, base.transform.position));
	}

	// Token: 0x040002F0 RID: 752
	public RotatingObject rotatingObject;

	// Token: 0x040002F1 RID: 753
	public float windForce = 45f;

	// Token: 0x040002F2 RID: 754
	private Rigidbody rigidbody;
}
