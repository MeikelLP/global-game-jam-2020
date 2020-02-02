using System;
using UnityEngine;
using Random = System.Random;

namespace PhoneScripts
{
    // This script will simply instantiate the Prefab when the game starts.
    public class PhoneSpawner : MonoBehaviour
    {
        public static PhoneSpawner Instance { get; private set; }
        public Phone ActivePhone { get; private set; }

        private readonly Random _random = new Random();

        public GameObject phonePosition;
        public Phone[] phonePrefabs;

        // scripts that need the phone
        public PhoneFlipper phoneFlipper;
        public InventoryScript inventoryScript;

        // will be initialized from bootstrapper at runtime
        public void Initialize()
        {
            if (Instance != null) return;
            if (!phonePosition) throw new NullReferenceException(nameof(phonePosition));
            if (!phoneFlipper) throw new NullReferenceException(nameof(phoneFlipper));
            if (!inventoryScript) throw new NullReferenceException(nameof(inventoryScript));
            if (phonePrefabs.Length == 0)
            {
                throw new InvalidOperationException("At least one phone prefab must be assigned");
            }

            Instance = this;

            Spawn();
        }

#if UNITY_EDITOR
        private void Start()
        {
            Initialize();
        }
#endif

        public void Spawn()
        {
            var phonePrefabIndex = _random.Next(phonePrefabs.Length);
            var phonePrefab = phonePrefabs[phonePrefabIndex];

            // Instantiate at position (0, 0, 0) and zero rotation.
            ActivePhone = Instantiate(phonePrefab, phonePosition.transform.position, Quaternion.identity);
            var activePhoneTransform = DamagePhone(ActivePhone).transform;
            activePhoneTransform.parent = phonePosition.transform;
            ActivePhone.Initialize();

            phoneFlipper.ActivePhoneTransform = activePhoneTransform;
            phoneFlipper.enabled = true;
            inventoryScript.ActivePhoneTransform = activePhoneTransform;
            inventoryScript.enabled = true;
        }

        private Phone DamagePhone(Phone phone)
        {
            foreach (var phonePart in phone.parts)
            {
                if (phonePart.name.Contains("screw"))
                {
                    continue;
                }
                var r = _random.Next(2);
                phonePart.broken = r > 0;
            }

            return phone;
        }
    }
}
