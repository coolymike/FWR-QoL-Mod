using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Token: 0x0200007C RID: 124
public class FocusObject : MonoBehaviour
{
	// Token: 0x06000281 RID: 641 RVA: 0x000042CA File Offset: 0x000024CA
	private void Start()
	{
		this.ragdollSettings = PlayerSettings.instance.simulatedRagdoll;
		this.focusObject = this.ragdollSettings.bodyElements.GetJoint(RagdollBodyElements.Joint.ChestLow);
	}

	// Token: 0x06000282 RID: 642 RVA: 0x000128C4 File Offset: 0x00010AC4
	private void Update()
	{
		if (!LevelManager.StatusIsWinOrLose(true))
		{
			if (this.ragdollSettings.RagdollModeEnabled)
			{
				if (LevelManager.StatusIsWinOrLose(true))
				{
					this.aperture = 0.1f;
				}
				else
				{
					this.aperture = 4f;
				}
				this.ChromaticAbbe = 0.4f;
			}
			else
			{
				this.aperture = 6.5f;
				this.ChromaticAbbe = 0.1f;
			}
			this.distance = Vector3.Distance(this.focusObject.transform.position, Camera.main.gameObject.transform.position);
			this.distanceTransition = 4f;
			this.setDistanceOnWin = false;
		}
		else
		{
			if (!this.setDistanceOnWin)
			{
				this.postProfile.GetSetting<DepthOfField>().focusDistance.value = 20f;
				this.setDistanceOnWin = true;
			}
			this.distance = 0.1f;
			this.distanceTransition = 2f;
		}
		this.postProfile.GetSetting<DepthOfField>().focusDistance.value = Mathf.Lerp(this.postProfile.GetSetting<DepthOfField>().focusDistance.value, this.distance, this.distanceTransition * Time.fixedDeltaTime);
		this.postProfile.GetSetting<DepthOfField>().aperture.value = Mathf.Lerp(this.postProfile.GetSetting<DepthOfField>().aperture.value, this.aperture, 4f * Time.fixedDeltaTime);
		this.postProfile.GetSetting<ChromaticAberration>().intensity.value = Mathf.Lerp(this.postProfile.GetSetting<ChromaticAberration>().intensity.value, this.ChromaticAbbe, 5f * Time.fixedDeltaTime);
	}

	// Token: 0x040003C9 RID: 969
	public PostProcessProfile postProfile;

	// Token: 0x040003CA RID: 970
	private RagdollSettings ragdollSettings;

	// Token: 0x040003CB RID: 971
	private GameObject focusObject;

	// Token: 0x040003CC RID: 972
	private float distance;

	// Token: 0x040003CD RID: 973
	private float distanceTransition = 4f;

	// Token: 0x040003CE RID: 974
	private bool setDistanceOnWin;

	// Token: 0x040003CF RID: 975
	private float aperture = 6f;

	// Token: 0x040003D0 RID: 976
	private float ChromaticAbbe = 0.15f;
}
