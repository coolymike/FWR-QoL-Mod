using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000B RID: 11
public class ParticleMenu : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x0000229D File Offset: 0x0000049D
	private void Start()
	{
		this.Navigate(0);
		this.currentIndex = 0;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00008508 File Offset: 0x00006708
	public void Navigate(int i)
	{
		this.currentIndex = (this.particleSystems.Length + this.currentIndex + i) % this.particleSystems.Length;
		if (this.currentGO != null)
		{
			UnityEngine.Object.Destroy(this.currentGO);
		}
		this.currentGO = UnityEngine.Object.Instantiate<GameObject>(this.particleSystems[this.currentIndex].particleSystemGO, this.spawnLocation.position + this.particleSystems[this.currentIndex].particlePosition, Quaternion.Euler(this.particleSystems[this.currentIndex].particleRotation));
		this.gunGameObject.SetActive(this.particleSystems[this.currentIndex].isWeaponEffect);
		this.title.text = this.particleSystems[this.currentIndex].title;
		this.description.text = this.particleSystems[this.currentIndex].description;
		this.navigationDetails.text = this.currentIndex + 1 + " out of " + this.particleSystems.Length.ToString();
	}

	// Token: 0x0400002E RID: 46
	public ParticleExamples[] particleSystems;

	// Token: 0x0400002F RID: 47
	public GameObject gunGameObject;

	// Token: 0x04000030 RID: 48
	private int currentIndex;

	// Token: 0x04000031 RID: 49
	private GameObject currentGO;

	// Token: 0x04000032 RID: 50
	public Transform spawnLocation;

	// Token: 0x04000033 RID: 51
	public Text title;

	// Token: 0x04000034 RID: 52
	public Text description;

	// Token: 0x04000035 RID: 53
	public Text navigationDetails;
}
