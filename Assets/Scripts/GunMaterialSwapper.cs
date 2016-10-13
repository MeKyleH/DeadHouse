using UnityEngine;
using System.Collections;

public class GunMaterialSwapper : MonoBehaviour
{
	public Material localMaterial;
	public Material notLocalMaterial;

	Renderer gunRenderer;

	void Awake()
	{
		gunRenderer = GetComponent<Renderer>();
		SwitchMaterial(false);
	}

	public void SwitchMaterial(bool isLocalPlayer)
	{
		if (isLocalPlayer) {
			gunRenderer.material = localMaterial;
		} else {
			gunRenderer.material = notLocalMaterial;
		}
	}
}