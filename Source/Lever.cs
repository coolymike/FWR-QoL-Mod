using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class Lever : MonoBehaviour
{
	// Token: 0x06000226 RID: 550 RVA: 0x00003F23 File Offset: 0x00002123
	private void Start()
	{
		this.SetOrientation();
	}

	// Token: 0x06000227 RID: 551 RVA: 0x000109E8 File Offset: 0x0000EBE8
	private void SetOrientation()
	{
		switch (this.orientation)
		{
		case Lever.Orientation.Flat:
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, 0f);
			return;
		case Lever.Orientation.Up:
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x + 90f, base.transform.eulerAngles.y, base.transform.eulerAngles.y);
			return;
		case Lever.Orientation.Side:
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, 90f);
			return;
		default:
			return;
		}
	}

	// Token: 0x04000322 RID: 802
	public Lever.Orientation orientation;

	// Token: 0x0200006D RID: 109
	public enum Orientation
	{
		// Token: 0x04000324 RID: 804
		Flat,
		// Token: 0x04000325 RID: 805
		Up,
		// Token: 0x04000326 RID: 806
		Side
	}
}
