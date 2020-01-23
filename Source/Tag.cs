using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class Tag : MonoBehaviour
{
	// Token: 0x06000687 RID: 1671 RVA: 0x0001D534 File Offset: 0x0001B734
	public static Tag GetTag(Transform transform, bool checkRoot = true, bool checkChildren = false)
	{
		Tag tag;
		if (checkRoot)
		{
			if (checkChildren)
			{
				tag = transform.root.GetComponentInChildren<Tag>();
			}
			else
			{
				tag = transform.root.GetComponent<Tag>();
			}
		}
		else if (checkChildren)
		{
			tag = transform.GetComponentInChildren<Tag>();
		}
		else
		{
			tag = transform.GetComponent<Tag>();
		}
		if (tag == null)
		{
			return null;
		}
		return tag;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0001D584 File Offset: 0x0001B784
	public static bool IsBuildItem(Transform transform)
	{
		Tag tag = Tag.GetTag(transform, true, false);
		return !(tag == null) && tag.isBuildItem;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0001D5AC File Offset: 0x0001B7AC
	public static bool Compare(Transform transform, Tag.Tags tag)
	{
		Tag tag2 = Tag.GetTag(transform, true, false);
		return !(tag2 == null) && tag2.tag == tag;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0001D5DC File Offset: 0x0001B7DC
	public static bool Compare(Transform transform, Tag.Tags[] tag)
	{
		Tag tag2 = Tag.GetTag(transform, true, false);
		if (tag2 == null)
		{
			return false;
		}
		for (int i = 0; i < tag.Length; i++)
		{
			if (tag2.tag == tag[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0001D61C File Offset: 0x0001B81C
	public static bool CollidesWithCamera(Transform transform, bool checkRoot = true, bool checkChildren = false)
	{
		Tag tag;
		if (checkRoot)
		{
			if (checkChildren)
			{
				tag = transform.root.GetComponentInChildren<Tag>();
			}
			else
			{
				tag = transform.root.GetComponent<Tag>();
			}
		}
		else if (checkChildren)
		{
			tag = transform.GetComponentInChildren<Tag>();
		}
		else
		{
			tag = transform.GetComponent<Tag>();
		}
		return tag == null || tag.collideWithCamera;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0001D670 File Offset: 0x0001B870
	public static void Transfer(GameObject from, GameObject to, Tag.Tags tagOnNull = Tag.Tags.None, bool setAsBuildItem = false)
	{
		Tag component = from.GetComponent<Tag>();
		Tag tag = to.AddComponent<Tag>();
		if (tag == null)
		{
			return;
		}
		tag.isBuildItem = setAsBuildItem;
		if (component != null)
		{
			tag.tag = ((component.tag == Tag.Tags.None) ? tagOnNull : component.tag);
			tag.collideWithCamera = component.collideWithCamera;
			return;
		}
		tag.tag = tagOnNull;
	}

	// Token: 0x0400077A RID: 1914
	public Tag.Tags tag;

	// Token: 0x0400077B RID: 1915
	public bool isBuildItem;

	// Token: 0x0400077C RID: 1916
	public bool collideWithCamera = true;

	// Token: 0x0200012D RID: 301
	public enum Tags
	{
		// Token: 0x0400077E RID: 1918
		None,
		// Token: 0x0400077F RID: 1919
		Player,
		// Token: 0x04000780 RID: 1920
		Ragdoll,
		// Token: 0x04000781 RID: 1921
		Vehicle,
		// Token: 0x04000782 RID: 1922
		Creature,
		// Token: 0x04000783 RID: 1923
		Domino
	}
}
