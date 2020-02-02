using UnityEngine;

namespace Tools
{
    public class RepairTool : Tool
    {
        protected override void OnInteract(PhonePart part)
        {
            if (part.Assembled)
            {
                Debug.Log("Assembled items can not be repaired");
                // TODO show ui message
            }
            else
            {
                part.broken = false;
            }
        }
    }
}