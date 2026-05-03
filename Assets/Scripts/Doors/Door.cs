using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


public class Door : MonoBehaviour {
	public bool open;
	public float smooth = 1.0f;
	float DoorOpenAngle = -90.0f;
    float DoorCloseAngle = 0.0f;
	public AudioSource asource;
	public AudioClip openDoor,closeDoor;
	public bool locked = false;
	public string requiredKey = "";

	public bool canBeLockPicked = true;

	// Use this for initialization
	void Start () {
		asource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (open)
		{
            var target = Quaternion.Euler (0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
	
		}
		else
		{
            var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
	
		}  
	}

	public void OpenDoor(){
		if (locked)
			{
				Debug.Log("Porte verrouillée");
				return;
			}
		open =!open;
		asource.clip = open?openDoor:closeDoor;
		asource.Play ();
	}

	public void ToggleLock(PlayerKeys player)
	{
		if (!player.HasKey(requiredKey))
		{
			Debug.Log("Tu n'as pas la clé");
			return;
		}

		locked = !locked;
		Debug.Log(locked ? "Porte verrouillée" : "Porte déverrouillée");
	}

	public void TrySimpleLockpick(PlayerInventory inventory)
	{
		if (!locked)
		{
			Debug.Log("La porte est déjà déverrouillée.");
			return;
		}

		if (!canBeLockPicked)
		{
			Debug.Log("Cette porte ne peut pas être crochetée.");
			return;
		}

		if (!inventory.UseSimpleLockpick())
		{
			return;
		}

		locked = false;
		Debug.Log("Porte crochetée avec un kit simple.");
	}

	public void TryExplosiveLockpick(PlayerInventory inventory)
	{
		if (!locked)
		{
			Debug.Log("La porte est déjà déverrouillée.");
			return;
		}

		if (!canBeLockPicked)
		{
			Debug.Log("Cette porte ne peut pas être forcée.");
			return;
		}

		if (!inventory.UseExplosiveLockpick())
		{
			return;
		}

		locked = false;
		Debug.Log("Porte forcée avec un kit explosif.");
	}
}
}