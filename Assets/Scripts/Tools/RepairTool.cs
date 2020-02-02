using UnityEngine;

namespace Tools
{
    public class RepairTool : Tool
    {
        public Material dissasemblableMaterial;

        protected override void OnInteract(PhonePart part)
        {
            if (part.Assembled)
            {
                UserFeedback.Instance.ShowInfoMessage("Assembled items can not be repaired");
            }
            else
            {
                part.broken = false;
                part.SetColor(dissasemblableMaterial);
            }
        }
    }
}