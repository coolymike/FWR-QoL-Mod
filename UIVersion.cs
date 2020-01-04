using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class UIVersion : MonoBehaviour
{
	// Token: 0x060003AB RID: 939
	private void Start()
	{
		string text = "RotXPosKey:C\nRotXNegKey:V\nRotZPosKey:B\nRotZNegKey:N\nRotationAmountKey:I\nDisableFlyKey:P\nFlyEnabledInPlayMode:true\nGridToggleKey:O\nCustomColor1:255,255,255\nCustomColor2:255,255,255";
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
			else if (array2[0] == "CustomColor1")
			{
				string[] Colors1Str = array2[1].Split(new char[]
				{
					','
				});
				int[] Colors1Int = new int[Colors1Str.Length];
				for (int count = 0; count < Colors1Str.Length; count++)
				{
					Colors1Int[count] = int.Parse(Colors1Str[count]);
				}
				UIVersion.Color1 = new Vector3((float)(Colors1Int[0] / 255), (float)(Colors1Int[1] / 255), (float)(Colors1Int[2] / 255));
			}
			else if (array2[0] == "CustomColor2")
			{
				string[] Colors2Str = array2[1].Split(new char[]
				{
					','
				});
				int[] Colors2Int = new int[Colors2Str.Length];
				for (int count2 = 0; count2 < Colors2Str.Length; count2++)
				{
					Colors2Int[count2] = int.Parse(Colors2Str[count2]);
				}
				UIVersion.Color2 = new Vector3((float)(Colors2Int[0] / 255), (float)(Colors2Int[1] / 255), (float)(Colors2Int[2] / 255));
			}
		}
		this.versionText.text = "Version " + Application.version + ", Whip's QoL 1.1.0";
	}

	// Token: 0x04000514 RID: 1300
	public Text versionText;

	// Token: 0x0400097F RID: 2431
	public static Vector3 Color1 = new Vector3(0f, 0f, 0f);

	// Token: 0x04000980 RID: 2432
	public static Vector3 Color2 = new Vector3(0f, 0f, 0f);
}
