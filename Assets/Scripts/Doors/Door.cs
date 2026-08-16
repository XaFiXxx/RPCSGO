using UnityEngine;

namespace DoorScript
{
    [RequireComponent(typeof(AudioSource))]
    public class Door : MonoBehaviour
    {
        [Header("Door")]
        public bool open;
        public float smooth = 1.0f;

        private float DoorOpenAngle = -90.0f;
        private float DoorCloseAngle = 0.0f;

        [Header("Audio")]
        public AudioSource asource;
        public AudioClip openDoor;
        public AudioClip closeDoor;

        [Header("Lock")]
        public bool locked = false;
        public string requiredKey = "";
        public bool canBeLockPicked = true;

        [Header("Lockpick Settings")]
        public float simpleLockpickDuration = 5f;

        [Range(0f, 1f)]
        public float simpleLockpickSuccessChance = 0.7f;

        public float explosiveLockpickDuration = 2f;

        [Range(0f, 1f)]
        public float explosiveLockpickSuccessChance = 0.9f;

        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (open)
            {
                Quaternion target = Quaternion.Euler(0, DoorOpenAngle, 0);

                transform.localRotation = Quaternion.Slerp(
                    transform.localRotation,
                    target,
                    Time.deltaTime * 5 * smooth
                );
            }
            else
            {
                Quaternion target = Quaternion.Euler(0, DoorCloseAngle, 0);

                transform.localRotation = Quaternion.Slerp(
                    transform.localRotation,
                    target,
                    Time.deltaTime * 5 * smooth
                );
            }
        }

        public void OpenDoor()
        {
            if (locked)
            {
                Debug.Log("Porte verrouillée.");
                return;
            }

            open = !open;

            if (asource != null)
            {
                asource.clip = open ? openDoor : closeDoor;

                if (asource.clip != null)
                    asource.Play();
            }
        }

        public void ToggleLock(PlayerKeys player)
        {
            if (!string.IsNullOrEmpty(requiredKey) &&
                !player.HasKey(requiredKey))
            {
                Debug.Log("Tu n'as pas la clé.");
                return;
            }

            locked = !locked;

            Debug.Log(
                locked
                    ? "Porte verrouillée."
                    : "Porte déverrouillée."
            );
        }

        public bool CanStartSimpleLockpick(PlayerInventory inventory)
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

            if (inventory.simpleLockpicks <= 0)
            {
                Debug.Log("Pas de lockpick simple.");
                return false;
            }

            return true;
        }

        public bool ResolveSimpleLockpick(PlayerInventory inventory)
        {
            if (!CanStartSimpleLockpick(inventory))
                return false;

            if (!inventory.UseSimpleLockpick())
                return false;

            bool success =
                Random.value <= simpleLockpickSuccessChance;

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

        public bool CanStartExplosiveLockpick(PlayerInventory inventory)
        {
            if (!locked)
            {
                Debug.Log("La porte est déjà déverrouillée.");
                return false;
            }

            if (!canBeLockPicked)
            {
                Debug.Log("Cette porte ne peut pas être forcée.");
                return false;
            }

            if (inventory.explosiveLockpicks <= 0)
            {
                Debug.Log("Pas de lockpick explosif.");
                return false;
            }

            return true;
        }

        public bool ResolveExplosiveLockpick(PlayerInventory inventory)
        {
            if (!CanStartExplosiveLockpick(inventory))
                return false;

            if (!inventory.UseExplosiveLockpick())
                return false;

            bool success =
                Random.value <= explosiveLockpickSuccessChance;

            if (success)
            {
                locked = false;

                Debug.Log("Porte forcée !");
            }
            else
            {
                Debug.Log("Forçage explosif échoué.");
            }

            return success;
        }
    }
}