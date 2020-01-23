using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class RagdollDatabase : MonoBehaviour
{
	// Token: 0x06000178 RID: 376 RVA: 0x0000357E File Offset: 0x0000177E
	private void Start()
	{
		this.smartRagdollController.simulatedRagdoll.OnRagdollToggle += this.OnRagdollToggle;
		this.OnRagdollToggle(this.smartRagdollController.simulatedRagdoll.RagdollModeEnabled);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000035B2 File Offset: 0x000017B2
	private void OnDestroy()
	{
		this.smartRagdollController.simulatedRagdoll.OnRagdollToggle -= this.OnRagdollToggle;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x000035D0 File Offset: 0x000017D0
	private void OnRagdollToggle(bool ragdollModeEnabled)
	{
		if (ragdollModeEnabled)
		{
			RagdollDatabase.activeRagdollsInScene.Remove(this.smartRagdollController);
			return;
		}
		if (!RagdollDatabase.activeRagdollsInScene.Contains(this.smartRagdollController))
		{
			RagdollDatabase.activeRagdollsInScene.Add(this.smartRagdollController);
		}
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000DD74 File Offset: 0x0000BF74
	public static SmartRagdollController ClosestRagdoll(Vector3 fromPosition)
	{
		if (RagdollDatabase.activeRagdollsInScene.Count == 0)
		{
			return null;
		}
		SmartRagdollController result = null;
		float num = float.PositiveInfinity;
		for (int i = 0; i < RagdollDatabase.activeRagdollsInScene.Count; i++)
		{
			if (!(RagdollDatabase.activeRagdollsInScene[i] == null) && RagdollDatabase.activeRagdollsInScene[i].isActiveAndEnabled)
			{
				float num2 = Vector3.Distance(fromPosition, RagdollDatabase.activeRagdollsInScene[i].simulatedRagdollCore.position);
				if (num2 < num)
				{
					num = num2;
					result = RagdollDatabase.activeRagdollsInScene[i];
				}
			}
		}
		return result;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x0000DE00 File Offset: 0x0000C000
	public static SmartRagdollController ClosestRagdoll(Vector3 fromPosition, SmartRagdollController.Logic ofLogic)
	{
		if (RagdollDatabase.activeRagdollsInScene.Count == 0)
		{
			return null;
		}
		SmartRagdollController result = null;
		float num = float.PositiveInfinity;
		for (int i = 0; i < RagdollDatabase.activeRagdollsInScene.Count; i++)
		{
			if (!(RagdollDatabase.activeRagdollsInScene[i] == null) && RagdollDatabase.activeRagdollsInScene[i].isActiveAndEnabled && RagdollDatabase.activeRagdollsInScene[i].logic == ofLogic)
			{
				float num2 = Vector3.Distance(fromPosition, RagdollDatabase.activeRagdollsInScene[i].simulatedRagdollCore.position);
				if (num2 < num)
				{
					num = num2;
					result = RagdollDatabase.activeRagdollsInScene[i];
				}
			}
		}
		return result;
	}

	// Token: 0x04000236 RID: 566
	public static List<SmartRagdollController> activeRagdollsInScene = new List<SmartRagdollController>();

	// Token: 0x04000237 RID: 567
	public SmartRagdollController smartRagdollController;
}
