using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001C RID: 28
internal class ExplosionsSpriteSheetAnimation : MonoBehaviour
{
	// Token: 0x06000067 RID: 103 RVA: 0x000026D2 File Offset: 0x000008D2
	private void Start()
	{
		this.currentRenderer = base.GetComponent<Renderer>();
		this.InitDefaultVariables();
		this.isInizialised = true;
		this.isVisible = true;
		this.Play();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000947C File Offset: 0x0000767C
	private void InitDefaultVariables()
	{
		this.currentRenderer = base.GetComponent<Renderer>();
		if (this.currentRenderer == null)
		{
			throw new Exception("UvTextureAnimator can't get renderer");
		}
		if (!this.currentRenderer.enabled)
		{
			this.currentRenderer.enabled = true;
		}
		this.allCount = 0;
		this.animationStoped = false;
		this.animationLifeTime = (float)(this.TilesX * this.TilesY) / this.AnimationFPS;
		this.count = this.TilesY * this.TilesX;
		this.index = this.TilesX - 1;
		Vector3 zero = Vector3.zero;
		this.StartFrameOffset -= this.StartFrameOffset / this.count * this.count;
		Vector2 value = new Vector2(1f / (float)this.TilesX, 1f / (float)this.TilesY);
		if (this.currentRenderer != null)
		{
			this.instanceMaterial = this.currentRenderer.material;
			this.instanceMaterial.SetTextureScale("_MainTex", value);
			this.instanceMaterial.SetTextureOffset("_MainTex", zero);
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000026FA File Offset: 0x000008FA
	private void Play()
	{
		if (this.isCorutineStarted)
		{
			return;
		}
		if (this.StartDelay > 0.0001f)
		{
			base.Invoke("PlayDelay", this.StartDelay);
		}
		else
		{
			base.StartCoroutine(this.UpdateCorutine());
		}
		this.isCorutineStarted = true;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00002739 File Offset: 0x00000939
	private void PlayDelay()
	{
		base.StartCoroutine(this.UpdateCorutine());
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00002748 File Offset: 0x00000948
	private void OnEnable()
	{
		if (!this.isInizialised)
		{
			return;
		}
		this.InitDefaultVariables();
		this.isVisible = true;
		this.Play();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00002766 File Offset: 0x00000966
	private void OnDisable()
	{
		this.isCorutineStarted = false;
		this.isVisible = false;
		base.StopAllCoroutines();
		base.CancelInvoke("PlayDelay");
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00002787 File Offset: 0x00000987
	private IEnumerator UpdateCorutine()
	{
		this.animationStartTime = Time.time;
		while (this.isVisible && (this.IsLoop || !this.animationStoped))
		{
			this.UpdateFrame();
			if (!this.IsLoop && this.animationStoped)
			{
				break;
			}
			float value = (Time.time - this.animationStartTime) / this.animationLifeTime;
			float num = this.FrameOverTime.Evaluate(Mathf.Clamp01(value));
			yield return new WaitForSeconds(1f / (this.AnimationFPS * num));
		}
		this.isCorutineStarted = false;
		this.currentRenderer.enabled = false;
		yield break;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000095A0 File Offset: 0x000077A0
	private void UpdateFrame()
	{
		this.allCount++;
		this.index++;
		if (this.index >= this.count)
		{
			this.index = 0;
		}
		if (this.count == this.allCount)
		{
			this.animationStartTime = Time.time;
			this.allCount = 0;
			this.animationStoped = true;
		}
		Vector2 value = new Vector2((float)this.index / (float)this.TilesX - (float)(this.index / this.TilesX), 1f - (float)(this.index / this.TilesX) / (float)this.TilesY);
		if (this.currentRenderer != null)
		{
			this.instanceMaterial.SetTextureOffset("_MainTex", value);
		}
		if (this.IsInterpolateFrames)
		{
			this.currentInterpolatedTime = 0f;
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00009678 File Offset: 0x00007878
	private void Update()
	{
		if (!this.IsInterpolateFrames)
		{
			return;
		}
		this.currentInterpolatedTime += Time.deltaTime;
		int num = this.index + 1;
		if (this.allCount == 0)
		{
			num = this.index;
		}
		Vector4 value = new Vector4(1f / (float)this.TilesX, 1f / (float)this.TilesY, (float)num / (float)this.TilesX - (float)(num / this.TilesX), 1f - (float)(num / this.TilesX) / (float)this.TilesY);
		if (this.currentRenderer != null)
		{
			this.instanceMaterial.SetVector("_MainTex_NextFrame", value);
			float value2 = (Time.time - this.animationStartTime) / this.animationLifeTime;
			float num2 = this.FrameOverTime.Evaluate(Mathf.Clamp01(value2));
			this.instanceMaterial.SetFloat("InterpolationValue", Mathf.Clamp01(this.currentInterpolatedTime * this.AnimationFPS * num2));
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00002796 File Offset: 0x00000996
	private void OnDestroy()
	{
		if (this.instanceMaterial != null)
		{
			UnityEngine.Object.Destroy(this.instanceMaterial);
			this.instanceMaterial = null;
		}
	}

	// Token: 0x04000093 RID: 147
	public int TilesX = 4;

	// Token: 0x04000094 RID: 148
	public int TilesY = 4;

	// Token: 0x04000095 RID: 149
	public float AnimationFPS = 30f;

	// Token: 0x04000096 RID: 150
	public bool IsInterpolateFrames;

	// Token: 0x04000097 RID: 151
	public int StartFrameOffset;

	// Token: 0x04000098 RID: 152
	public bool IsLoop = true;

	// Token: 0x04000099 RID: 153
	public float StartDelay;

	// Token: 0x0400009A RID: 154
	public AnimationCurve FrameOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x0400009B RID: 155
	private bool isInizialised;

	// Token: 0x0400009C RID: 156
	private int index;

	// Token: 0x0400009D RID: 157
	private int count;

	// Token: 0x0400009E RID: 158
	private int allCount;

	// Token: 0x0400009F RID: 159
	private float animationLifeTime;

	// Token: 0x040000A0 RID: 160
	private bool isVisible;

	// Token: 0x040000A1 RID: 161
	private bool isCorutineStarted;

	// Token: 0x040000A2 RID: 162
	private Renderer currentRenderer;

	// Token: 0x040000A3 RID: 163
	private Material instanceMaterial;

	// Token: 0x040000A4 RID: 164
	private float currentInterpolatedTime;

	// Token: 0x040000A5 RID: 165
	private float animationStartTime;

	// Token: 0x040000A6 RID: 166
	private bool animationStoped;
}
