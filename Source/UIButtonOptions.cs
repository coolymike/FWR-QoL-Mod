using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000080 RID: 128
public class UIButtonOptions : MonoBehaviour
{
	// Token: 0x0600028B RID: 651 RVA: 0x0000434E File Offset: 0x0000254E
	private void Start()
	{
		this.UpdateOptions();
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00004356 File Offset: 0x00002556
	public void UpdateOptions()
	{
		if (this.options.Count > 0)
		{
			this.text.text = this.options[this.selectedOption];
			return;
		}
		this.text.text = "No Data";
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00004393 File Offset: 0x00002593
	public void NextOption()
	{
		this.selectedOption++;
		if (this.selectedOption > this.options.Count - 1)
		{
			this.selectedOption = 0;
		}
		this.UpdateOptions();
	}

	// Token: 0x040003DC RID: 988
	public Text text;

	// Token: 0x040003DD RID: 989
	public List<string> options = new List<string>();

	// Token: 0x040003DE RID: 990
	public int selectedOption;
}
