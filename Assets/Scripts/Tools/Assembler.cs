using System;
using GameScripts;
using UnityEngine;

namespace Tools
{
    public class Assembler : Tool
    {
        [SerializeField] private InventoryScript inventory;
        [SerializeField] private GameState gameState;

        protected override void OnEnable()
        {
            if (!inventory) throw new ArgumentNullException(nameof(inventory));
            if (!gameState) throw new ArgumentNullException(nameof(gameState));

            base.OnEnable();
        }

        protected override void OnInteract(PhonePart part)
        {
            if (!part.Assemblable)
            {
                Debug.Log("Item can not be assembled");
            }
            else
            {
                inventory.Remove(part);
                part.Phone.AddPart(part);
                gameState.CheckPhone(part.Phone);
            }
        }
    }
}
