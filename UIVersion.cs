using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BB RID: 187
public class UIVersion : MonoBehaviour
{
	// Token: 0x060003B1 RID: 945 RVA: 0x00016494 File Offset: 0x00014694
	private void Start()
	{
		string text = "RotXPosKey:C\nRotXNegKey:V\nRotZPosKey:B\nRotZNegKey:N\nRotationAmountKey:I\nDisableFlyKey:P\nFlyEnabledInPlayMode:true\nGridToggleKey:O\nCustomColor1:255,255,255\nCustomColor2:255,255,255\nSkipIntroMovie:true\nDestructibleObjectsDontHide:true\nNukeEnabled:false";
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
		}
		this.versionText.text = "Version " + Application.version + ", Whip's QoL 1.1.0";
	}

	// Token: 0x04000517 RID: 1303
	public Text versionText;

	// Token: 0x04000518 RID: 1304
	public static bool NukeEnabled;
}
