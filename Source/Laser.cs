using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class Laser : MonoBehaviour
{
	// Token: 0x06000223 RID: 547 RVA: 0x00010884 File Offset: 0x0000EA84
	private void FixedUpdate()
	{
		if (Physics.Raycast(base.transform.position, base.transform.forward, out this.hit, float.PositiveInfinity, this.laserLayerMask) && this.laserOn)
		{
			this.laserDistance = this.hit.distance;
			RagdollSettings.DismemberJoint(this.hit.transform);
			this.CheckForMine(this.hit.transform);
			this.audioSourceBeamHit.volume = 1f;
			this.audioSourceBeamHit.transform.position = this.hit.point;
		}
		else
		{
			this.audioSourceBeamHit.volume = 0f;
			this.laserDistance = 2000f;
		}
		if (this.laserOn)
		{
			this.laserLight.localPosition = new Vector3(0f, 0f, this.laserDistance);
			this.laserBeam.SetPosition(1, new Vector3(0f, 0f, this.laserDistance));
		}
		this.audioSourceBeamHit.gameObject.SetActive(this.laserOn);
		this.audioSourceBeam.gameObject.SetActive(this.laserOn);
		this.laserLight.gameObject.SetActive(this.laserOn);
		this.laserBeam.gameObject.SetActive(this.laserOn);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00003EE3 File Offset: 0x000020E3
	private void CheckForMine(Transform hitTransform)
	{
		this.anotherMine = hitTransform.GetComponentInParent<Mine>();
		if (this.anotherMine != null)
		{
			this.anotherMine.remoteDetonate = true;
		}
	}

	// Token: 0x04000318 RID: 792
	private RaycastHit hit;

	// Token: 0x04000319 RID: 793
	private float laserDistance;

	// Token: 0x0400031A RID: 794
	public Transform laserLight;

	// Token: 0x0400031B RID: 795
	public LineRenderer laserBeam;

	// Token: 0x0400031C RID: 796
	public bool laserOn;

	// Token: 0x0400031D RID: 797
	[Header("Audio")]
	public AudioSource audioSourceBeam;

	// Token: 0x0400031E RID: 798
	public AudioSource audioSourceBeamHit;

	// Token: 0x0400031F RID: 799
	private LayerMask laserLayerMask = -8709;

	// Token: 0x04000320 RID: 800
	private Mine anotherMine;

	// Token: 0x04000321 RID: 801
	private ConfigurableJoint cJoint;
}
