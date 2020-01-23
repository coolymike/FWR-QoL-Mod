using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class AudioWindVelocity : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x0000E6BC File Offset: 0x0000C8BC
	private void Update()
	{
		this.audioSource.volume = (this.lastPositionMagnitude = Mathf.Lerp(this.lastPositionMagnitude, Mathf.Clamp((base.transform.position - this.lastPosition).magnitude * 0.8f, 0f, this.playerSettings.simulatedRagdoll.ragdollModeEnabled ? 0.5f : 1f), 2f * Time.deltaTime));
		this.lastPosition = base.transform.position;
	}

	// Token: 0x04000276 RID: 630
	public AudioSource audioSource;

	// Token: 0x04000277 RID: 631
	private Vector3 lastPosition;

	// Token: 0x04000278 RID: 632
	private float lastPositionMagnitude;

	// Token: 0x04000279 RID: 633
	public PlayerSettings playerSettings;
}
