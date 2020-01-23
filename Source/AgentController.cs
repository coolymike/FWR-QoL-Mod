using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000048 RID: 72
public class AgentController : MonoBehaviour
{
	// Token: 0x06000167 RID: 359 RVA: 0x000034A0 File Offset: 0x000016A0
	private void Awake()
	{
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000034B3 File Offset: 0x000016B3
	private void Start()
	{
		this.agentStartRotation = this.agent.transform.rotation;
		this.OnBuildModeToggle(LevelManager.BuildModeOn);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000034D6 File Offset: 0x000016D6
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000034E9 File Offset: 0x000016E9
	private void Update()
	{
		this.AgentLogic();
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000DA08 File Offset: 0x0000BC08
	private void AgentLogic()
	{
		if (!this.agent.isActiveAndEnabled)
		{
			return;
		}
		if (this.keepUpWithTarget)
		{
			this.agent.speed = this.speed + this.targetDistance * 0.1f;
		}
		else
		{
			this.agent.speed = this.speed;
		}
		this.MoveAgent();
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000034F1 File Offset: 0x000016F1
	private void OnDisable()
	{
		this.ResetPosition();
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000034F9 File Offset: 0x000016F9
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.ResetPosition();
		}
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000DA64 File Offset: 0x0000BC64
	private void MoveAgent()
	{
		if (this.target == null)
		{
			return;
		}
		this.targetDistance = Vector3.Distance(this.agent.transform.position, this.target.position);
		switch (this.moveMethod)
		{
		case AgentController.MoveMethod.FollowTarget:
			this.FollowTarget();
			return;
		case AgentController.MoveMethod.AvoidTarget:
			this.AvoidTarget();
			return;
		case AgentController.MoveMethod.MoveToPosition:
			this.MoveToPosition(this.target.position);
			return;
		case AgentController.MoveMethod.Explore:
			this.Explore();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000DAEC File Offset: 0x0000BCEC
	private void FollowTarget()
	{
		if (this.targetDistance > this.followInRange.y)
		{
			this.MoveToPosition(this.target.position);
			return;
		}
		if (this.targetDistance < this.followInRange.x)
		{
			this.AvoidTarget();
			return;
		}
		if (this.agent.isOnNavMesh)
		{
			this.agent.ResetPath();
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000DB50 File Offset: 0x0000BD50
	private void AvoidTarget()
	{
		if (this.targetDistance < this.avoidDistance)
		{
			this.MoveToPosition(this.agent.transform.position + (this.agent.transform.position - this.target.position));
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
	private void Explore()
	{
		this.exploreWaitTimer -= Time.deltaTime;
		if (this.exploreWaitTimer < 0f)
		{
			this.exploreTargetPosition.x = this.agent.transform.position.x + UnityEngine.Random.Range(this.exploreRangeXZ.x, this.exploreRangeXZ.y);
			this.exploreTargetPosition.y = this.agent.transform.position.y + UnityEngine.Random.Range(this.exploreRangeY.x, this.exploreRangeY.y);
			this.exploreTargetPosition.z = this.agent.transform.position.z + UnityEngine.Random.Range(this.exploreRangeXZ.x, this.exploreRangeXZ.y);
			this.exploreWaitTimer = UnityEngine.Random.Range(this.exploreWaitTimeRange.x, this.exploreWaitTimeRange.y);
		}
		this.MoveToPosition(this.exploreTargetPosition);
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00003504 File Offset: 0x00001704
	private void MoveToPosition(Vector3 target)
	{
		if (!this.agent.isOnNavMesh)
		{
			return;
		}
		if (this.forceStop)
		{
			this.ResetPath();
			return;
		}
		this.agent.destination = target;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000352F File Offset: 0x0000172F
	private void ResetPath()
	{
		if (this.agent.isOnNavMesh)
		{
			this.agent.ResetPath();
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00003549 File Offset: 0x00001749
	private void ResetPosition()
	{
		this.ResetPath();
		this.agent.Warp(base.transform.position);
		this.agent.transform.rotation = this.agentStartRotation;
	}

	// Token: 0x0400021D RID: 541
	public NavMeshAgent agent;

	// Token: 0x0400021E RID: 542
	private Quaternion agentStartRotation;

	// Token: 0x0400021F RID: 543
	private bool allowNavigation;

	// Token: 0x04000220 RID: 544
	[Header("Agent Settings")]
	public float speed = 8f;

	// Token: 0x04000221 RID: 545
	public bool keepUpWithTarget;

	// Token: 0x04000222 RID: 546
	[Space]
	public bool forceStop;

	// Token: 0x04000223 RID: 547
	[Header("Movement Method")]
	public AgentController.MoveMethod moveMethod;

	// Token: 0x04000224 RID: 548
	[Header("Locality Settings")]
	public Transform target;

	// Token: 0x04000225 RID: 549
	public float targetDistance;

	// Token: 0x04000226 RID: 550
	[Header("Follow Target")]
	public Vector2 followInRange = new Vector2(2.5f, 7f);

	// Token: 0x04000227 RID: 551
	[Header("Avoid Target")]
	public float avoidDistance = 20f;

	// Token: 0x04000228 RID: 552
	[Header("Explore")]
	public Vector2 exploreRangeXZ = new Vector2(-50f, 50f);

	// Token: 0x04000229 RID: 553
	public Vector2 exploreRangeY = new Vector2(-10f, 10f);

	// Token: 0x0400022A RID: 554
	private Vector3 exploreTargetPosition;

	// Token: 0x0400022B RID: 555
	public Vector2 exploreWaitTimeRange = new Vector2(0.5f, 10f);

	// Token: 0x0400022C RID: 556
	private float exploreWaitTimer;

	// Token: 0x02000049 RID: 73
	public enum MoveMethod
	{
		// Token: 0x0400022E RID: 558
		DoNothing,
		// Token: 0x0400022F RID: 559
		FollowTarget,
		// Token: 0x04000230 RID: 560
		AvoidTarget,
		// Token: 0x04000231 RID: 561
		MoveToPosition,
		// Token: 0x04000232 RID: 562
		Explore,
		// Token: 0x04000233 RID: 563
		HideAndSeek
	}
}
