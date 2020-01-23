using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000087 RID: 135
public class UIControls : MonoBehaviour
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060002B3 RID: 691 RVA: 0x000135B8 File Offset: 0x000117B8
	// (remove) Token: 0x060002B4 RID: 692 RVA: 0x000135EC File Offset: 0x000117EC
	public static event UIControls.ResetHandler OnReset;

	// Token: 0x060002B5 RID: 693 RVA: 0x000045F4 File Offset: 0x000027F4
	private void Start()
	{
		GameData.LoadControls();
		this.menu.OnMenuActivated += this.OnThisMenu;
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00004612 File Offset: 0x00002812
	private void OnDestroy()
	{
		this.menu.OnMenuActivated -= this.OnThisMenu;
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x0000462B File Offset: 0x0000282B
	private void OnThisMenu(bool activated)
	{
		if (activated)
		{
			this.FillData();
		}
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00013620 File Offset: 0x00011820
	private void FillData()
	{
		this.lookSensitivityX.value = GameData.controls.lookSensitivity.x;
		this.lookSensitivityY.value = GameData.controls.lookSensitivity.y;
		this.invertX.isOn = GameData.controls.invertLookX;
		this.invertY.isOn = GameData.controls.invertLookY;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0001368C File Offset: 0x0001188C
	private void RecordData()
	{
		GameData.controls.lookSensitivity.x = this.lookSensitivityX.value;
		GameData.controls.lookSensitivity.y = this.lookSensitivityY.value;
		GameData.controls.invertLookX = this.invertX.isOn;
		GameData.controls.invertLookY = this.invertY.isOn;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00004636 File Offset: 0x00002836
	public void SaveChanges()
	{
		this.RecordData();
		GameData.SaveControls();
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00004643 File Offset: 0x00002843
	public void ResetData()
	{
		GameData.ResetControls();
		this.FillData();
		UIControls.ResetHandler onReset = UIControls.OnReset;
		if (onReset == null)
		{
			return;
		}
		onReset();
	}

	// Token: 0x0400041E RID: 1054
	public UIMenu menu;

	// Token: 0x0400041F RID: 1055
	public Slider lookSensitivityX;

	// Token: 0x04000420 RID: 1056
	public Slider lookSensitivityY;

	// Token: 0x04000421 RID: 1057
	public Toggle invertX;

	// Token: 0x04000422 RID: 1058
	public Toggle invertY;

	// Token: 0x02000088 RID: 136
	// (Invoke) Token: 0x060002BE RID: 702
	public delegate void ResetHandler();
}
