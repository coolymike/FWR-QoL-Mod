using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BD RID: 189
public class UIVersion : MonoBehaviour
{
	// Token: 0x060003BD RID: 957
	private void Start()
	{
		string text = "ModVersion:1.4.0\nRotXPosKey:C\nRotXNegKey:V\nRotZPosKey:B\nRotZNegKey:N\nRotationAmountKey:I\nDisableFlyKey:P\nFlyEnabledInPlayMode:true\nGridToggleKey:O\nCustomColor1:255,255,255\nCustomColor2:255,255,255\nSkipIntroMovie:true\nDestructibleObjectsDontHide:true\nNukeEnabled:false\nXPosBuildToggle:G\nYPosBuildToggle:H\nZPosBuildToggle:J\nCustomGravity:-14.81\nLasersOnPermanently:false\nCinematicCameraToggle:U\nRunningEnabled:true\nDestructibleObjectsCollide:false\nUnlockTutorial:true";
		string path = Application.persistentDataPath + "\\mod_settings.txt";
		if (!File.Exists(path))
		{
			using (FileStream fileStream = File.Create(path))
			{
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				fileStream.Write(bytes, 0, text.Length);
			}
		}
		string[] array = File.ReadAllText(path).Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			if (array2[0] == "RotXPosKey")
			{
				InputManager.RotXPosKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "RotXNegKey")
			{
				InputManager.RotXNegKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "RotZPosKey")
			{
				InputManager.RotZPosKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "RotZNegKey")
			{
				InputManager.RotZNegKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "RotationAmountKey")
			{
				InputManager.SmallRotKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "DisableFlyKey")
			{
				InputManager.DisableFlyKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "FlyEnabledInPlayMode")
			{
				if (array2[1] == "true")
				{
					InputManager.PlayModeFlyDisabled = false;
				}
				else
				{
					InputManager.PlayModeFlyDisabled = true;
				}
			}
			else if (array2[0] == "GridToggleKey")
			{
				InputManager.GridToggleKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
			}
			else if (array2[0] == "DestructibleObjectsDontHide")
			{
				if (array2[1] == "true")
				{
					DestructibleObject.LastsInfinite = true;
				}
				else
				{
					DestructibleObject.LastsInfinite = false;
				}
			}
			else if (array2[0] == "NukeEnabled")
			{
				if (array2[1] == "true")
				{
					UIVersion.NukeEnabled = true;
				}
				else
				{
					UIVersion.NukeEnabled = false;
				}
			}
			else
			{
				if (array2[0] == "ModVersion" && array2[1] != "1.4.0")
				{
					File.Delete(path);
					using (FileStream fileStream2 = File.Create(path))
					{
						byte[] bytes2 = Encoding.ASCII.GetBytes(text);
						fileStream2.Write(bytes2, 0, text.Length);
					}
				}
				if (array2[0] == "XPosBuildToggle")
				{
					InputManager.XPosToggleKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
				}
				else if (array2[0] == "YPosBuildToggle")
				{
					InputManager.YPosToggleKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
				}
				else if (array2[0] == "ZPosBuildToggle")
				{
					InputManager.ZPosToggleKey = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
				}
				else if (array2[0] == "CustomGravity")
				{
					WorldData.customGravity = float.Parse(array2[1], CultureInfo.InvariantCulture.NumberFormat);
				}
				else if (array2[0] == "CinematicCameraToggle")
				{
					InputManager.switchCamera = (KeyCode)Enum.Parse(typeof(KeyCode), array2[1]);
				}
				else if (array2[0] == "LasersOnPermanently")
				{
					if (array2[1] == "true")
					{
						CanonShoot.LaserPermanentOn = true;
					}
					else
					{
						CanonShoot.LaserPermanentOn = false;
					}
				}
				else if (array2[0] == "RunningEnabled")
				{
					if (array2[1] == "true")
					{
						InputManager.RunningEnabled = true;
					}
					else
					{
						InputManager.RunningEnabled = false;
					}
				}
				else if (array2[0] == "DestructibleObjectsCollide")
				{
					if (array2[1] == "true")
					{
						DestructibleObject.destructibleObjectsCollide = true;
					}
					else
					{
						DestructibleObject.destructibleObjectsCollide = false;
					}
				}
				else if (array2[0] == "UnlockTutorial")
				{
					if (array2[1] == "true")
					{
						Tutorial.TutorialUnlocked = true;
					}
					else
					{
						Tutorial.TutorialUnlocked = false;
					}
				}
			}
		}
		this.versionText.text = "Version " + Application.version + ", Whip's QoL 1.4.0";
	}

	// Token: 0x04000520 RID: 1312
	public Text versionText;

	// Token: 0x04000521 RID: 1313
	public static bool NukeEnabled;
}
