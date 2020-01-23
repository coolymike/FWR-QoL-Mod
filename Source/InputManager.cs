using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class InputManager : MonoBehaviour
{
	// Token: 0x06000584 RID: 1412 RVA: 0x000066E9 File Offset: 0x000048E9
	public static bool MenuIsActive(bool _bypassTimeCheck = false)
	{
		if (UIMenuManager.AnyMenuActive())
		{
			InputManager.menuCooldown = 200;
		}
		else
		{
			InputManager.menuCooldown--;
		}
		return InputManager.menuCooldown > 0;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0001BE04 File Offset: 0x0001A004
	public static Vector2 Move(bool allowSlowDown = true)
	{
		if (InputManager.MenuIsActive(true))
		{
			return new Vector2(0f, 0f);
		}
		InputManager.move = new Vector2(InputManager.HorizontalMovement() + InputManager.TouchInput.LeftScreen().x, InputManager.VerticalMovement() + InputManager.TouchInput.LeftScreen().y);
		if (InputManager.move.magnitude > 1f)
		{
			if (InputManager.RunningEnabled)
			{
				return InputManager.move.normalized * ((InputManager.LeftShift() && allowSlowDown && !PlayerController.flyMode && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled) ? 3f : 1f);
			}
			return InputManager.move.normalized * ((InputManager.LeftShift() && allowSlowDown && !PlayerController.flyMode && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled) ? 0.2f : 1f);
		}
		else
		{
			if (InputManager.RunningEnabled)
			{
				return InputManager.move * ((InputManager.LeftShift() && allowSlowDown && !PlayerController.flyMode && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled) ? 3f : 1f);
			}
			return InputManager.move * ((InputManager.LeftShift() && allowSlowDown && !PlayerController.flyMode && !PlayerRagdollMode.playerSettings.simulatedRagdoll.RagdollModeEnabled) ? 0.2f : 1f);
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00006712 File Offset: 0x00004912
	public static Vector2 MoveLerp(bool allowSlowDown = true)
	{
		InputManager.moveLerp = Vector2.Lerp(InputManager.moveLerp, InputManager.Move(allowSlowDown), InputManager.moveAccelerationSpeed * Time.deltaTime);
		return InputManager.moveLerp;
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00006739 File Offset: 0x00004939
	public static bool LeftShift()
	{
		return !InputManager.MenuIsActive(true) && (Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.shift);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001BF60 File Offset: 0x0001A160
	public static Vector2 Look()
	{
		if (InputManager.MenuIsActive(false))
		{
			return new Vector3(0f, 0f);
		}
		if (Application.isMobilePlatform)
		{
			InputManager.look = InputManager.TouchInput.RightScreen() * InputManager.mouseLookSensitivityMultiplier * GameData.controls.lookSensitivity;
		}
		else
		{
			InputManager.look = InputManager.PcInput.MouseLook() * InputManager.mouseLookSensitivityMultiplier * GameData.controls.lookSensitivity;
		}
		if (GameData.controls.invertLookX)
		{
			InputManager.look.x = InputManager.look.x * -1f;
		}
		if (!GameData.controls.invertLookY)
		{
			InputManager.look.y = InputManager.look.y * -1f;
		}
		return InputManager.look;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001C02C File Offset: 0x0001A22C
	public static float VerticalMovement()
	{
		float result = 0f;
		if (Input.GetKey(GameData.controls.moveForward) && Input.GetKey(GameData.controls.moveBackward))
		{
			result = 0f;
		}
		else if (Input.GetKey(GameData.controls.moveForward))
		{
			result = 1f;
		}
		else if (Input.GetKey(GameData.controls.moveBackward))
		{
			result = -1f;
		}
		return result;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001C09C File Offset: 0x0001A29C
	public static float HorizontalMovement()
	{
		float result = 0f;
		if (Input.GetKey(GameData.controls.moveLeft) && Input.GetKey(GameData.controls.moveRight))
		{
			result = 0f;
		}
		else if (Input.GetKey(GameData.controls.moveRight))
		{
			result = 1f;
		}
		else if (Input.GetKey(GameData.controls.moveLeft))
		{
			result = -1f;
		}
		return result;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00006758 File Offset: 0x00004958
	public static bool Jumping()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.jumping || InputManager.ConsoleInput.XDown());
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00006783 File Offset: 0x00004983
	public static bool Jump()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.jump) || InputManager.TouchInput.jump);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x000067A7 File Offset: 0x000049A7
	public static bool ToggleRagdollMode()
	{
		return (!InputManager.MenuIsActive(false) || LevelManager.StatusIsWinOrLose(true)) && (InputManager.ConsoleInput.TriangleDown() || Input.GetKeyDown(GameData.controls.toggleRagdoll) || InputManager.TouchInput.toggleRagdoll);
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x000067DA File Offset: 0x000049DA
	public static bool CameraToggle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.switchCamera);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x000067F0 File Offset: 0x000049F0
	public static bool NewCameraAngle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.generateCameraAngle);
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0000680B File Offset: 0x00004A0B
	public static bool SetSpawn()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.setSpawn) || InputManager.ConsoleInput.CircleDown() || InputManager.TouchInput.setSpawn);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00006836 File Offset: 0x00004A36
	public static bool CameraZoom()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(GameData.controls.zoom));
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0000685A File Offset: 0x00004A5A
	public static bool Grabbing()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick());
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00006874 File Offset: 0x00004A74
	public static bool SlowMotionToggle()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown("joystick button 10") || Input.GetKeyDown(GameData.controls.toggleSlowMotion) || InputManager.TouchInput.toggleSlowMotion);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x000068A4 File Offset: 0x00004AA4
	public static bool ToggleBuildItemMenu()
	{
		return Input.GetKeyDown(GameData.controls.toggleBuildItemMenu) || InputManager.TouchInput.toggleBuildItemMenu;
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x000068BE File Offset: 0x00004ABE
	public static bool MountVehicle()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.SquareDown() || InputManager.TouchInput.interact || Input.GetKeyDown(GameData.controls.interact));
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000068E9 File Offset: 0x00004AE9
	public static bool VehicleA()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2() || Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.vehicleA || InputManager.PcInput.LeftClick());
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0000691B File Offset: 0x00004B1B
	public static bool VehicleB()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.vehicleB || InputManager.PcInput.RightClick());
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00006948 File Offset: 0x00004B48
	public static bool Break()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || Input.GetKey(KeyCode.LeftControl));
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00006967 File Offset: 0x00004B67
	public static bool PlaceObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick() || InputManager.TouchInput.place);
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00006988 File Offset: 0x00004B88
	public static bool RemoveObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || InputManager.PcInput.RightClick() || InputManager.TouchInput.remove);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x000069A9 File Offset: 0x00004BA9
	public static bool RotateObjectRight()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2Down() || Input.GetKeyDown(GameData.controls.rotateItemRight) || InputManager.TouchInput.rotateRight);
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x000069D4 File Offset: 0x00004BD4
	public static bool RotateObjectLeft()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2Down() || Input.GetKeyDown(GameData.controls.rotateItemLeft) || InputManager.TouchInput.rotateLeft);
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x000069FF File Offset: 0x00004BFF
	public static bool ToggleBuildMode()
	{
		return InputManager.ConsoleInput.MiddlePadDown() || Input.GetKeyDown(GameData.controls.toggleBuildMode) || InputManager.TouchInput.toggleBuildMode;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x00006A20 File Offset: 0x00004C20
	public static bool RemoveLastItem()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.removeLastItem);
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00006A3B File Offset: 0x00004C3B
	public static bool Escape()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x00006A3B File Offset: 0x00004C3B
	public static bool TogglePauseMenu()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x00006A54 File Offset: 0x00004C54
	public static bool Back()
	{
		return InputManager.ConsoleInput.CircleDown() || Input.GetKeyDown(KeyCode.Escape);
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001C10C File Offset: 0x0001A30C
	static InputManager()
	{
		InputManager.moveAccelerationSpeed = 6f;
		InputManager.mouseLookSensitivityMultiplier = 10f;
		InputManager.menuCooldown = 500;
		InputManager.GridToggleKey = KeyCode.O;
		InputManager.RotXPosKey = KeyCode.C;
		InputManager.switchCamera = KeyCode.U;
		InputManager.RotXNegKey = KeyCode.V;
		InputManager.RotZPosKey = KeyCode.B;
		InputManager.RotZNegKey = KeyCode.N;
		InputManager.SmallRotKey = KeyCode.I;
		InputManager.DisableFlyKey = KeyCode.P;
		InputManager.XPosToggleKey = KeyCode.G;
		InputManager.YPosToggleKey = KeyCode.H;
		InputManager.ZPosToggleKey = KeyCode.J;
		InputManager.PlayModeFlyDisabled = false;
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00006A66 File Offset: 0x00004C66
	public static bool RotateObjectXPositive()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotXPosKey);
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00006A7C File Offset: 0x00004C7C
	public static bool RotateObjectXNegative()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotXNegKey);
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x00006A92 File Offset: 0x00004C92
	public static bool RotateObjectZPositive()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotZPosKey);
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00006AA8 File Offset: 0x00004CA8
	public static bool RotateObjectZNegative()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotZNegKey);
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00006ABE File Offset: 0x00004CBE
	public static bool SmallRotation()
	{
		if (InputManager.MenuIsActive(false) || !Input.GetKeyDown(InputManager.SmallRotKey))
		{
			return !InputManager.SmallRotationActive;
		}
		if (InputManager.SmallRotationActive)
		{
			InputManager.SmallRotationActive = false;
			return false;
		}
		InputManager.SmallRotationActive = true;
		return true;
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00006AF3 File Offset: 0x00004CF3
	public static bool FlyInPlayMode()
	{
		return InputManager.PlayModeFlyDisabled;
	}

	// Token: 0x060005AA RID: 1450 RVA: 0x00006AFA File Offset: 0x00004CFA
	public static bool DisableFly()
	{
		return Input.GetKeyDown(InputManager.DisableFlyKey);
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x00006B06 File Offset: 0x00004D06
	public static bool GridToggle()
	{
		if (InputManager.MenuIsActive(false) || !Input.GetKeyDown(InputManager.GridToggleKey))
		{
			return !InputManager.GridToggleActive;
		}
		if (InputManager.GridToggleActive)
		{
			InputManager.GridToggleActive = false;
			return false;
		}
		InputManager.GridToggleActive = true;
		return true;
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00006B3B File Offset: 0x00004D3B
	public static bool DebugKeyHeld()
	{
		return Input.GetKey(KeyCode.Home);
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00006B47 File Offset: 0x00004D47
	public static bool XPosToggle()
	{
		if (InputManager.MenuIsActive(false) || !Input.GetKeyDown(InputManager.XPosToggleKey))
		{
			return !InputManager.XPosToggleActive;
		}
		if (InputManager.XPosToggleActive)
		{
			InputManager.XPosToggleActive = false;
			return false;
		}
		InputManager.XPosToggleActive = true;
		return true;
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x00006B7C File Offset: 0x00004D7C
	public static bool YPosToggle()
	{
		if (InputManager.MenuIsActive(false) || !Input.GetKeyDown(InputManager.YPosToggleKey))
		{
			return !InputManager.YPosToggleActive;
		}
		if (InputManager.YPosToggleActive)
		{
			InputManager.YPosToggleActive = false;
			return false;
		}
		InputManager.YPosToggleActive = true;
		return true;
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00006BB1 File Offset: 0x00004DB1
	public static bool ZPosToggle()
	{
		if (InputManager.MenuIsActive(false) || !Input.GetKeyDown(InputManager.ZPosToggleKey))
		{
			return !InputManager.ZPosToggleActive;
		}
		if (InputManager.ZPosToggleActive)
		{
			InputManager.ZPosToggleActive = false;
			return false;
		}
		InputManager.ZPosToggleActive = true;
		return true;
	}

	// Token: 0x040006F0 RID: 1776
	private static Vector2 move;

	// Token: 0x040006F1 RID: 1777
	private static Vector2 moveLerp;

	// Token: 0x040006F2 RID: 1778
	private static readonly float moveAccelerationSpeed;

	// Token: 0x040006F3 RID: 1779
	private static readonly float mouseLookSensitivityMultiplier;

	// Token: 0x040006F4 RID: 1780
	public static int menuCooldown;

	// Token: 0x040006F5 RID: 1781
	private static Vector2 look;

	// Token: 0x040006F6 RID: 1782
	private static bool SmallRotationActive;

	// Token: 0x040006F7 RID: 1783
	private static bool FlyInPlayModeActive;

	// Token: 0x040006F8 RID: 1784
	public static KeyCode RotXPosKey;

	// Token: 0x040006F9 RID: 1785
	public static KeyCode RotXNegKey;

	// Token: 0x040006FA RID: 1786
	public static KeyCode RotZPosKey;

	// Token: 0x040006FB RID: 1787
	public static KeyCode RotZNegKey;

	// Token: 0x040006FC RID: 1788
	public static KeyCode SmallRotKey;

	// Token: 0x040006FD RID: 1789
	public static KeyCode DisableFlyKey;

	// Token: 0x040006FE RID: 1790
	public static bool PlayModeFlyDisabled;

	// Token: 0x040006FF RID: 1791
	private static bool GridToggleActive;

	// Token: 0x04000700 RID: 1792
	public static KeyCode GridToggleKey;

	// Token: 0x04000701 RID: 1793
	public static KeyCode XPosToggleKey;

	// Token: 0x04000702 RID: 1794
	public static KeyCode YPosToggleKey;

	// Token: 0x04000703 RID: 1795
	public static KeyCode ZPosToggleKey;

	// Token: 0x04000704 RID: 1796
	public static bool XPosToggleActive;

	// Token: 0x04000705 RID: 1797
	public static bool YPosToggleActive;

	// Token: 0x04000706 RID: 1798
	public static bool ZPosToggleActive;

	// Token: 0x04000707 RID: 1799
	public static KeyCode switchCamera;

	// Token: 0x04000708 RID: 1800
	public static bool RunningEnabled = true;

	// Token: 0x0200010D RID: 269
	public static class PcInput
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x00006BE6 File Offset: 0x00004DE6
		public static bool LeftClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse0);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00006BFB File Offset: 0x00004DFB
		public static bool RightClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse1);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00006C10 File Offset: 0x00004E10
		public static Vector2 MouseLook()
		{
			return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		}
	}

	// Token: 0x0200010E RID: 270
	public static class ConsoleInput
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00006C2B File Offset: 0x00004E2B
		public static bool OptionsDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton9);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00006C37 File Offset: 0x00004E37
		public static bool XDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton1);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00006C43 File Offset: 0x00004E43
		public static bool CircleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton2);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00006C4F File Offset: 0x00004E4F
		public static bool SquareDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton0);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00006C5B File Offset: 0x00004E5B
		public static bool TriangleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton3);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00006C67 File Offset: 0x00004E67
		public static bool MiddlePadDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton13);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00006C73 File Offset: 0x00004E73
		public static bool R1()
		{
			return Input.GetKey(KeyCode.JoystickButton5);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00006C7F File Offset: 0x00004E7F
		public static bool R2()
		{
			return Input.GetKey(KeyCode.JoystickButton7);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00006C8B File Offset: 0x00004E8B
		public static bool R2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton7);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00006C97 File Offset: 0x00004E97
		public static bool L1()
		{
			return Input.GetKey(KeyCode.JoystickButton4);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00006CA3 File Offset: 0x00004EA3
		public static bool L2()
		{
			return Input.GetKey(KeyCode.JoystickButton6);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00006CAF File Offset: 0x00004EAF
		public static bool L2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton6);
		}
	}

	// Token: 0x0200010F RID: 271
	public static class TouchInput
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0001C190 File Offset: 0x0001A390
		public static Vector2 LeftScreen()
		{
			if (Input.touchCount == 0)
			{
				InputManager.TouchInput.setMoveStart = false;
			}
			for (int i = 0; i < Input.touches.Length; i++)
			{
				if (!InputManager.TouchInput.setMoveStart || Input.touches[i].fingerId == InputManager.TouchInput.moveStartFingerId)
				{
					if (Input.touches[i].position.x < (float)Screen.width * 0.5f && Input.touches[i].position.y < (float)Screen.height * 0.9f)
					{
						InputManager.TouchInput.moveStartFingerId = Input.touches[i].fingerId;
						if (!InputManager.TouchInput.setMoveStart)
						{
							InputManager.TouchInput.moveStart = Input.touches[i].position;
							InputManager.TouchInput.setMoveStart = true;
						}
						return new Vector2(Mathf.Clamp((Input.touches[i].position.x - InputManager.TouchInput.moveStart.x) / ((float)Screen.height * 0.18f), -1f, 1f), Mathf.Clamp((Input.touches[i].position.y - InputManager.TouchInput.moveStart.y) / ((float)Screen.height * 0.18f), -1f, 1f));
					}
					InputManager.TouchInput.setMoveStart = false;
				}
			}
			return new Vector2(0f, 0f);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public static Vector2 RightScreen()
		{
			for (int i = 0; i < Input.touches.Length; i++)
			{
				Debug.Log(Input.touches[i].position.x + " : " + (Input.touches[i].position.x > (float)Screen.width * 0.5f).ToString());
				if (Input.touches[i].position.x > (float)Screen.width * 0.5f)
				{
					return Input.touches[i].deltaPosition / ((float)Screen.height * 0.02f);
				}
			}
			return new Vector2(0f, 0f);
		}

		// Token: 0x04000709 RID: 1801
		public static bool tapToJump = true;

		// Token: 0x0400070A RID: 1802
		private static Vector2 moveStart = new Vector2(0f, 0f);

		// Token: 0x0400070B RID: 1803
		private static bool setMoveStart = false;

		// Token: 0x0400070C RID: 1804
		private static int moveStartFingerId;

		// Token: 0x0400070D RID: 1805
		public static Vector2 lookStart = new Vector2(0f, 0f);

		// Token: 0x0400070E RID: 1806
		private static bool setLookStart = false;

		// Token: 0x0400070F RID: 1807
		public static bool jump;

		// Token: 0x04000710 RID: 1808
		public static bool jumping;

		// Token: 0x04000711 RID: 1809
		public static bool shift;

		// Token: 0x04000712 RID: 1810
		public static bool interact;

		// Token: 0x04000713 RID: 1811
		public static bool toggleRagdoll;

		// Token: 0x04000714 RID: 1812
		public static bool toggleBuildMode;

		// Token: 0x04000715 RID: 1813
		public static bool toggleBuildItemMenu;

		// Token: 0x04000716 RID: 1814
		public static bool toggleSlowMotion;

		// Token: 0x04000717 RID: 1815
		public static bool escape;

		// Token: 0x04000718 RID: 1816
		public static bool setSpawn;

		// Token: 0x04000719 RID: 1817
		public static bool place;

		// Token: 0x0400071A RID: 1818
		public static bool remove;

		// Token: 0x0400071B RID: 1819
		public static bool rotateRight;

		// Token: 0x0400071C RID: 1820
		public static bool rotateLeft;

		// Token: 0x0400071D RID: 1821
		public static bool vehicleA;

		// Token: 0x0400071E RID: 1822
		public static bool vehicleB;

		// Token: 0x0400071F RID: 1823
		private static readonly Rect screenLeft = new Rect(0f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);

		// Token: 0x04000720 RID: 1824
		private static readonly Rect screenRight = new Rect((float)Screen.width * 0.5f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);
	}
}
