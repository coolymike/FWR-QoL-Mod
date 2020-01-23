using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004C RID: 76
[Serializable]
public class SmartRagdollController : MonoBehaviour
{
	// Token: 0x0600017F RID: 383 RVA: 0x00003615 File Offset: 0x00001815
	private void Start()
	{
		this.simulatedRagdoll.OnRagdollToggle += this.OnRagdollToggle;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000363F File Offset: 0x0000183F
	private void OnDestroy()
	{
		this.simulatedRagdoll.OnRagdollToggle -= this.OnRagdollToggle;
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
	private void OnEnable()
	{
		if (this.enableSuicide)
		{
			this.timeToSuicide = UnityEngine.Random.Range(this.suicideTimeFrame.x, this.suicideTimeFrame.y);
			base.Invoke("Suicide", this.timeToSuicide);
		}
		this.OnRagdollToggle(this.simulatedRagdoll.RagdollModeEnabled);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00003669 File Offset: 0x00001869
	private void OnDisable()
	{
		base.CancelInvoke("Suicide");
		this.ResetRagdoll();
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000367C File Offset: 0x0000187C
	private void Update()
	{
		this.AgentSetLogic();
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00003684 File Offset: 0x00001884
	private void FixedUpdate()
	{
		this.GroundDistanceCheck();
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000368C File Offset: 0x0000188C
	private void Suicide()
	{
		this.simulatedRagdoll.RagdollModeEnabled = true;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000369A File Offset: 0x0000189A
	private bool RagdollIsActive(bool checkNavMesh)
	{
		return this.ragdollIsSmart && !LevelManager.BuildModeOn && (!checkNavMesh || NavMeshGenerator.IsReady) && !this.simulatedRagdoll.RagdollModeEnabled;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x000036CB File Offset: 0x000018CB
	private void OnBuildModeToggle(bool buildModeOn)
	{
		this.animatedSmartRagdoll.SetActive(this.RagdollIsActive(false));
		if (buildModeOn && !this.ragdollIsSmart)
		{
			this.simulatedRagdoll.RagdollModeEnabled = true;
			this.simulatedRagdoll.enableBoneRetargeting = false;
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
	private void GroundDistanceCheck()
	{
		if (!this.RagdollIsActive(true))
		{
			return;
		}
		if (Physics.Raycast(this.simulatedRagdollCore.position, Vector3.down, out this.hitInfo, float.PositiveInfinity, -1025) && this.hitInfo.distance <= this.maxGroundDistance)
		{
			this.groundDistance = this.hitInfo.distance;
		}
		else
		{
			this.groundDistance = this.maxGroundDistance;
		}
		if (this.groundDistance > 1.7f)
		{
			this.simulatedRagdoll.RagdollModeEnabled = true;
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000DF84 File Offset: 0x0000C184
	private void OnRagdollToggle(bool ragdollModeEnabled)
	{
		if (!ragdollModeEnabled)
		{
			if (!SmartRagdollController.activeRagdollsInScene.Contains(this))
			{
				SmartRagdollController.activeRagdollsInScene.Add(this);
			}
			this.EnableAutoHide(false);
			return;
		}
		SmartRagdollController.activeRagdollsInScene.Remove(this);
		SmartRagdollController.OnRagdollMode onRagdollMode = this.doOnRagdollMode;
		if (onRagdollMode == SmartRagdollController.OnRagdollMode.Hide)
		{
			this.ApplyAutoHide();
			this.EnableAutoHide(true);
			return;
		}
		if (onRagdollMode != SmartRagdollController.OnRagdollMode.Destroy)
		{
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject, this.onRagdollModeTimer);
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00003702 File Offset: 0x00001902
	private void ApplyAutoHide()
	{
		if (this.autoHideScript == null)
		{
			this.autoHideScript = AutoHide.AddComponent(base.gameObject, this.onRagdollModeTimer);
			this.EnableAutoHide(false);
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00003730 File Offset: 0x00001930
	private void EnableAutoHide(bool _status)
	{
		if (this.autoHideScript != null)
		{
			this.autoHideScript.enabled = _status;
		}
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000374C File Offset: 0x0000194C
	private void ResetRagdoll()
	{
		this.simulatedRagdoll.RagdollModeEnabled = false;
		this.simulatedRagdoll.ResetRagdoll();
		this.animatedSmartRagdoll.transform.position = base.transform.position;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
	private void AgentSetLogic()
	{
		if (!this.RagdollIsActive(true))
		{
			this.agent.SetActive(false);
			return;
		}
		this.agent.SetActive(true);
		if (!this.useLogicConditions)
		{
			return;
		}
		switch (this.logic)
		{
		case SmartRagdollController.Logic.Follow:
			this.agentController.moveMethod = AgentController.MoveMethod.FollowTarget;
			this.AgentSetTarget();
			return;
		case SmartRagdollController.Logic.Attack:
			this.RagdollAttackDefendTargeting(SmartRagdollController.Logic.Defend);
			return;
		case SmartRagdollController.Logic.Defend:
			this.RagdollAttackDefendTargeting(SmartRagdollController.Logic.Attack);
			return;
		case SmartRagdollController.Logic.Explore:
			this.agentController.moveMethod = AgentController.MoveMethod.Explore;
			this.AgentSetTarget();
			return;
		case SmartRagdollController.Logic.Avoid:
			this.agentController.moveMethod = AgentController.MoveMethod.AvoidTarget;
			this.AgentSetTarget();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000E098 File Offset: 0x0000C298
	private void RagdollAttackDefendTargeting(SmartRagdollController.Logic chaseLogic)
	{
		if (this.ragdollFound == null)
		{
			this.ragdollFound = RagdollDatabase.ClosestRagdoll(this.simulatedRagdollCore.position, chaseLogic);
		}
		if (this.ragdollFound != null)
		{
			if (this.ragdollFound.simulatedRagdoll.ragdollModeEnabled)
			{
				this.ragdollFound = RagdollDatabase.ClosestRagdoll(this.simulatedRagdollCore.position, chaseLogic);
			}
			if (this.ragdollFound == null)
			{
				return;
			}
			this.agentController.moveMethod = AgentController.MoveMethod.MoveToPosition;
			this.agentController.target = this.ragdollFound.simulatedRagdollCore;
			if (Vector3.Distance(this.simulatedRagdollCore.position, this.ragdollFound.simulatedRagdollCore.position) < this.attackDistance)
			{
				this.ragdollFound.simulatedRagdoll.RagdollModeEnabled = true;
				return;
			}
		}
		else
		{
			if (chaseLogic == SmartRagdollController.Logic.Attack)
			{
				this.agentController.moveMethod = AgentController.MoveMethod.FollowTarget;
			}
			else
			{
				this.agentController.moveMethod = AgentController.MoveMethod.MoveToPosition;
			}
			this.AgentSetTarget();
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000E194 File Offset: 0x0000C394
	private void AgentSetTarget()
	{
		if (this.targetIsPlayer)
		{
			this.agentController.target = PlayerSettings.instance.simulatedRagdoll.bodyElements.ragdollJoints[0].transform;
		}
		if (!this.abideByFlags)
		{
			return;
		}
		if (Flag.flagsInScene.Count == 0)
		{
			return;
		}
		this.closestFlag = Flag.GetClosestFlag(this.agent.transform.position, Flag.Type.Follow);
		if (this.closestFlag == null)
		{
			return;
		}
		SmartRagdollController.Logic logic = this.logic;
		if (logic == SmartRagdollController.Logic.Follow && this.closestFlag.type == Flag.Type.Follow)
		{
			this.agentController.target = this.closestFlag.transform;
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000E240 File Offset: 0x0000C440
	public static bool RagdollHasLogic(Transform ragdollTransform, SmartRagdollController.Logic logicToCheck)
	{
		SmartRagdollController componentInChildren = ragdollTransform.root.GetComponentInChildren<SmartRagdollController>();
		return !(componentInChildren == null) && componentInChildren.logic == logicToCheck;
	}

	// Token: 0x04000238 RID: 568
	public static List<SmartRagdollController> activeRagdollsInScene = new List<SmartRagdollController>();

	// Token: 0x04000239 RID: 569
	[Header("References")]
	public GameObject simulatedSmartRagdoll;

	// Token: 0x0400023A RID: 570
	public GameObject animatedSmartRagdoll;

	// Token: 0x0400023B RID: 571
	public Transform simulatedRagdollCore;

	// Token: 0x0400023C RID: 572
	public RagdollSettings simulatedRagdoll;

	// Token: 0x0400023D RID: 573
	public AgentController agentController;

	// Token: 0x0400023E RID: 574
	public GameObject agent;

	// Token: 0x0400023F RID: 575
	[Header("Settings")]
	public bool ragdollIsSmart = true;

	// Token: 0x04000240 RID: 576
	public SmartRagdollController.OnRagdollMode doOnRagdollMode;

	// Token: 0x04000241 RID: 577
	public float onRagdollModeTimer = 4f;

	// Token: 0x04000242 RID: 578
	private AutoHide autoHideScript;

	// Token: 0x04000243 RID: 579
	[Header("Suicide")]
	public bool enableSuicide;

	// Token: 0x04000244 RID: 580
	public Vector2 suicideTimeFrame = new Vector2(5f, 10f);

	// Token: 0x04000245 RID: 581
	private float timeToSuicide;

	// Token: 0x04000246 RID: 582
	[Header("Agent Settings")]
	private bool navMeshReady;

	// Token: 0x04000247 RID: 583
	private readonly float attackDistance = 1.5f;

	// Token: 0x04000248 RID: 584
	public bool useLogicConditions = true;

	// Token: 0x04000249 RID: 585
	public SmartRagdollController.Logic logic;

	// Token: 0x0400024A RID: 586
	public bool targetIsPlayer = true;

	// Token: 0x0400024B RID: 587
	public bool abideByFlags = true;

	// Token: 0x0400024C RID: 588
	private RaycastHit hitInfo;

	// Token: 0x0400024D RID: 589
	private float groundDistance;

	// Token: 0x0400024E RID: 590
	private readonly float maxGroundDistance = 2.4f;

	// Token: 0x0400024F RID: 591
	private SmartRagdollController ragdollFound;

	// Token: 0x04000250 RID: 592
	private Flag closestFlag;

	// Token: 0x0200004D RID: 77
	public enum OnRagdollMode
	{
		// Token: 0x04000252 RID: 594
		DoNothing,
		// Token: 0x04000253 RID: 595
		Hide,
		// Token: 0x04000254 RID: 596
		Destroy
	}

	// Token: 0x0200004E RID: 78
	public enum Logic
	{
		// Token: 0x04000256 RID: 598
		DoNothing,
		// Token: 0x04000257 RID: 599
		Follow,
		// Token: 0x04000258 RID: 600
		Attack,
		// Token: 0x04000259 RID: 601
		Defend,
		// Token: 0x0400025A RID: 602
		Explore,
		// Token: 0x0400025B RID: 603
		Avoid
	}
}
