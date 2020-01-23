using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class RagdollAnimatorSmartRagdoll : MonoBehaviour
{
	// Token: 0x06000176 RID: 374 RVA: 0x0000DD38 File Offset: 0x0000BF38
	private void Update()
	{
		this.ragdollAnimator.SetFloat("movingSpeed", this.agentController.agent.velocity.magnitude / 6f);
	}

	// Token: 0x04000234 RID: 564
	public Animator ragdollAnimator;

	// Token: 0x04000235 RID: 565
	public AgentController agentController;
}
