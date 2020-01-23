using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[RequireComponent(typeof(PlayerSettings))]
public class PlayerGrabbing : MonoBehaviour
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060000A8 RID: 168 RVA: 0x0000A0C0 File Offset: 0x000082C0
	// (remove) Token: 0x060000A9 RID: 169 RVA: 0x0000A0F8 File Offset: 0x000082F8
	public event PlayerGrabbing.GrabToggleHandler onGrabToggle;

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000AA RID: 170 RVA: 0x00002A6A File Offset: 0x00000C6A
	// (set) Token: 0x060000AB RID: 171 RVA: 0x00002A72 File Offset: 0x00000C72
	public bool Grabbing
	{
		get
		{
			return this.grabbing;
		}
		set
		{
			if (value == this.grabbing)
			{
				return;
			}
			this.grabbing = value;
			if (this.onGrabToggle != null)
			{
				this.onGrabToggle(value);
			}
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x00002A99 File Offset: 0x00000C99
	private void Start()
	{
		this.playerSettings = base.GetComponent<PlayerSettings>();
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00002AA7 File Offset: 0x00000CA7
	private void Update()
	{
		this.InputGrabCheck();
		this.KillSmartRagdollOnGrab();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000A130 File Offset: 0x00008330
	private void InputGrabCheck()
	{
		if (!InputManager.Grabbing())
		{
			this.Grabbing = false;
			return;
		}
		if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.Grabbing = false;
			return;
		}
		if (this.focusRigidbody == null)
		{
			this.Grabbing = false;
			return;
		}
		if (this.playerSettings.vehicleController.Mounted)
		{
			this.Grabbing = false;
			return;
		}
		if (this.focusRigidbody.isKinematic)
		{
			this.Grabbing = false;
			return;
		}
		if (!this.focusRigidbody.gameObject.activeInHierarchy)
		{
			this.Grabbing = false;
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			this.Grabbing = false;
			return;
		}
		if (this.playerSettings.mainCamera.target != CameraController.Target.Player)
		{
			this.Grabbing = false;
			return;
		}
		this.Grabbing = true;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00002AB5 File Offset: 0x00000CB5
	private void FixedUpdate()
	{
		this.GrabCheck();
		if (this.grabbing)
		{
			this.GrabObject();
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000A1FC File Offset: 0x000083FC
	private void GrabObject()
	{
		if (this.focusRigidbody == null)
		{
			return;
		}
		this.grabPosition = this.playerSettings.mainCamera.transform.position + this.playerSettings.mainCamera.transform.forward * this.grabDistance;
		this.distance = Vector3.Distance(this.grabPosition, this.focusRigidbody.transform.position);
		this.direction = Vector3.Normalize(this.grabPosition - this.focusRigidbody.transform.position);
		this.focusRigidbody.velocity = this.direction * this.distance * this.moveStrength;
		this.focusRigidbody.AddForce(this.direction * this.distance * this.moveStrength);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0000A2F0 File Offset: 0x000084F0
	private void GrabCheck()
	{
		if (Physics.Raycast(this.playerSettings.mainCamera.transform.position, this.playerSettings.mainCamera.transform.TransformDirection(Vector3.forward), out this.hit, this.grabReachDistance, -16385))
		{
			if (!Tag.Compare(this.hit.transform, Tag.Tags.Player) && !this.grabbing)
			{
				this.focusRigidbody = this.hit.rigidbody;
				return;
			}
		}
		else if (!this.grabbing)
		{
			this.focusRigidbody = null;
		}
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000A380 File Offset: 0x00008580
	private void KillSmartRagdollOnGrab()
	{
		if (this.grabbing && this.focusRigidbody != null && Tag.Compare(this.focusRigidbody.transform, Tag.Tags.Ragdoll) && !this.killedSmartRagdoll)
		{
			AntiTouchSystem.KillSmartRagdoll(this.focusRigidbody.transform);
			this.killedSmartRagdoll = true;
			return;
		}
		this.killedSmartRagdoll = false;
	}

	// Token: 0x04000118 RID: 280
	private PlayerSettings playerSettings;

	// Token: 0x0400011A RID: 282
	private bool grabbing;

	// Token: 0x0400011B RID: 283
	public Rigidbody focusRigidbody;

	// Token: 0x0400011C RID: 284
	public float grabDistance = 6f;

	// Token: 0x0400011D RID: 285
	public float grabReachDistance = 8f;

	// Token: 0x0400011E RID: 286
	public float moveStrength = 25f;

	// Token: 0x0400011F RID: 287
	private Vector3 grabPosition;

	// Token: 0x04000120 RID: 288
	private float distance;

	// Token: 0x04000121 RID: 289
	private Vector3 direction;

	// Token: 0x04000122 RID: 290
	private RaycastHit hit;

	// Token: 0x04000123 RID: 291
	private bool killedSmartRagdoll;

	// Token: 0x0200002F RID: 47
	// (Invoke) Token: 0x060000B5 RID: 181
	public delegate void GrabToggleHandler(bool grabbingStatus);
}
