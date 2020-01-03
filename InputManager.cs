using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class InputManager : MonoBehaviour
{
	// Token: 0x06000569 RID: 1385 RVA: 0x00006798 File Offset: 0x00004998
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

	// Token: 0x0600056A RID: 1386 RVA: 0x0001AD98 File Offset: 0x00018F98
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

	// Token: 0x0600056B RID: 1387 RVA: 0x000067C1 File Offset: 0x000049C1
	public static Vector2 MoveLerp(bool allowSlowDown = true)
	{
		InputManager.moveLerp = Vector2.Lerp(InputManager.moveLerp, InputManager.Move(allowSlowDown), InputManager.moveAccelerationSpeed * Time.deltaTime);
		return InputManager.moveLerp;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000067E8 File Offset: 0x000049E8
	public static bool LeftShift()
	{
		return !InputManager.MenuIsActive(true) && (Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.shift);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0001AE40 File Offset: 0x00019040
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

	// Token: 0x0600056E RID: 1390 RVA: 0x0001AEFC File Offset: 0x000190FC
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

	// Token: 0x0600056F RID: 1391 RVA: 0x0001AF6C File Offset: 0x0001916C
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

	// Token: 0x06000570 RID: 1392 RVA: 0x00006807 File Offset: 0x00004A07
	public static bool Jumping()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.jumping || InputManager.ConsoleInput.XDown());
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00006832 File Offset: 0x00004A32
	public static bool Jump()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.jump) || InputManager.TouchInput.jump);
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00006856 File Offset: 0x00004A56
	public static bool ToggleRagdollMode()
	{
		return (!InputManager.MenuIsActive(false) || LevelManager.StatusIsWinOrLose(true)) && (InputManager.ConsoleInput.TriangleDown() || Input.GetKeyDown(GameData.controls.toggleRagdoll) || InputManager.TouchInput.toggleRagdoll);
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00006889 File Offset: 0x00004A89
	public static bool CameraToggle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.switchCamera);
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x000068A4 File Offset: 0x00004AA4
	public static bool NewCameraAngle()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.generateCameraAngle);
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x000068BF File Offset: 0x00004ABF
	public static bool SetSpawn()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown(GameData.controls.setSpawn) || InputManager.ConsoleInput.CircleDown() || InputManager.TouchInput.setSpawn);
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x000068EA File Offset: 0x00004AEA
	public static bool CameraZoom()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(GameData.controls.zoom));
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0000690E File Offset: 0x00004B0E
	public static bool Grabbing()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick());
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00006928 File Offset: 0x00004B28
	public static bool SlowMotionToggle()
	{
		return !InputManager.MenuIsActive(false) && (Input.GetKeyDown("joystick button 10") || Input.GetKeyDown(GameData.controls.toggleSlowMotion) || InputManager.TouchInput.toggleSlowMotion);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00006958 File Offset: 0x00004B58
	public static bool ToggleBuildItemMenu()
	{
		return Input.GetKeyDown(GameData.controls.toggleBuildItemMenu) || InputManager.TouchInput.toggleBuildItemMenu;
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x00006972 File Offset: 0x00004B72
	public static bool MountVehicle()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.SquareDown() || InputManager.TouchInput.interact || Input.GetKeyDown(GameData.controls.interact));
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0000699D File Offset: 0x00004B9D
	public static bool VehicleA()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2() || Input.GetKey(GameData.controls.jump) || InputManager.TouchInput.vehicleA || InputManager.PcInput.LeftClick());
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x000069CF File Offset: 0x00004BCF
	public static bool VehicleB()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2() || Input.GetKey(KeyCode.LeftShift) || InputManager.TouchInput.vehicleB || InputManager.PcInput.RightClick());
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000069FC File Offset: 0x00004BFC
	public static bool Break()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || Input.GetKey(KeyCode.LeftControl));
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x00006A1B File Offset: 0x00004C1B
	public static bool PlaceObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R1() || InputManager.PcInput.LeftClick() || InputManager.TouchInput.place);
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00006A3C File Offset: 0x00004C3C
	public static bool RemoveObject()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L1() || InputManager.PcInput.RightClick() || InputManager.TouchInput.remove);
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00006A5D File Offset: 0x00004C5D
	public static bool RotateObjectRight()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.R2Down() || Input.GetKeyDown(GameData.controls.rotateItemRight) || InputManager.TouchInput.rotateRight);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00006A88 File Offset: 0x00004C88
	public static bool RotateObjectLeft()
	{
		return !InputManager.MenuIsActive(false) && (InputManager.ConsoleInput.L2Down() || Input.GetKeyDown(GameData.controls.rotateItemLeft) || InputManager.TouchInput.rotateLeft);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00006AB3 File Offset: 0x00004CB3
	public static bool ToggleBuildMode()
	{
		return InputManager.ConsoleInput.MiddlePadDown() || Input.GetKeyDown(GameData.controls.toggleBuildMode) || InputManager.TouchInput.toggleBuildMode;
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00006AD4 File Offset: 0x00004CD4
	public static bool RemoveLastItem()
	{
		return !InputManager.MenuIsActive(false) && Input.GetKeyDown(GameData.controls.removeLastItem);
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00006AEF File Offset: 0x00004CEF
	public static bool Escape()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00006AEF File Offset: 0x00004CEF
	public static bool TogglePauseMenu()
	{
		return InputManager.ConsoleInput.OptionsDown() || Input.GetKeyDown(KeyCode.Escape) || InputManager.TouchInput.escape;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00006B08 File Offset: 0x00004D08
	public static bool Back()
	{
		return InputManager.ConsoleInput.CircleDown() || Input.GetKeyDown(KeyCode.Escape);
	}

	// Token: 0x040006D4 RID: 1748
	private static Vector2 move;

	// Token: 0x040006D5 RID: 1749
	private static Vector2 moveLerp;

	// Token: 0x040006D6 RID: 1750
	private static readonly float moveAccelerationSpeed = 6f;

	// Token: 0x040006D7 RID: 1751
	private static readonly float mouseLookSensitivityMultiplier = 10f;

	// Token: 0x040006D8 RID: 1752
	public static int menuCooldown = 500;

	// Token: 0x040006D9 RID: 1753
	private static Vector2 look;

	// Token: 0x02000109 RID: 265
	public static class PcInput
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x00006B3A File Offset: 0x00004D3A
		public static bool LeftClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse0);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00006B4F File Offset: 0x00004D4F
		public static bool RightClick()
		{
			return Input.mousePresent && Input.GetKey(KeyCode.Mouse1);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00006B64 File Offset: 0x00004D64
		public static Vector2 MouseLook()
		{
			return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		}
	}

	// Token: 0x0200010A RID: 266
	public static class ConsoleInput
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x00006B7F File Offset: 0x00004D7F
		public static bool OptionsDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton9);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00006B8B File Offset: 0x00004D8B
		public static bool XDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton1);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00006B97 File Offset: 0x00004D97
		public static bool CircleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton2);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00006BA3 File Offset: 0x00004DA3
		public static bool SquareDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton0);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00006BAF File Offset: 0x00004DAF
		public static bool TriangleDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton3);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00006BBB File Offset: 0x00004DBB
		public static bool MiddlePadDown()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton13);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00006BC7 File Offset: 0x00004DC7
		public static bool R1()
		{
			return Input.GetKey(KeyCode.JoystickButton5);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00006BD3 File Offset: 0x00004DD3
		public static bool R2()
		{
			return Input.GetKey(KeyCode.JoystickButton7);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00006BDF File Offset: 0x00004DDF
		public static bool R2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton7);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00006BEB File Offset: 0x00004DEB
		public static bool L1()
		{
			return Input.GetKey(KeyCode.JoystickButton4);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00006BF7 File Offset: 0x00004DF7
		public static bool L2()
		{
			return Input.GetKey(KeyCode.JoystickButton6);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00006C03 File Offset: 0x00004E03
		public static bool L2Down()
		{
			return Input.GetKeyDown(KeyCode.JoystickButton6);
		}
	}

	// Token: 0x0200010B RID: 267
	public static class TouchInput
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0001AFDC File Offset: 0x000191DC
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

		// Token: 0x06000599 RID: 1433 RVA: 0x0001B148 File Offset: 0x00019348
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

		// Token: 0x040006DA RID: 1754
		public static bool tapToJump = true;

		// Token: 0x040006DB RID: 1755
		private static Vector2 moveStart = new Vector2(0f, 0f);

		// Token: 0x040006DC RID: 1756
		private static bool setMoveStart = false;

		// Token: 0x040006DD RID: 1757
		private static int moveStartFingerId;

		// Token: 0x040006DE RID: 1758
		public static Vector2 lookStart = new Vector2(0f, 0f);

		// Token: 0x040006DF RID: 1759
		private static bool setLookStart = false;

		// Token: 0x040006E0 RID: 1760
		public static bool jump;

		// Token: 0x040006E1 RID: 1761
		public static bool jumping;

		// Token: 0x040006E2 RID: 1762
		public static bool shift;

		// Token: 0x040006E3 RID: 1763
		public static bool interact;

		// Token: 0x040006E4 RID: 1764
		public static bool toggleRagdoll;

		// Token: 0x040006E5 RID: 1765
		public static bool toggleBuildMode;

		// Token: 0x040006E6 RID: 1766
		public static bool toggleBuildItemMenu;

		// Token: 0x040006E7 RID: 1767
		public static bool toggleSlowMotion;

		// Token: 0x040006E8 RID: 1768
		public static bool escape;

		// Token: 0x040006E9 RID: 1769
		public static bool setSpawn;

		// Token: 0x040006EA RID: 1770
		public static bool place;

		// Token: 0x040006EB RID: 1771
		public static bool remove;

		// Token: 0x040006EC RID: 1772
		public static bool rotateRight;

		// Token: 0x040006ED RID: 1773
		public static bool rotateLeft;

		// Token: 0x040006EE RID: 1774
		public static bool vehicleA;

		// Token: 0x040006EF RID: 1775
		public static bool vehicleB;

		// Token: 0x040006F0 RID: 1776
		private static readonly Rect screenLeft = new Rect(0f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);

		// Token: 0x040006F1 RID: 1777
		private static readonly Rect screenRight = new Rect((float)Screen.width * 0.5f, 0f, (float)Screen.width * 0.5f, (float)Screen.height);
	}
}
