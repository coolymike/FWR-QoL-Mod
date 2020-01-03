using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class UIVersion : MonoBehaviour
{
	// Token: 0x060003AA RID: 938
	private void Start()
	{
		this.versionText.text = "Version " + Application.version + ", Whip's QoL 1.0.0";
	}

	// Token: 0x04000514 RID: 1300
	public Text versionText;
}
