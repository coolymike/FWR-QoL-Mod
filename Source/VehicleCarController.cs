using System;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class VehicleCarController : MonoBehaviour
{
	// Token: 0x060001FB RID: 507 RVA: 0x00003D36 File Offset: 0x00001F36
	private void Awake()
	{
		this.vehicleController = base.GetComponent<Vehicle>();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000FA50 File Offset: 0x0000DC50
	private void Start()
	{
		this.drivingWheelPos = new Vector3[this.drivingWheels.Length];
		this.steeringWheelPos = new Vector3[this.steeringWheels.Length];
		this.drivingWheelRot = new Quaternion[this.drivingWheels.Length];
		this.steeringWheelRot = new Quaternion[this.steeringWheels.Length];
		this.body.centerOfMass = Vector3.down;
		for (int i = 0; i < this.drivingWheels.Length; i++)
		{
			this.drivingWheels[i].ConfigureVehicleSubsteps(this.maxSpeed, this.substepBelowThreshold, this.substepAboveThreshold);
		}
		for (int j = 0; j < this.drivingWheelMeshes.Length; j++)
		{
			this.drivingWheels[j].transform.position = this.drivingWheelMeshes[j].transform.position;
		}
		for (int k = 0; k < this.steeringWheelMeshes.Length; k++)
		{
			this.steeringWheels[k].transform.position = this.steeringWheelMeshes[k].transform.position;
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000FB5C File Offset: 0x0000DD5C
	private void FixedUpdate()
	{
		this.activeSpeed = this.body.velocity.sqrMagnitude;
		if (this.vehicleController != null && this.vehicleController.playerControlling != null)
		{
			this.PlayerControlling();
		}
		else
		{
			this.AIControlling();
		}
		for (int i = 0; i < this.steeringWheels.Length; i++)
		{
			this.steeringWheels[i].steerAngle = this.activeTurnAngle;
		}
		this.MeshTransforms();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
	private void PlayerControlling()
	{
		this.activeTurnAngle = Mathf.MoveTowards(this.activeTurnAngle, InputManager.Move(true).x * (this.maxTurnAngle - this.activeSpeed / this.maxSpeed * this.maxTurnAngle * this.turnAngleOnSpeedMultiplier), this.turnAngleSpeed * Time.deltaTime);
		for (int i = 0; i < this.drivingWheels.Length; i++)
		{
			if (InputManager.VehicleA() && InputManager.VehicleB())
			{
				this.drivingWheels[i].brakeTorque = this.breakForce;
			}
			else if (InputManager.VehicleA() && this.activeSpeed < this.maxSpeed)
			{
				this.drivingWheels[i].motorTorque = this.motorForce;
				this.drivingWheels[i].brakeTorque = 0f;
			}
			else if (InputManager.VehicleB() && this.activeSpeed < this.maxSpeed * 0.7f)
			{
				this.drivingWheels[i].motorTorque = -this.motorForce;
				this.drivingWheels[i].brakeTorque = 0f;
			}
			else
			{
				this.drivingWheels[i].brakeTorque = (this.drivingWheels[i].motorTorque = 0f);
				this.drivingWheels[i].brakeTorque = this.breakForce * 0.2f;
			}
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000FD34 File Offset: 0x0000DF34
	private void AIControlling()
	{
		if (this.driveToTarget)
		{
			this.DriveToTarget();
			return;
		}
		if (this.autoDrive)
		{
			this.AutoDrive();
			return;
		}
		for (int i = 0; i < this.drivingWheels.Length; i++)
		{
			this.drivingWheels[i].brakeTorque = this.breakForce;
			this.drivingWheels[i].motorTorque = 0f;
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000FD98 File Offset: 0x0000DF98
	private void AutoDrive()
	{
		for (int i = 0; i < this.drivingWheels.Length; i++)
		{
			this.drivingWheels[i].motorTorque = this.autoMotor * this.motorForce;
			this.drivingWheels[i].brakeTorque = 0f;
		}
		for (int j = 0; j < this.steeringWheels.Length; j++)
		{
			this.activeTurnAngle = this.autoSteer * this.maxTurnAngle;
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000FE0C File Offset: 0x0000E00C
	private void DriveToTarget()
	{
		if (this.targetIsPlayer)
		{
			this.target = this.GetClosestPlayerPosition();
		}
		else if (this.targetIsRagdoll)
		{
			this.target = this.LockOnRagdoll();
		}
		if (this.target == null)
		{
			for (int i = 0; i < this.drivingWheels.Length; i++)
			{
				this.drivingWheels[i].brakeTorque = this.breakForce;
				this.drivingWheels[i].motorTorque = 0f;
			}
			return;
		}
		for (int j = 0; j < this.drivingWheels.Length; j++)
		{
			this.drivingWheels[j].brakeTorque = 0f;
		}
		this.dotForward = Vector3.Dot(this.target.position - this.body.transform.position, this.body.transform.forward);
		this.dotRight = Vector3.Dot(this.target.position - this.body.transform.position, this.body.transform.right);
		for (int k = 0; k < this.drivingWheels.Length; k++)
		{
			if (this.dotForward > 0f)
			{
				this.drivingWheels[k].motorTorque = this.motorForce * 0.5f;
			}
		}
		this.activeTurnAngle = Mathf.Clamp(this.dotRight * this.maxTurnAngle * 0.5f, -this.maxTurnAngle, this.maxTurnAngle);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00003D44 File Offset: 0x00001F44
	private Transform GetClosestPlayerPosition()
	{
		return PlayerSettings.instance.simulatedRagdoll.bodyElements.ragdollJoints[0].transform;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000FF8C File Offset: 0x0000E18C
	private void MeshTransforms()
	{
		for (int i = 0; i < this.drivingWheels.Length; i++)
		{
			this.drivingWheels[i].GetWorldPose(out this.drivingWheelPos[i], out this.drivingWheelRot[i]);
		}
		for (int j = 0; j < this.steeringWheels.Length; j++)
		{
			this.steeringWheels[j].GetWorldPose(out this.steeringWheelPos[j], out this.steeringWheelRot[j]);
		}
		for (int k = 0; k < this.drivingWheelMeshes.Length; k++)
		{
			this.drivingWheelMeshes[k].transform.position = this.drivingWheelPos[k];
			this.drivingWheelMeshes[k].transform.rotation = this.drivingWheelRot[k];
		}
		for (int l = 0; l < this.steeringWheelMeshes.Length; l++)
		{
			this.steeringWheelMeshes[l].transform.position = this.steeringWheelPos[l];
			this.steeringWheelMeshes[l].transform.rotation = this.steeringWheelRot[l];
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x000100A8 File Offset: 0x0000E2A8
	private Transform LockOnRagdoll()
	{
		if (SmartRagdollController.activeRagdollsInScene.Count == 0)
		{
			return null;
		}
		if (this.closestRagdoll == null)
		{
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
		else if (this.closestRagdoll.simulatedRagdoll.RagdollModeEnabled || !this.closestRagdoll.isActiveAndEnabled)
		{
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
		else
		{
			if (Vector3.Distance(this.closestRagdoll.simulatedRagdollCore.position, this.body.position) <= 24f)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00003D61 File Offset: 0x00001F61
	private void AssignClosestRagdoll()
	{
		this.closestRagdoll = RagdollDatabase.ClosestRagdoll(this.body.position);
	}

	// Token: 0x040002D1 RID: 721
	[Header("Player Driving")]
	public Vehicle vehicle;

	// Token: 0x040002D2 RID: 722
	[Header("AI Driving")]
	public bool driveToTarget;

	// Token: 0x040002D3 RID: 723
	public bool targetIsPlayer;

	// Token: 0x040002D4 RID: 724
	public bool targetIsRagdoll;

	// Token: 0x040002D5 RID: 725
	public Transform target;

	// Token: 0x040002D6 RID: 726
	public bool autoDrive;

	// Token: 0x040002D7 RID: 727
	public float autoMotor;

	// Token: 0x040002D8 RID: 728
	public float autoSteer;

	// Token: 0x040002D9 RID: 729
	[Header("Speed")]
	public float maxSpeed = 400f;

	// Token: 0x040002DA RID: 730
	private float activeSpeed;

	// Token: 0x040002DB RID: 731
	[Header("Turning")]
	public float maxTurnAngle = 30f;

	// Token: 0x040002DC RID: 732
	public float turnAngleOnSpeedMultiplier = 0.5f;

	// Token: 0x040002DD RID: 733
	public float turnAngleSpeed = 100f;

	// Token: 0x040002DE RID: 734
	private float activeTurnAngle;

	// Token: 0x040002DF RID: 735
	[Header("Forces")]
	public float motorForce = 5000f;

	// Token: 0x040002E0 RID: 736
	public float breakForce = 6000f;

	// Token: 0x040002E1 RID: 737
	[Header("Substeps")]
	public int substepBelowThreshold = 4;

	// Token: 0x040002E2 RID: 738
	public int substepAboveThreshold = 6;

	// Token: 0x040002E3 RID: 739
	[Header("GameObjects")]
	public Rigidbody body;

	// Token: 0x040002E4 RID: 740
	public WheelCollider[] drivingWheels;

	// Token: 0x040002E5 RID: 741
	public WheelCollider[] steeringWheels;

	// Token: 0x040002E6 RID: 742
	public Transform[] drivingWheelMeshes;

	// Token: 0x040002E7 RID: 743
	public Transform[] steeringWheelMeshes;

	// Token: 0x040002E8 RID: 744
	private Vector3[] drivingWheelPos;

	// Token: 0x040002E9 RID: 745
	private Vector3[] steeringWheelPos;

	// Token: 0x040002EA RID: 746
	private Quaternion[] drivingWheelRot;

	// Token: 0x040002EB RID: 747
	private Quaternion[] steeringWheelRot;

	// Token: 0x040002EC RID: 748
	private Vehicle vehicleController;

	// Token: 0x040002ED RID: 749
	private float dotForward;

	// Token: 0x040002EE RID: 750
	private float dotRight;

	// Token: 0x040002EF RID: 751
	private SmartRagdollController closestRagdoll;
}
