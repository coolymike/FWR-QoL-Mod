using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class AgentMoveTrigger : MonoBehaviour
{
	// Token: 0x060003C3 RID: 963 RVA: 0x000052EE File Offset: 0x000034EE
	private void Start()
	{
		if (this.agentController != null)
		{
			this.agentController.forceStop = true;
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0000530A File Offset: 0x0000350A
	private void OnTriggerExit()
	{
		if (this.agentController != null)
		{
			this.agentController.forceStop = false;
		}
	}

	// Token: 0x04000525 RID: 1317
	public AgentController agentController;
}
