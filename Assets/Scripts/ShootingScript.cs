using UnityEngine;
using UnityEngine.Networking;

public class ShootingScript : NetworkBehaviour
{
	public ParticleSystem _muzzleFlash;
	public AudioSource _gunAudio;
	public GameObject _impactPrefab;
	public Transform cameraTransform;

	ParticleSystem _impactEffect;


	void Start()
	{
		_impactEffect = Instantiate(_impactPrefab).GetComponent<ParticleSystem>();
	}

	void Update()
	{
		//suicide
		if (Input.GetButtonDown("Fire2"))
			CmdHitPlayer(gameObject);

		if (Input.GetButtonDown("Fire1"))
		{
			_muzzleFlash.Stop();
			_muzzleFlash.Play();
			_gunAudio.Stop();
			_gunAudio.Play();

			RaycastHit hit;
			Vector3 rayPos = cameraTransform.position + (1f * cameraTransform.forward);

			if (Physics.Raycast(rayPos, cameraTransform.forward, out hit, 50f))
			{
				_impactEffect.transform.position = hit.point;
				_impactEffect.Stop();
				_impactEffect.Play();

				if (hit.transform.tag == "Player")
				{
					CmdHitPlayer(hit.transform.gameObject);
				}
			}
		}
	}

	[Command]
	void CmdHitPlayer(GameObject hit)
	{
		hit.GetComponent<NetworkedPlayerScript>().RpcResolveHit();
	}
}