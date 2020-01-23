using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000DA RID: 218
public class LoadSceneOnStart : MonoBehaviour
{
	// Token: 0x0600045C RID: 1116 RVA: 0x000059AA File Offset: 0x00003BAA
	private void Start()
	{
		SceneManager.LoadSceneAsync(this.sceneToLoad);
	}

	// Token: 0x040005C5 RID: 1477
	public string sceneToLoad;
}
