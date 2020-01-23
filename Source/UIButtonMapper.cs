using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000082 RID: 130
public class UIButtonMapper : MonoBehaviour
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000296 RID: 662 RVA: 0x0000445F File Offset: 0x0000265F
	// (set) Token: 0x06000297 RID: 663 RVA: 0x00004467 File Offset: 0x00002667
	private bool WaitForKeyPress
	{
		get
		{
			return this.waitForKeyPress;
		}
		set
		{
			if (this.waitForKeyPress == value)
			{
				return;
			}
			this.waitForKeyPress = value;
			this.OnWaitForKeyPress(value);
			if (value)
			{
				UIButtonMapper.ButtonMapSelectedHandler onButtonMapSelected = UIButtonMapper.OnButtonMapSelected;
				if (onButtonMapSelected == null)
				{
					return;
				}
				onButtonMapSelected(this.id);
			}
		}
	}

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000298 RID: 664 RVA: 0x00012EF4 File Offset: 0x000110F4
	// (remove) Token: 0x06000299 RID: 665 RVA: 0x00012F28 File Offset: 0x00011128
	public static event UIButtonMapper.ButtonMapSelectedHandler OnButtonMapSelected;

	// Token: 0x0600029A RID: 666 RVA: 0x00012F5C File Offset: 0x0001115C
	private void Start()
	{
		this.id = base.transform.GetInstanceID();
		this.menu.OnMenuActivated += this.OnThisMenu;
		UIButtonMapper.OnButtonMapSelected += this.OnSelectedButton;
		UIControls.OnReset += this.OnReset;
		this.OnReset();
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00004499 File Offset: 0x00002699
	private void OnDestroy()
	{
		this.menu.OnMenuActivated -= this.OnThisMenu;
		UIButtonMapper.OnButtonMapSelected -= this.OnSelectedButton;
		UIControls.OnReset -= this.OnReset;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x000044D4 File Offset: 0x000026D4
	private void OnThisMenu(bool Activated)
	{
		this.WaitForKeyPress = false;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x000044DD File Offset: 0x000026DD
	private void OnSelectedButton(int buttonID)
	{
		if (this.id != buttonID)
		{
			this.WaitForKeyPress = false;
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x000044EF File Offset: 0x000026EF
	private void OnReset()
	{
		this.WaitForKeyPress = false;
		this.OnWaitForKeyPress(this.WaitForKeyPress);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00012FBC File Offset: 0x000111BC
	public void OnWaitForKeyPress(bool isWaiting)
	{
		if (isWaiting)
		{
			this.buttonText.text = "Press A Key";
			return;
		}
		this.buttonText.text = this.GetAssignedButton(this.assignedButtonMap).ToString();
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00004504 File Offset: 0x00002704
	public void AssignNewKey()
	{
		this.WaitForKeyPress = true;
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x000044D4 File Offset: 0x000026D4
	private void CancelNewKeyAssignment()
	{
		this.WaitForKeyPress = false;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x0000450D File Offset: 0x0000270D
	private void Update()
	{
		this.WaitingOnKeyPress();
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x00013004 File Offset: 0x00011204
	private void WaitingOnKeyPress()
	{
		if (!this.WaitForKeyPress)
		{
			return;
		}
		if (!this.menu.Active || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.WaitForKeyPress = false;
		}
		if (Input.anyKeyDown)
		{
			foreach (object obj in Enum.GetValues(typeof(KeyCode)))
			{
				KeyCode keyCode = (KeyCode)obj;
				if (Input.GetKeyDown(keyCode))
				{
					this.AssignNewButton(this.assignedButtonMap, keyCode);
					this.WaitForKeyPress = false;
				}
			}
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x000130B4 File Offset: 0x000112B4
	private KeyCode GetAssignedButton(UIButtonMapper.Map buttonMap)
	{
		switch (buttonMap)
		{
		case UIButtonMapper.Map.RagdollToggle:
			return GameData.controls.toggleRagdoll;
		case UIButtonMapper.Map.SetSpawn:
			return GameData.controls.setSpawn;
		case UIButtonMapper.Map.BuildModeToggle:
			return GameData.controls.toggleBuildMode;
		case UIButtonMapper.Map.BuildItemMenuToggle:
			return GameData.controls.toggleBuildItemMenu;
		case UIButtonMapper.Map.RotateItemLeft:
			return GameData.controls.rotateItemLeft;
		case UIButtonMapper.Map.RotateItemRight:
			return GameData.controls.rotateItemRight;
		case UIButtonMapper.Map.SlowMotionToggle:
			return GameData.controls.toggleSlowMotion;
		case UIButtonMapper.Map.Zoom:
			return GameData.controls.zoom;
		case UIButtonMapper.Map.CameraSwitch:
			return GameData.controls.switchCamera;
		case UIButtonMapper.Map.GenerateCameraAngle:
			return GameData.controls.generateCameraAngle;
		case UIButtonMapper.Map.Interact:
			return GameData.controls.interact;
		case UIButtonMapper.Map.RemoveLastItem:
			return GameData.controls.removeLastItem;
		case UIButtonMapper.Map.MoveForward:
			return GameData.controls.moveForward;
		case UIButtonMapper.Map.MoveBackward:
			return GameData.controls.moveBackward;
		case UIButtonMapper.Map.MoveLeft:
			return GameData.controls.moveLeft;
		case UIButtonMapper.Map.MoveRight:
			return GameData.controls.moveRight;
		case UIButtonMapper.Map.Jump:
			return GameData.controls.jump;
		default:
			return KeyCode.None;
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x000131CC File Offset: 0x000113CC
	public void AssignNewButton(UIButtonMapper.Map buttonMap, KeyCode newKeyCode)
	{
		switch (buttonMap)
		{
		case UIButtonMapper.Map.RagdollToggle:
			GameData.controls.toggleRagdoll = newKeyCode;
			return;
		case UIButtonMapper.Map.SetSpawn:
			GameData.controls.setSpawn = newKeyCode;
			return;
		case UIButtonMapper.Map.BuildModeToggle:
			GameData.controls.toggleBuildMode = newKeyCode;
			return;
		case UIButtonMapper.Map.BuildItemMenuToggle:
			GameData.controls.toggleBuildItemMenu = newKeyCode;
			return;
		case UIButtonMapper.Map.RotateItemLeft:
			GameData.controls.rotateItemLeft = newKeyCode;
			return;
		case UIButtonMapper.Map.RotateItemRight:
			GameData.controls.rotateItemRight = newKeyCode;
			return;
		case UIButtonMapper.Map.SlowMotionToggle:
			GameData.controls.toggleSlowMotion = newKeyCode;
			return;
		case UIButtonMapper.Map.Zoom:
			GameData.controls.zoom = newKeyCode;
			return;
		case UIButtonMapper.Map.CameraSwitch:
			GameData.controls.switchCamera = newKeyCode;
			return;
		case UIButtonMapper.Map.GenerateCameraAngle:
			GameData.controls.generateCameraAngle = newKeyCode;
			return;
		case UIButtonMapper.Map.Interact:
			GameData.controls.interact = newKeyCode;
			return;
		case UIButtonMapper.Map.RemoveLastItem:
			GameData.controls.removeLastItem = newKeyCode;
			return;
		case UIButtonMapper.Map.MoveForward:
			GameData.controls.moveForward = newKeyCode;
			return;
		case UIButtonMapper.Map.MoveBackward:
			GameData.controls.moveBackward = newKeyCode;
			return;
		case UIButtonMapper.Map.MoveLeft:
			GameData.controls.moveLeft = newKeyCode;
			return;
		case UIButtonMapper.Map.MoveRight:
			GameData.controls.moveRight = newKeyCode;
			return;
		case UIButtonMapper.Map.Jump:
			GameData.controls.jump = newKeyCode;
			return;
		default:
			return;
		}
	}

	// Token: 0x040003EF RID: 1007
	private int id;

	// Token: 0x040003F0 RID: 1008
	public UIMenu menu;

	// Token: 0x040003F1 RID: 1009
	public UIButtonMapper.Map assignedButtonMap;

	// Token: 0x040003F2 RID: 1010
	public Text buttonText;

	// Token: 0x040003F3 RID: 1011
	private bool waitForKeyPress;

	// Token: 0x02000083 RID: 131
	// (Invoke) Token: 0x060002A8 RID: 680
	public delegate void ButtonMapSelectedHandler(int buttonID);

	// Token: 0x02000084 RID: 132
	public enum Map
	{
		// Token: 0x040003F6 RID: 1014
		RagdollToggle,
		// Token: 0x040003F7 RID: 1015
		SetSpawn,
		// Token: 0x040003F8 RID: 1016
		BuildModeToggle,
		// Token: 0x040003F9 RID: 1017
		BuildItemMenuToggle,
		// Token: 0x040003FA RID: 1018
		RotateItemLeft,
		// Token: 0x040003FB RID: 1019
		RotateItemRight,
		// Token: 0x040003FC RID: 1020
		RemoveLastItem = 11,
		// Token: 0x040003FD RID: 1021
		SlowMotionToggle = 6,
		// Token: 0x040003FE RID: 1022
		Zoom,
		// Token: 0x040003FF RID: 1023
		CameraSwitch,
		// Token: 0x04000400 RID: 1024
		GenerateCameraAngle,
		// Token: 0x04000401 RID: 1025
		Interact,
		// Token: 0x04000402 RID: 1026
		MoveForward = 12,
		// Token: 0x04000403 RID: 1027
		MoveBackward,
		// Token: 0x04000404 RID: 1028
		MoveLeft,
		// Token: 0x04000405 RID: 1029
		MoveRight,
		// Token: 0x04000406 RID: 1030
		Jump
	}
}
