using System;
using GameScripts;
using UnityEngine;

namespace Tools
{
    public class Dissassembler : Tool
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
            if (!part.Disassemblable)
            {
                UserFeedback.Instance.ShowInfoMessage("Item can not be disassembled!");
            }
            else
            {
                inventory.Add(part);
                part.Phone.RemovePart(part);
            }
        }
    }
}
