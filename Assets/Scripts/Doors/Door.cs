using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]
	public class Door : MonoBehaviour
	{
		public bool open;
		public float smooth = 1.0f;

		float DoorOpenAngle = -90.0f;
		float DoorCloseAngle = 0.0f;

		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		public bool locked = false;
		public string requiredKey = "";

		public bool canBeLockPicked = true;

		[Header("Lockpick Settings")]
		public float simpleLockpickDuration = 5f;

		[Range(0f, 1f)]
		public float simpleLockpickSuccessChance = 0.7f;

		void Start()
		{
			asource = GetComponent<AudioSource>();
		}

		void Update()
		{
			if (open)
			{
				var target = Quaternion.Euler(0, DoorOpenAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
			}
			else
			{
				var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
			}
		}

		public void OpenDoor()
		{
			if (locked)
			{
				Debug.Log("Porte verrouillée");
				return;
			}

			open = !open;
			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
		}

		public void ToggleLock(PlayerKeys player)
		{
			if (!string.IsNullOrEmpty(requiredKey) && !player.HasKey(requiredKey))
			{
				Debug.Log("Tu n'as pas la clé");
				return;
			}

			locked = !locked;
			Debug.Log(locked ? "Porte verrouillée" : "Porte déverrouillée");
		}

		public bool ResolveSimpleLockpick(PlayerInventory inventory)
		{
			if (!locked)
			{
				Debug.Log("La porte est déjà déverrouillée.");
				return false;
			}

			if (!canBeLockPicked)
			{
				Debug.Log("Cette porte ne peut pas être crochetée.");
				return false;
			}

			if (!inventory.UseSimpleLockpick())
			{
				return false;
			}

			bool success = Random.value <= simpleLockpickSuccessChance;

			if (success)
			{
				locked = false;
				Debug.Log("Crochetage réussi.");
			}
			else
			{
				Debug.Log("Crochetage échoué.");
			}

			return success;
		}

		public void TryExplosiveLockpick(PlayerInventory inventory)
		{
			if (!locked)
			{
				Debug.Log("Déjà ouverte.");
				return;
			}

			if (!inventory.UseExplosiveLockpick())
			{
				return;
			}

			locked = false;
			Debug.Log("Porte forcée !");
		}
	}
}