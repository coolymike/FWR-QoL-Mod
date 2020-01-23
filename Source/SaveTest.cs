using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class SaveTest : MonoBehaviour
{
	// Token: 0x0600057F RID: 1407 RVA: 0x0001BD1C File Offset: 0x00019F1C
	private void Awake()
	{
		for (int i = 0; i < 10000; i++)
		{
			this.dummyDataClass.dummyDataClass.Add(new SaveTest.DummyData());
		}
		FileManager.RenameFile("New Rename", "New rename");
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001BD60 File Offset: 0x00019F60
	private void Start()
	{
		foreach (string message in FileManager.GetWorldNames())
		{
			Debug.Log(message);
		}
	}

	// Token: 0x040006EA RID: 1770
	[SerializeField]
	public SaveTest.AllBuildData dummyDataClass = new SaveTest.AllBuildData();

	// Token: 0x0200010A RID: 266
	[Serializable]
	public class AllBuildData
	{
		// Token: 0x040006EB RID: 1771
		public List<SaveTest.DummyData> dummyDataClass = new List<SaveTest.DummyData>();
	}

	// Token: 0x0200010B RID: 267
	[Serializable]
	public class DummyData
	{
		// Token: 0x040006EC RID: 1772
		public string stringData = "Hello World!";

		// Token: 0x040006ED RID: 1773
		public float floatData = UnityEngine.Random.Range(0f, 1f);

		// Token: 0x040006EE RID: 1774
		public float secondFloatData = 2f;

		// Token: 0x040006EF RID: 1775
		public int intData = UnityEngine.Random.Range(100000, 999999);
	}
}
