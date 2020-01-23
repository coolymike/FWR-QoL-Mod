using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class Camera_SeeTargetCheck : MonoBehaviour
{
	// Token: 0x06000260 RID: 608 RVA: 0x000041A2 File Offset: 0x000023A2
	private void Start()
	{
		Debug.Log(true);
		this.cam = base.gameObject.GetComponent<Camera>();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00011544 File Offset: 0x0000F744
	private void LateUpdate()
	{
		Vector3 vector = base.gameObject.GetComponent<Camera>().WorldToViewportPoint(this.target.transform.position);
		if (vector.z > 0f && vector.x > 0f && vector.x < 1f && vector.y > 0f)
		{
			bool flag = vector.y < 1f;
		}
		if (this.RayCanSeeObject(this.CastRay(this.cam.ScreenToWorldPoint(new Vector3(0f, 0f, this.cam.nearClipPlane)))) && this.RayCanSeeObject(this.CastRay(this.cam.ScreenToWorldPoint(new Vector3((float)Screen.width, (float)Screen.height, this.cam.nearClipPlane)))) && this.RayCanSeeObject(this.CastRay(this.cam.ScreenToWorldPoint(new Vector3(0f, (float)Screen.height, this.cam.nearClipPlane)))) && this.RayCanSeeObject(this.CastRay(this.cam.ScreenToWorldPoint(new Vector3((float)Screen.width, 0f, this.cam.nearClipPlane)))))
		{
			this.canSeeTarget = true;
			this.hitDistance = this.CastRay(this.cam.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), this.cam.nearClipPlane))).distance;
			return;
		}
		this.canSeeTarget = false;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x000116DC File Offset: 0x0000F8DC
	private void VisionCheck()
	{
		Vector3 normalized = (this.target.transform.position - base.transform.position).normalized;
		RaycastHit raycastHit;
		if (Physics.Linecast(base.transform.position, this.target.GetComponentInChildren<Renderer>().bounds.center, out raycastHit))
		{
			if (raycastHit.transform.gameObject.layer != LayerMask.NameToLayer("Ragdoll Controller"))
			{
				Debug.Log(this.target.name + " occluded by " + raycastHit.transform.name);
				return;
			}
			Debug.Log(this.target.name + " NOT occluded by " + raycastHit.transform.name);
		}
	}

	// Token: 0x06000263 RID: 611 RVA: 0x000117A8 File Offset: 0x0000F9A8
	private RaycastHit CastRay(Vector3 fromPosition)
	{
		Vector3 normalized = (this.target.transform.position - fromPosition).normalized;
		RaycastHit result;
		Physics.Linecast(fromPosition, this.target.transform.position, out result);
		return result;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x000117F0 File Offset: 0x0000F9F0
	private bool RayCanSeeObject(RaycastHit hit)
	{
		return !(hit.transform != null) || hit.transform.gameObject.layer == LayerMask.NameToLayer("Ragdoll") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Ragdoll Controller");
	}

	// Token: 0x0400036A RID: 874
	public GameObject target;

	// Token: 0x0400036B RID: 875
	private Camera cam;

	// Token: 0x0400036C RID: 876
	public bool canSeeTarget;

	// Token: 0x0400036D RID: 877
	public float hitDistance;
}
