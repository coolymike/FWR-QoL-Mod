using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class InputManager : MonoBehaviour
{
	// Token: 0x06000570 RID: 1392
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

	// Token: 0x06000571 RID: 1393
	public static Vector2 Move(bool allowSlowDown = true)
	{
		if (InputManager.MenuIsActive(true))
		{
			return new Vector2(0f, 0f);
		}
		InputManager.move = new Vector2(InputManager.HorizontalMovement() + InputManager.TouchInput.LeftScreen().x, InputManager.VerticalMovement() + InputManager.TouchInput.LeftScreen().y);
		if (InputManager.move.magnitude > 1f)
		{
			return InputManager.move.normalized * ((InputManager.LeftShift() && allowSlowDown) ? 0.2f : 1f);
		}
		return InputManager.move * ((InputManager.LeftShift() && allowSlowDown) ? 0.2f : 1f);
	}

	// Token: 0x06000572 RID: 1394
	public static Vector2 MoveLerp(bool allowSlowDown = true)
	{
		InputManager.moveLerp = Vector2.Lerp(InputManager.moveLerp, InputManager.Move(allowSlowDown), InputManager.moveAccelerationSpeed * Time.deltaTime);
		return InputManager.moveLerp;
	}

	// Token: 0x06000573 RID: 1395
	public static bool LeftShift()
	{
		return !InputManager.MenuIsActive(true) && (Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.shift);
	}

	// Token: 0x06000574 RID: 1396
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

	// Token: 0x06000575 RID: 1397
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

	// Token: 0x06000576 RID: 1398
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

	// Token: 0x06000577 RID: 1399
	public static bool Jumping()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.jumping || InputManager.ConsoleInput.XDown());
	}

	// Token: 0x06000578 RID: 1400
	public static bool Jump()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.jump) || InputManager.TouchInput.jump);
	}

	// Token: 0x06000579 RID: 1401
	public static bool ToggleRagdollMode()
	{
		return (!InputManager.MenuIsActive(false) || LevelManager.StatusIsWinOrLose(true)) && (InputManager.ConsoleInput.TriangleDown() || Input.GetKeyDown(GameData.controls.toggleRagdoll) || InputManager.TouchInput.toggleRagdoll);
	}

	// Token: 0x0600057A RID: 1402
	public static bool CameraToggle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.switchCamera);
	}

	// Token: 0x0600057B RID: 1403
	public static bool NewCameraAngle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.generateCameraAngle);
	}

	// Token: 0x0600057C RID: 1404
	public static bool SetSpawn()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.setSpawn) || InputManager.ConsoleInput.CircleDown() || InputManager.TouchInput.setSpawn);
	}

	// Token: 0x0600057D RID: 1405
	public static bool CameraZoom()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(GameData.controls.zoom));
	}

	// Token: 0x0600057E RID: 1406
	public static bool Grabbing()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick());
	}

	// Token: 0x0600057F RID: 1407
	public static bool SlowMotionToggle()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown("joystick button 10") || Input.GetKeyDown(GameData.controls.toggleSlowMotion) || InputManager.TouchInput.toggleSlowMotion);
	}

	// Token: 0x06000580 RID: 1408
	public static bool ToggleBuildItemMenu()
	{
		return Input.GetKeyDown(GameData.controls.toggleBuildItemMenu) || InputManager.TouchInput.toggleBuildItemMenu;
	}

	// Token: 0x06000581 RID: 1409
	public static bool MountVehicle()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.SquareDown() || InputManager.TouchInput.interact || Input.GetKeyDown(GameData.controls.interact));
	}

	// Token: 0x06000582 RID: 1410
	public static bool VehicleA()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2() || Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.vehicleA || InputManager.PcInput.LeftClick());
	}

	// Token: 0x06000583 RID: 1411
	public static bool VehicleB()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.vehicleB || InputManager.PcInput.RightClick());
	}

	// Token: 0x06000584 RID: 1412
	public static bool Break()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || Input.GetKey(KeyCode.LeftControl));
	}

	// Token: 0x06000585 RID: 1413
	public static bool PlaceObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick() || InputManager.TouchInput.place);
	}

	// Token: 0x06000586 RID: 1414
	public static bool RemoveObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || InputManager.PcInput.RightClick() || InputManager.TouchInput.remove);
	}

	// Token: 0x06000587 RID: 1415
	public static bool RotateObjectRight()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2Down() || Input.GetKeyDown(GameData.controls.rotateItemRight) || InputManager.TouchInput.rotateRight);
	}

	// Token: 0x06000588 RID: 1416
	public static bool RotateObjectLeft()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2Down() || Input.GetKeyDown(GameData.controls.rotateItemLeft) || InputManager.TouchInput.rotateLeft);
	}

	// Token: 0x06000589 RID: 1417
	public static bool ToggleBuildMode()
	{
		return InputManager.ConsoleInput.MiddlePadDown() || Input.GetKeyDown(GameData.controls.toggleBuildMode) || InputManager.TouchInput.toggleBuildMode;
	}

	// Token: 0x0600058A RID: 1418
	public static bool RemoveLastItem()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.removeLastItem);
	}

	// Token: 0x0600058B RID: 1419
	public static bool Escape()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x0600058C RID: 1420
	public static bool TogglePauseMenu()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x0600058D RID: 1421
	public static bool Back()
	{
		return InputManager.ConsoleInput.CircleDown() || Input.GetKeyDown(KeyCode.Escape);
	}

	// Token: 0x0600058F RID: 1423
	static InputManager()
	{
		InputManager.RotXPosKey = KeyCode.C;
		InputManager.RotXNegKey = KeyCode.V;
		InputManager.RotZPosKey = KeyCode.B;
		InputManager.RotZNegKey = KeyCode.N;
		InputManager.SmallRotKey = KeyCode.I;
		InputManager.DisableFlyKey = KeyCode.P;
		InputManager.PlayModeFlyDisabled = false;
	}

	// Token: 0x06000590 RID: 1424
	public static bool RotateObjectXPositive()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotXPosKey);
	}

	// Token: 0x06000591 RID: 1425
	public static bool RotateObjectXNegative()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotXNegKey);
	}

	// Token: 0x06000592 RID: 1426
	public static bool RotateObjectZPositive()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotZPosKey);
	}

	// Token: 0x06000593 RID: 1427
	public static bool RotateObjectZNegative()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(InputManager.RotZNegKey);
	}

	// Token: 0x06000594 RID: 1428
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

	// Token: 0x06000595 RID: 1429
	public static bool FlyInPlayMode()
	{
		return InputManager.PlayModeFlyDisabled;
	}

	// Token: 0x06000596 RID: 1430
	public static bool DisableFly()
	{
		return Input.GetKeyDown(InputManager.DisableFlyKey);
	}

	// Token: 0x06000597 RID: 1431
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

	// Token: 0x06000808 RID: 2056
	public static bool DebugKeyHeld()
	{
		return Input.GetKey(KeyCode.Home);
	}

	// Token: 0x040006E0 RID: 1760
	private static Vector2 move;

	// Token: 0x040006E1 RID: 1761
	private static Vector2 moveLerp;

	// Token: 0x040006E2 RID: 1762
	private static readonly float moveAccelerationSpeed = 6f;

	// Token: 0x040006E3 RID: 1763
	private static readonly float mouseLookSensitivityMultiplier = 10f;

	// Token: 0x040006E4 RID: 1764
	public static int menuCooldown = 500;

	// Token: 0x040006E5 RID: 1765
	private static Vector2 look;

	// Token: 0x040006E6 RID: 1766
	private static bool SmallRotationActive;

	// Token: 0x040006E7 RID: 1767
	private static bool FlyInPlayModeActive;

	// Token: 0x040006E8 RID: 1768
	public static KeyCode RotXPosKey;

	// Token: 0x040006E9 RID: 1769
	public static KeyCode RotXNegKey;

	// Token: 0x040006EA RID: 1770
	public static KeyCode RotZPosKey;

	// Token: 0x040006EB RID: 1771
	public static KeyCode RotZNegKey;

	// Token: 0x040006EC RID: 1772
	public static KeyCode SmallRotKey;

	// Token: 0x040006ED RID: 1773
	public static KeyCode DisableFlyKey;

	// Token: 0x040006EE RID: 1774
	public static bool PlayModeFlyDisabled;

	// Token: 0x040006EF RID: 1775
	private static bool GridToggleActive;

	// Token: 0x040006F0 RID: 1776
	public static KeyCode GridToggleKey = KeyCode.O;

	// Token: 0x0200010A RID: 266
	public static class PcInput
	{
		// Token: 0x06000598 RID: 1432
		public static bool LeftClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse0);
		}

		// Token: 0x06000599 RID: 1433
		public static bool RightClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse1);
		}

		// Token: 0x0600059A RID: 1434
		public static Vector2 MouseLook()
		{
			return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		}
	}

	// Token: 0x0200010B RID: 267
	public static class ConsoleInput
	{
		// Token: 0x0600059B RID: 1435
		public static bool OptionsDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton9);
		}

		// Token: 0x0600059C RID: 1436
		public static bool XDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton1);
		}

		// Token: 0x0600059D RID: 1437
		public static bool CircleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton2);
		}

		// Token: 0x0600059E RID: 1438
		public static bool SquareDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton0);
		}

		// Token: 0x0600059F RID: 1439
		public static bool TriangleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton3);
		}

		// Token: 0x060005A0 RID: 1440
		public static bool MiddlePadDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton13);
		}

		// Token: 0x060005A1 RID: 1441
		public static bool R1()
		{
			return Input.GetKey(KeyCode.JoystickButton5);
		}

		// Token: 0x060005A2 RID: 1442
		public static bool R2()
		{
			return Input.GetKey(KeyCode.JoystickButton7);
		}

		// Token: 0x060005A3 RID: 1443
		public static bool R2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton7);
		}

		// Token: 0x060005A4 RID: 1444
		public static bool L1()
		{
			return Input.GetKey(KeyCode.JoystickButton4);
		}

		// Token: 0x060005A5 RID: 1445
		public static bool L2()
		{
			return Input.GetKey(KeyCode.JoystickButton6);
		}

		// Token: 0x060005A6 RID: 1446
		public static bool L2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton6);
		}
	}

	// Token: 0x0200010C RID: 268
	public static class TouchInput
	{
		// Token: 0x060005A7 RID: 1447
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

		// Token: 0x060005A8 RID: 1448
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

		// Token: 0x040006F1 RID: 1777
		public static bool tapToJump = true;

		// Token: 0x040006F2 RID: 1778
		private static Vector2 moveStart = new Vector2(0f, 0f);

		// Token: 0x040006F3 RID: 1779
		private static bool setMoveStart = false;

		// Token: 0x040006F4 RID: 1780
		private static int moveStartFingerId;

		// Token: 0x040006F5 RID: 1781
		public static Vector2 lookStart = new Vector2(0f, 0f);

		// Token: 0x040006F6 RID: 1782
		private static bool setLookStart = false;

		// Token: 0x040006F7 RID: 1783
		public static bool jump;

		// Token: 0x040006F8 RID: 1784
		public static bool jumping;

		// Token: 0x040006F9 RID: 1785
		public static bool shift;

		// Token: 0x040006FA RID: 1786
		public static bool interact;

		// Token: 0x040006FB RID: 1787
		public static bool toggleRagdoll;

		// Token: 0x040006FC RID: 1788
		public static bool toggleBuildMode;

		// Token: 0x040006FD RID: 1789
		public static bool toggleBuildItemMenu;

		// Token: 0x040006FE RID: 1790
		public static bool toggleSlowMotion;

		// Token: 0x040006FF RID: 1791
		public static bool escape;

		// Token: 0x04000700 RID: 1792
		public static bool setSpawn;

		// Token: 0x04000701 RID: 1793
		public static bool place;

		// Token: 0x04000702 RID: 1794
		public static bool remove;

		// Token: 0x04000703 RID: 1795
		public static bool rotateRight;

		// Token: 0x04000704 RID: 1796
		public static bool rotateLeft;

		// Token: 0x04000705 RID: 1797
		public static bool vehicleA;

		// Token: 0x04000706 RID: 1798
		public static bool vehicleB;

		// Token: 0x04000707 RID: 1799
		private static readonly Rect screenLeft = new Rect(0f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);

		// Token: 0x04000708 RID: 1800
		private static readonly Rect screenRight = new Rect((float)Screen.width * 0.5f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);
	}
}
