using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000100 RID: 256
public static class FileManager
{
	// Token: 0x0600052C RID: 1324 RVA: 0x000061A8 File Offset: 0x000043A8
	public static void CreateWorldDirectory()
	{
		if (!Directory.Exists(FileManager.worldPath))
		{
			Directory.CreateDirectory(FileManager.worldPath);
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x000061C1 File Offset: 0x000043C1
	public static string GetPotentialWorldPath(string worldName)
	{
		return FileManager.worldPath + FileManager.Sanitize(worldName) + "." + FileManager.fileType;
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0001B730 File Offset: 0x00019930
	public static void Save(string name, object data, bool nameIsLocation)
	{
		string path;
		if (nameIsLocation)
		{
			path = name;
		}
		else
		{
			if (FileManager.TextIsNotValid(name))
			{
				return;
			}
			name = FileManager.Sanitize(name);
			path = FileManager.worldPath + name + "." + FileManager.fileType;
		}
		FileManager.CreateWorldDirectory();
		File.WriteAllText(path, JsonUtility.ToJson(data));
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001B77C File Offset: 0x0001997C
	public static T Load<T>(string fileLocation) where T : class
	{
		T result;
		if (FileManager.TextIsNotValid(fileLocation))
		{
			result = default(T);
			return result;
		}
		if (!File.Exists(fileLocation))
		{
			result = default(T);
			return result;
		}
		try
		{
			result = JsonUtility.FromJson<T>(File.ReadAllText(fileLocation));
		}
		catch
		{
			Debug.Log("No world data found!");
			result = default(T);
		}
		return result;
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x000061DD File Offset: 0x000043DD
	public static bool Delete(string fileLocation)
	{
		if (FileManager.TextIsNotValid(fileLocation))
		{
			return false;
		}
		if (!File.Exists(fileLocation))
		{
			return false;
		}
		File.Delete(fileLocation);
		return true;
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001B7E4 File Offset: 0x000199E4
	public static List<string> GetWorldNames()
	{
		List<string> worldFileDirectories = FileManager.GetWorldFileDirectories();
		List<string> list = new List<string>();
		for (int i = 0; i < worldFileDirectories.Count; i++)
		{
			list.Add(FileManager.GetWorldName(worldFileDirectories[i]));
		}
		return list;
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0001B824 File Offset: 0x00019A24
	public static string GetWorldName(string fileLocation)
	{
		string result;
		try
		{
			result = JsonUtility.FromJson<WorldData.Data>(File.ReadAllText(fileLocation)).worldName;
		}
		catch
		{
			result = "[ERROR: File Not Readable]";
		}
		return result;
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0001B860 File Offset: 0x00019A60
	public static List<string> GetWorldFileDirectories()
	{
		List<string> list = new List<string>();
		FileInfo[] files = new DirectoryInfo(FileManager.worldPath).GetFiles("*." + FileManager.fileType);
		for (int i = 0; i < files.Length; i++)
		{
			list.Add(files[i].FullName);
		}
		return list;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001B8B0 File Offset: 0x00019AB0
	public static bool RenameFile(string fileLocation, string newName)
	{
		bool result;
		try
		{
			WorldData.Data data = JsonUtility.FromJson<WorldData.Data>(File.ReadAllText(fileLocation));
			newName = FileManager.Sanitize(newName);
			data.worldName = newName;
			File.WriteAllText(fileLocation, JsonUtility.ToJson(data));
			result = true;
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x000061FA File Offset: 0x000043FA
	public static string Sanitize(string input)
	{
		return Regex.Replace(input, "[^a-zA-Z0-9.()_' ]", string.Empty).Trim();
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00006211 File Offset: 0x00004411
	public static bool TextIsNotValid(string text)
	{
		return text == null || text == "" || text == " ";
	}

	// Token: 0x040006A2 RID: 1698
	public static readonly string worldPath = Application.persistentDataPath + "/Worlds/";

	// Token: 0x040006A3 RID: 1699
	public static readonly string fileType = "fwr";
}
