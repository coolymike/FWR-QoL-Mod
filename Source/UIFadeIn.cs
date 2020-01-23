using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000AE RID: 174
public class UIFadeIn : MonoBehaviour
{
	// Token: 0x0600037F RID: 895 RVA: 0x00015878 File Offset: 0x00013A78
	private void Update()
	{
		if (this.fadingImage != null && this.fadingImage.color.a > 0f)
		{
			this.fadingImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(this.fadingImage.color.a, 0f, this.fadeSpeed * Time.deltaTime));
		}
	}

	// Token: 0x040004D1 RID: 1233
	public Image fadingImage;

	// Token: 0x040004D2 RID: 1234
	public float fadeSpeed = 0.5f;
}
