using UnityEngine;

namespace Tools
{
    public class Analyzer : Tool
    {
        [SerializeField] private Material brokenMaterial;
        [SerializeField] private Material dissasemblableMaterial;

        protected override void OnInteract(PhonePart part)
        {
            Material mat = null;
            if (part.broken)
            {
                mat = brokenMaterial;
            }
            else if (part.Disassemblable)
            {
                mat = dissasemblableMaterial;
            }

            if (mat != null)
            {
                part.SetColor(mat);
            }
        }
    }
}
