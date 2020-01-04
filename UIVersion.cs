using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class UIVersion : MonoBehaviour
{
	// Token: 0x060003AA RID: 938
	private void Start()
	{
		string defaultSettings = "RotXPosKey:C\nRotXNegKey:V\nRotZPosKey:B\nRotZNegKey:N\nRotationAmountKey:I\nDisableFlyKey:P\nFlyEnabledInPlayMode:true";
		string path = Application.persistentDataPath + "\\mod_settings.txt";
		if (!File.Exists(path))
		{
			using (FileStream fileStream = File.Create(path))
			{
				byte[] bytes = Encoding.ASCII.GetBytes(defaultSettings);
				fileStream.Write(bytes, 0, defaultSettings.Length);
			}
		}
		string[] array = File.ReadAllText(path).Split(new char[]
		{
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			string[] setting = array[i].Split(new char[]
			{
				':'
			});
			if (setting[0] == "RotXPosKey")
			{
				InputManager.RotXPosKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "RotXNegKey")
			{
				InputManager.RotXNegKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "RotZPosKey")
			{
				InputManager.RotZPosKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "RotZNegKey")
			{
				InputManager.RotZNegKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "RotationAmountKey")
			{
				InputManager.SmallRotKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "DisableFlyKey")
			{
				InputManager.DisableFlyKey = (KeyCode)Enum.Parse(typeof(KeyCode), setting[1]);
			}
			else if (setting[0] == "FlyEnabledInPlayMode")
			{
				if (setting[1] == "true")
				{
					InputManager.PlayModeFlyDisabled = false;
				}
				else
				{
					InputManager.PlayModeFlyDisabled = true;
				}
			}
		}
		this.versionText.text = "Version " + Application.version + ", Whip's QoL 1.0.0";
	}

	// Token: 0x04000514 RID: 1300
	public Text versionText;
}
