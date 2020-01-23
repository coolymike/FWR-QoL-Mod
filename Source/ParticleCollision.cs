using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class ParticleCollision : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00002274 File Offset: 0x00000474
	private void Start()
	{
		this.m_ParticleSystem = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000084B0 File Offset: 0x000066B0
	private void OnParticleCollision(GameObject other)
	{
		int collisionEvents = this.m_ParticleSystem.GetCollisionEvents(other, this.m_CollisionEvents);
		for (int i = 0; i < collisionEvents; i++)
		{
			ExtinguishableFire component = this.m_CollisionEvents[i].colliderComponent.GetComponent<ExtinguishableFire>();
			if (component != null)
			{
				component.Extinguish();
			}
		}
	}

	// Token: 0x04000026 RID: 38
	private List<ParticleCollisionEvent> m_CollisionEvents = new List<ParticleCollisionEvent>();

	// Token: 0x04000027 RID: 39
	private ParticleSystem m_ParticleSystem;
}
