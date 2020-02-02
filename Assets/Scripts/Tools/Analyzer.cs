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
                SetColor(part, brokenMaterial);
            }
        }

        private static void SetColor(PhonePart part, Material mat)
        {
            if (part.TryGetComponent<MeshRenderer>(out var comp))
            {
                part.OriginalMaterials.Add(comp, comp.sharedMaterial);
                comp.sharedMaterial = mat;
            }
            else
            {
                var comps = part.GetComponentsInChildren<MeshRenderer>();
                foreach (var renderer in comps)
                {
                    part.OriginalMaterials.Add(renderer, renderer.sharedMaterial);
                    renderer.sharedMaterial = mat;
                }
            }
        }
    }
}
