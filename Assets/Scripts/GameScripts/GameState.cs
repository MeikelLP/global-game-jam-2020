using System;
using PhoneScripts;
using UnityEngine;

namespace GameScripts
{
    public class GameState : MonoBehaviour
    {
        public PhoneSpawner phoneSpawner;

        public void Start()
        {
            if (!phoneSpawner) throw new ArgumentNullException(nameof(phoneSpawner));
        }

        public void CheckPhone(Phone phone)
        {
            if (!phone.IsRepaired())
            {
                return;
            }
            Debug.Log("The phone is repaired, spawning new phone");
            
            // despawn old phone
            // TODO award money
            // TODO increase timer
            // TODO show message about finished phone
            Destroy(phone.gameObject);
            
            phoneSpawner.Spawn();
        }
        
        public void CheckTime()
        {
            // TODO check time in fixed interval
        }
    }
}