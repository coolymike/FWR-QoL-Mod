using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class PlayerController : MonoBehaviour
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060000CF RID: 207
	// (remove) Token: 0x060000D0 RID: 208
	public event PlayerController.ToggleHandler OnGroundedToggle;

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000D1 RID: 209
	// (set) Token: 0x060000D2 RID: 210
	public bool IsGrounded
	{
		get
		{
			return this.isGrounded;
		}
		set
		{
			if (value == this.isGrounded)
			{
				return;
			}
			this.isGrounded = value;
			PlayerController.ToggleHandler onGroundedToggle = this.OnGroundedToggle;
			if (onGroundedToggle == null)
			{
				return;
			}
			onGroundedToggle(value);
		}
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060000D3 RID: 211
	// (remove) Token: 0x060000D4 RID: 212
	public event PlayerController.ToggleHandler OnFlyToggle;

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000D5 RID: 213
	// (set) Token: 0x060000D6 RID: 214
	public bool FlyMode
	{
		get
		{
			return this.flyMode;
		}
		set
		{
			if (value == this.flyMode)
			{
				return;
			}
			this.flyMode = value;
			PlayerController.ToggleHandler onFlyToggle = this.OnFlyToggle;
			if (onFlyToggle == null)
			{
				return;
			}
			onFlyToggle(value);
		}
	}

	// Token: 0x060000D7 RID: 215
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.controller = base.GetComponent<CharacterController>();
		this.playerSettings.simulatedRagdoll.OnRagdollToggle += this.OnRagdollToggle;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		this.OnGroundedToggle += this.GroundedToggle;
	}

	// Token: 0x060000D8 RID: 216
	private void OnDestroy()
	{
		this.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.OnRagdollToggle;
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
		this.OnGroundedToggle -= this.GroundedToggle;
	}

	// Token: 0x060000D9 RID: 217
	private void Update()
	{
		this.velocityXZ = new Vector3(this.controller.velocity.x, 0f, this.controller.velocity.z).magnitude / this.speed;
		this.velocityY = new Vector3(0f, this.controller.velocity.y, 0f).magnitude / this.gravity;
		if (PlayerSettings.disablePlayerControlls || !this.enableMovement)
		{
			this.horizontal = 0f;
			this.vertical = 0f;
			return;
		}
		this.horizontal = InputManager.MoveLerp(!this.FlyMode).x * this.moveMultiplier * (this.FlyMode ? 1.4f : 1f);
		this.vertical = InputManager.MoveLerp(!this.FlyMode).y * this.moveMultiplier * (this.FlyMode ? 1.4f : 1f);
	}

	// Token: 0x060000DA RID: 218
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (!buildModeOn)
		{
			if (!InputManager.FlyInPlayMode()) {
				this.FlyMode = false;
			}
		}
	}

	// Token: 0x060000DB RID: 219
	private void OnRagdollToggle(bool ragdollModeEnabled)
	{
		this.moveDirection.y = 0f;
		this.controller.enabled = !ragdollModeEnabled;
		if (!this.IsGrounded)
		{
			this.ragdollBeforeGrounded = true;
		}
		if (ragdollModeEnabled)
		{
			this.FlyMode = false;
		}
	}

	// Token: 0x060000DC RID: 220
	private void GroundedToggle(bool grounded)
	{
		if (LevelManager.BuildModeOn)
		{
			this.lastGroundedPosition = this.controller.transform.position;
			return;
		}
		if (grounded)
		{
			if (this.ragdollBeforeGrounded)
			{
				return;
			}
			if (this.lastGroundedPosition.y - this.controller.transform.position.y > 19.3f)
			{
				this.playerSettings.simulatedRagdoll.RagdollModeEnabled = true;
				return;
			}
		}
		else
		{
			this.lastGroundedPosition = this.controller.transform.position;
			this.ragdollBeforeGrounded = false;
		}
	}

	// Token: 0x060000DD RID: 221
	private void FixedUpdate()
	{
		this.gravity = Mathf.Abs(Physics.gravity.y * 1.4f);
		if (InputManager.FlyInPlayMode())
		{
			if (InputManager.Jump() && !this.FlyMode && !this.isGrounded && this.enableFlying && !this.playerSettings.simulatedRagdoll.RagdollModeEnabled && LevelManager.BuildModeOn)
			{
				this.FlyMode = !this.FlyMode;
			}
		}
		else if (InputManager.Jump() && !this.FlyMode && !this.isGrounded && this.enableFlying && !this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.FlyMode = !this.FlyMode;
		}
		if (!this.playerSettings.simulatedRagdoll.RagdollModeEnabled && this.controller.enabled)
		{
			this.MoveController();
			this.controller.transform.eulerAngles = new Vector3(0f, this.playerSettings.mainCamera.transform.eulerAngles.y, 0f);
		}
	}

	// Token: 0x060000DE RID: 222
	private void MoveController()
	{
		this.IsGrounded = this.controller.isGrounded;
		if (this.IsGrounded || this.FlyMode)
		{
			this.moveMultiplier = 1f;
		}
		else
		{
			this.moveMultiplier = Mathf.Lerp(this.moveMultiplier, 0f, 0.3f * Time.fixedDeltaTime);
		}
		if (this.controller.isGrounded)
		{
			this.FlyMode = false;
			this.moveDirection = new Vector3(this.horizontal, 0f, this.vertical);
			this.moveDirection *= this.speed;
			if (!this.isJumping)
			{
				this.moveDirection.y = -10f;
			}
			if (InputManager.Jumping() && !PlayerSettings.disablePlayerControlls && !this.isJumping && this.enableJumping)
			{
				base.StartCoroutine(this.Jump());
			}
			this.gravityReset = false;
		}
		else
		{
			if (!this.gravityReset && !this.isJumping)
			{
				this.moveDirection.y = 0f;
				this.gravityReset = true;
			}
			this.moveDirection.x = this.horizontal * this.speed;
			this.moveDirection.z = this.vertical * this.speed;
		}
		this.moveDirection = this.controller.transform.TransformDirection(this.moveDirection);
		if (this.FlyMode)
		{
			if (InputManager.LeftShift())
			{
				this.moveDirection.y = Mathf.Lerp(this.moveDirection.y, -12f, 5f * Time.deltaTime);
			}
			else if (InputManager.Jumping())
			{
				this.moveDirection.y = Mathf.Lerp(this.moveDirection.y, 12f, 5f * Time.deltaTime);
			}
			else
			{
				this.moveDirection.y = Mathf.Lerp(this.moveDirection.y, 0f, 10f * Time.deltaTime);
			}
		}
		else
		{
			this.moveDirection.y = this.moveDirection.y - this.gravity * 1.3f * Time.deltaTime;
		}
		this.moveDirection.y = Mathf.Clamp(this.moveDirection.y, this.gravity * -3f, float.PositiveInfinity);
		this.controller.Move(this.moveDirection * Time.deltaTime);
	}

	// Token: 0x060000DF RID: 223
	private IEnumerator Jump()
	{
		this.moveDirection.y = this.jumpSpeed;
		this.isJumping = true;
		yield return new WaitForFixedUpdate();
		yield return new WaitUntil(() => this.controller.isGrounded);
		yield return new WaitForSeconds(this.jumpCooldown);
		this.isJumping = false;
		yield break;
	}

	// Token: 0x060000E0 RID: 224
	public PlayerController()
	{
	}

	// Token: 0x0400014D RID: 333
	private PlayerSettings playerSettings;

	// Token: 0x0400014E RID: 334
	public CharacterController controller;

	// Token: 0x0400014F RID: 335
	public bool enableMovement = true;

	// Token: 0x04000150 RID: 336
	public bool enableJumping = true;

	// Token: 0x04000151 RID: 337
	public bool enableFlying = true;

	// Token: 0x04000152 RID: 338
	public float speed = 9f;

	// Token: 0x04000153 RID: 339
	private readonly float jumpSpeed = 12.5f;

	// Token: 0x04000154 RID: 340
	private float gravity;

	// Token: 0x04000155 RID: 341
	private float moveMultiplier = 1f;

	// Token: 0x04000156 RID: 342
	private bool gravityReset;

	// Token: 0x04000158 RID: 344
	private bool isGrounded;

	// Token: 0x04000159 RID: 345
	private bool isJumping;

	// Token: 0x0400015A RID: 346
	private readonly float jumpCooldown = 0.1f;

	// Token: 0x0400015C RID: 348
	private bool flyMode;

	// Token: 0x0400015D RID: 349
	public float velocityXZ;

	// Token: 0x0400015E RID: 350
	public float velocityY;

	// Token: 0x0400015F RID: 351
	private Vector3 moveDirection = Vector3.zero;

	// Token: 0x04000160 RID: 352
	private float horizontal;

	// Token: 0x04000161 RID: 353
	private float vertical;

	// Token: 0x04000162 RID: 354
	private bool ragdollBeforeGrounded;

	// Token: 0x04000163 RID: 355
	private Vector3 lastGroundedPosition;

	// Token: 0x02000034 RID: 52
	// (Invoke) Token: 0x060000E3 RID: 227
	public delegate void ToggleHandler(bool enabled);
}
