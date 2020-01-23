using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class DontDestoryOnLoad : MonoBehaviour
{
	// Token: 0x06000440 RID: 1088 RVA: 0x00005890 File Offset: 0x00003A90
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
