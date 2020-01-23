using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class PlayerHandGrabber : MonoBehaviour
{
	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002E17 File Offset: 0x00001017
	// (set) Token: 0x060000F6 RID: 246 RVA: 0x00002E1F File Offset: 0x0000101F
	private bool IsGrabbing
	{
		get
		{
			return this.isGrabbing;
		}
		set
		{
			this.isGrabbing = value;
			if (!value)
			{
				this.transformGrabbed = null;
				this.handHinge.connectedBody = null;
			}
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00002E3E File Offset: 0x0000103E
	private void Start()
	{
		this.oldHandParent = this.handHinge.transform.parent;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000B5A0 File Offset: 0x000097A0
	private void OnTriggerStay(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Player))
		{
			return;
		}
		if (this.IsGrabbing && this.transformGrabbed == null)
		{
			this.handHinge.transform.SetParent(null);
			this.handHinge.transform.SetParent(other.transform);
			this.localGrabPosition = this.handHinge.transform.localPosition;
			this.handHinge.transform.SetParent(null);
			this.handHinge.transform.SetParent(this.oldHandParent);
			this.handHinge.connectedBody = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftElbow).GetComponent<Rigidbody>();
			this.transformGrabbed = other.transform;
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000B670 File Offset: 0x00009870
	private void HoldGrabTransform()
	{
		if (!this.IsGrabbing)
		{
			return;
		}
		if (this.transformGrabbed == null)
		{
			return;
		}
		this.handHinge.transform.SetParent(null);
		this.handHinge.transform.SetParent(this.transformGrabbed);
		this.handHinge.transform.localPosition = this.localGrabPosition;
		this.handHinge.transform.SetParent(null);
		this.handHinge.transform.SetParent(this.oldHandParent);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00002E56 File Offset: 0x00001056
	private void Update()
	{
		if (this.playerSettings.simulatedRagdoll.ragdollModeEnabled)
		{
			this.IsGrabbing = Input.GetKey(KeyCode.Mouse0);
			return;
		}
		this.IsGrabbing = false;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00002E82 File Offset: 0x00001082
	private void FixedUpdate()
	{
		this.HoldGrabTransform();
	}

	// Token: 0x0400016F RID: 367
	public PlayerSettings playerSettings;

	// Token: 0x04000170 RID: 368
	public HingeJoint handHinge;

	// Token: 0x04000171 RID: 369
	private Transform transformGrabbed;

	// Token: 0x04000172 RID: 370
	private bool isGrabbing;

	// Token: 0x04000173 RID: 371
	private Transform oldHandParent;

	// Token: 0x04000174 RID: 372
	public Vector3 localGrabPosition;

	// Token: 0x04000175 RID: 373
	private Vector3 startPosition;
}
