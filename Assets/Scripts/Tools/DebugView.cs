using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Tools
{
    public class DebugView : Tool
    {
        [SerializeField] private Material blockenMaterial;
        [SerializeField] private Material changableMaterial;

        public void Refresh(Phone phone)
        {
            // reset original materials
            foreach (var part in phone.parts)
            {
                foreach (var pair in part.OriginalMaterials)
                {
                    pair.Key.sharedMaterial = pair.Value;
                }

                part.OriginalMaterials.Clear();
            }

            var parts = phone.parts.Where(x => x.broken).ToArray();

            foreach (var part in parts)
            {
                SetColor(part, blockenMaterial);
            }

            var changableParts = (Toolbar.Instance.toolMode == ToolMode.Disassemble
                ? phone.parts.Where(x => x.Disassemblable && !x.broken)
                : phone.parts.Where(x => x.Assemblable)).ToArray();

            foreach (var part in changableParts)
            {
                SetColor(part, changableMaterial);
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
