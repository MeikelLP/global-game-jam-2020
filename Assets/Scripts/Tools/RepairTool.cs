using UnityEngine;

namespace Tools
{
    public class RepairTool : Tool
    {
        public Material dissasemblableMaterial;

        protected override void OnInteract(PhonePart part)
        {
            Repair(part);
        }

        public bool Repair(PhonePart phonePart)
        {
            if (phonePart.Assembled)
            {
                UserFeedback.Instance.ShowInfoMessage("Assembled items can not be repaired");
                return false;
            }
            phonePart.broken = false;
            phonePart.SetColor(dissasemblableMaterial);
            return true;
        }
    }
}