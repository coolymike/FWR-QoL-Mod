using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000E5 RID: 229
public class SceneReset : MonoBehaviour
{
	// Token: 0x06000497 RID: 1175 RVA: 0x00018F64 File Offset: 0x00017164
	private void Update()
	{
		if (Input.GetKeyDown(this.resetKey))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}
	}

	// Token: 0x04000613 RID: 1555
	public KeyCode resetKey = KeyCode.R;
}
