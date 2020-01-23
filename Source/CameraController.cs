using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class CameraController : MonoBehaviour
{
	// Token: 0x06000266 RID: 614 RVA: 0x0001184C File Offset: 0x0000FA4C
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		base.gameObject.AddComponent(typeof(CinematicCamera));
		this.ragdollMesh = this.playerSettings.simulatedRagdoll.bodyElements.ragdollMesh.GetComponent<SkinnedMeshRenderer>();
		this.tremorInitialAmount = this.tremorAmount;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x000118B4 File Offset: 0x0000FAB4
	private void Update()
	{
		this.RagdollVisibility();
		if (this.playerSettings.vehicleController.Mounted)
		{
			this.targetVehicleCam = this.playerSettings.vehicleController.focusVehicle.povCamera;
			this.target = CameraController.Target.Vehicle;
			return;
		}
		if (!this.playerSettings.vehicleController.Mounted && this.target == CameraController.Target.Vehicle)
		{
			this.target = CameraController.Target.Player;
		}
		if (InputManager.CameraToggle())
		{
			if (this.CinematicCameraToggle)
			{
				this.CinematicCameraToggle = false;
				this.target = CameraController.Target.Player;
			}
			else
			{
				this.CinematicCameraToggle = true;
			}
		}
		if (this.CinematicCameraToggle)
		{
			this.target = CameraController.Target.Cinematic;
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00011954 File Offset: 0x0000FB54
	private void LateUpdate()
	{
		if (this.target == CameraController.Target.Player)
		{
			if (!this.playerSettings.simulatedRagdoll.RagdollModeEnabled && !this.toggledRagdollMode)
			{
				this.transitionSpeed = 0f;
				this.toggledRagdollMode = true;
			}
			else if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				this.toggledRagdollMode = false;
			}
		}
		else if (this.target == CameraController.Target.Cinematic && (InputManager.NewCameraAngle() || InputManager.CameraToggle()))
		{
			this.transitionSpeed = 0f;
		}
		else if (this.target == CameraController.Target.BodyCam)
		{
			this.transitionSpeed = 100f;
		}
		this.transitionSpeed = Mathf.Lerp(this.transitionSpeed, this.targetTransitionSpeed, Time.deltaTime * 1f);
		if (GameData.settings.CameraShake)
		{
			this.Handheld();
			this.Tremor();
			this.JumpShake();
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00011A2C File Offset: 0x0000FC2C
	private void FixedUpdate()
	{
		switch (this.target)
		{
		case CameraController.Target.Player:
			this.targetCam = this.targetPlayerCam;
			break;
		case CameraController.Target.Cinematic:
			this.targetCam = this.targetCinematicCam;
			break;
		case CameraController.Target.Vehicle:
			this.targetCam = this.targetVehicleCam;
			break;
		case CameraController.Target.BodyCam:
			this.targetCam = this.targetBodyCam;
			break;
		default:
			this.targetCam = null;
			break;
		}
		this.UpdateCameraTransform();
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00011AA0 File Offset: 0x0000FCA0
	private void RagdollVisibility()
	{
		this.ragdollMesh.enabled = (this.target == CameraController.Target.BodyCam || Vector3.Distance(this.playerSettings.simulatedRagdoll.bodyElements.AverageJointPosition(), base.transform.position) > 0.9f);
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00011AF0 File Offset: 0x0000FCF0
	private void UpdateCameraTransform()
	{
		if (this.targetCam == null)
		{
			return;
		}
		if (this.target == CameraController.Target.Vehicle)
		{
			base.transform.position = this.targetCam.position;
			this.rotationLayerTarget = Quaternion.Slerp(base.transform.rotation, this.targetCam.parent.rotation, Time.deltaTime * this.transitionSpeed);
		}
		else
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.targetCam.position, Time.fixedDeltaTime * this.transitionSpeed);
			this.rotationLayerTarget = Quaternion.Slerp(base.transform.rotation, this.targetCam.rotation, Time.fixedDeltaTime * this.transitionSpeed);
		}
		base.transform.rotation = Quaternion.Euler(this.rotationLayerTarget.eulerAngles + this.rotationLayerHandheld.eulerAngles + this.rotationLayerTremor.eulerAngles + this.rotationLayerJumpShake.eulerAngles);
	}

	// Token: 0x0600026C RID: 620 RVA: 0x000041C0 File Offset: 0x000023C0
	private float ShakeFormula(float time, float amount, float seed)
	{
		return (Mathf.PerlinNoise(time, seed) * 2f - 1f) * amount;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00011C0C File Offset: 0x0000FE0C
	private void Handheld()
	{
		this.handheldMovement = Mathf.Abs(InputManager.Move(true).magnitude) * 0.5f;
		this.handheldTime += Time.deltaTime * Mathf.Clamp(this.handheldMovement * this.handheldMovementMultiplier * this.handheldMovementSpeedMultiplier, 1f, 10f);
		this.rotationLayerHandheld.eulerAngles = new Vector3(this.ShakeFormula(this.handheldTime * this.handheldSpeed, this.handheldAmount + this.handheldMovement * this.handheldMovementMultiplier, 0.1f), this.ShakeFormula(this.handheldTime * this.handheldSpeed, this.handheldAmount + this.handheldMovement * this.handheldMovementMultiplier, 0.6f), this.ShakeFormula(this.handheldTime * this.handheldSpeed, this.handheldAmount + this.handheldMovement * this.handheldMovementMultiplier, 0.4f));
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00011D04 File Offset: 0x0000FF04
	private void Tremor()
	{
		if (this.tremorTrigger)
		{
			this.tremorDurationTime = 1f;
			this.tremorTrigger = false;
		}
		this.tremorTime += Time.deltaTime * this.tremorSpeed;
		this.tremorDurationTime = Mathf.Lerp(this.tremorDurationTime, 0f, Time.deltaTime * this.tremorDuration);
		this.tremorSin = Mathf.Sin(this.tremorDurationTime / 1f * 3.14159274f);
		this.rotationLayerTremor.eulerAngles = new Vector3(this.ShakeFormula(this.tremorTime, this.tremorAmount * this.tremorSin, 0.2f), this.ShakeFormula(this.tremorTime, this.tremorAmount * this.tremorSin, 0.5f), this.ShakeFormula(this.tremorTime, this.tremorAmount * this.tremorSin, 0.8f));
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00011DF0 File Offset: 0x0000FFF0
	private void JumpShake()
	{
		if (this.jumpShakeTrigger)
		{
			this.jumpShakeDurationTime = 1f;
			this.jumpShakeTrigger = false;
		}
		this.jumpShakeTime += Time.deltaTime * this.jumpShakeSpeed;
		this.jumpShakeDurationTime = Mathf.Lerp(this.jumpShakeDurationTime, 0f, Time.deltaTime * this.jumpShakeDuration);
		this.jumpShakeSin = Mathf.Sin(this.jumpShakeTime);
		this.rotationLayerJumpShake.eulerAngles = new Vector3(this.jumpShakeSin * (this.jumpShakeDurationTime / this.jumpShakeDuration) * this.jumpShakeAmount, this.ShakeFormula(this.jumpShakeTime * 0.5f, this.jumpShakeDurationTime * this.jumpShakeAmount * 0.1f, 0.3f), this.ShakeFormula(this.jumpShakeTime * 0.5f, this.jumpShakeDurationTime * this.jumpShakeAmount * 0.25f, 0.7f));
	}

	// Token: 0x06000270 RID: 624 RVA: 0x000041D7 File Offset: 0x000023D7
	public static void TriggerCameraTremor(CameraController cameraController)
	{
		if (!GameData.settings.CameraShake)
		{
			return;
		}
		if (cameraController == null)
		{
			return;
		}
		cameraController.tremorTrigger = true;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x000041F7 File Offset: 0x000023F7
	public static void TriggerCameraJumpShake(CameraController cameraController, float amount = 7f)
	{
		if (!GameData.settings.CameraShake)
		{
			return;
		}
		if (cameraController == null)
		{
			return;
		}
		cameraController.jumpShakeTrigger = true;
		cameraController.jumpShakeAmount = amount;
	}

	// Token: 0x0400036E RID: 878
	private PlayerSettings playerSettings;

	// Token: 0x0400036F RID: 879
	private SkinnedMeshRenderer ragdollMesh;

	// Token: 0x04000370 RID: 880
	public float targetTransitionSpeed = 20f;

	// Token: 0x04000371 RID: 881
	private float transitionSpeed;

	// Token: 0x04000372 RID: 882
	public CameraController.Target target;

	// Token: 0x04000373 RID: 883
	public Transform targetPlayerCam;

	// Token: 0x04000374 RID: 884
	public Transform targetCinematicCam;

	// Token: 0x04000375 RID: 885
	public Transform targetBodyCam;

	// Token: 0x04000376 RID: 886
	public Transform targetVehicleCam;

	// Token: 0x04000377 RID: 887
	private Transform targetCam;

	// Token: 0x04000378 RID: 888
	private Quaternion rotationLayerTarget;

	// Token: 0x04000379 RID: 889
	private Quaternion rotationLayerHandheld;

	// Token: 0x0400037A RID: 890
	private Quaternion rotationLayerTremor;

	// Token: 0x0400037B RID: 891
	private Quaternion rotationLayerJumpShake;

	// Token: 0x0400037C RID: 892
	private bool toggledRagdollMode;

	// Token: 0x0400037D RID: 893
	[Header("Handheld")]
	public float handheldAmount = 0.1f;

	// Token: 0x0400037E RID: 894
	public float handheldSpeed = 1.5f;

	// Token: 0x0400037F RID: 895
	[Space]
	public float handheldMovementMultiplier = 0.7f;

	// Token: 0x04000380 RID: 896
	public float handheldMovementSpeedMultiplier = 4f;

	// Token: 0x04000381 RID: 897
	private float handheldMovement;

	// Token: 0x04000382 RID: 898
	private float handheldTime;

	// Token: 0x04000383 RID: 899
	[Header("Tremor")]
	public bool tremorTrigger;

	// Token: 0x04000384 RID: 900
	public float tremorDuration = 5f;

	// Token: 0x04000385 RID: 901
	public float tremorAmount = 2.5f;

	// Token: 0x04000386 RID: 902
	private float tremorInitialAmount;

	// Token: 0x04000387 RID: 903
	public float tremorSpeed = 4.8f;

	// Token: 0x04000388 RID: 904
	private float tremorTime;

	// Token: 0x04000389 RID: 905
	private float tremorDurationTime;

	// Token: 0x0400038A RID: 906
	private float tremorSin;

	// Token: 0x0400038B RID: 907
	[Header("JumpShake")]
	public bool jumpShakeTrigger;

	// Token: 0x0400038C RID: 908
	public float jumpShakeDuration = 4f;

	// Token: 0x0400038D RID: 909
	public float jumpShakeAmount = 7f;

	// Token: 0x0400038E RID: 910
	public float jumpShakeSpeed = 18f;

	// Token: 0x0400038F RID: 911
	private float jumpShakeTime;

	// Token: 0x04000390 RID: 912
	private float jumpShakeDurationTime;

	// Token: 0x04000391 RID: 913
	private float jumpShakeSin;

	// Token: 0x04000392 RID: 914
	private bool CinematicCameraToggle;

	// Token: 0x02000079 RID: 121
	public enum Target
	{
		// Token: 0x04000394 RID: 916
		None,
		// Token: 0x04000395 RID: 917
		Player,
		// Token: 0x04000396 RID: 918
		Cinematic,
		// Token: 0x04000397 RID: 919
		Vehicle,
		// Token: 0x04000398 RID: 920
		BodyCam
	}
}
