using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace PhoneScripts
{
    public class PhoneSpawner : MonoBehaviour
    {
        public static PhoneSpawner Instance { get; private set; }

        private readonly Random _random = new Random();
    
        public GameObject phonePosition;
        public Phone[] phonePrefabs;

        // scripts that need the phone
        public PhoneFlipper phoneFlipper;
        public InventoryScript inventoryScript;

        // This script will simply instantiate the Prefab when the game starts.
        private void Start()
        {
            if(!phonePosition) throw new NullReferenceException(nameof(phonePosition));
            if (phonePrefabs.Length == 0)
            {
                throw new InvalidOperationException("At least one phone prefab must be assigned");
            }
            
            Spawn();
        }
    
        public void Spawn()
        {
            var phonePrefabIndex = _random.Next(phonePrefabs.Length-1);
            var phonePrefab = phonePrefabs[phonePrefabIndex];

            // Instantiate at position (0, 0, 0) and zero rotation.
            var activePhone = Instantiate(phonePrefab, phonePosition.transform.position, Quaternion.identity);
            var activePhoneTransform = DamagePhone(activePhone).transform;
            activePhoneTransform.parent = phonePosition.transform;
            
            phoneFlipper.ActivePhoneTransform = activePhoneTransform;
            phoneFlipper.enabled = true;
            inventoryScript.ActivePhoneTransform = activePhoneTransform;
            inventoryScript.enabled = true;
        }

        private Phone DamagePhone(Phone phone)
        {
            foreach (var phonePart in phone.parts)
            {
                var r = _random.Next(1);
                phonePart.broken = r > 0;
            }

            return phone;
        }
    }
}