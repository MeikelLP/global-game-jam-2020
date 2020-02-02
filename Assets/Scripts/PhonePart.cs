using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhonePart : MonoBehaviour
{
    public PhonePart[] dependsOn = new PhonePart[0];
    public string title;
    public bool broken;

    public float price;
    public Vector3 OriginalLocalPosition { get; set; }
    public Quaternion OriginalLocalRotation { get; set; }
    public Vector3 OriginalLocalScale { get; set; }

    public Dictionary<MeshRenderer, Material> OriginalMaterials { get; set; } =
        new Dictionary<MeshRenderer, Material>();

    public bool Disassemblable => dependsOn.All(blockingPart => !blockingPart.Assembled);

    public bool Assemblable => Phone.GetDependents(this).All(x => x.Assembled);

    public bool Assembled { get; set; }
    public Phone Phone { get; set; }

    public void Initialize(Phone phone)
    {
        Phone = phone;
        var t = transform;
        OriginalLocalPosition = t.localPosition;
        OriginalLocalRotation = t.localRotation;
        OriginalLocalScale = t.localScale;

        Assembled = true;
    }

    private void Reset()
    {
        title = gameObject.name;
    }

    public override string ToString()
    {
        return $"{Phone.title}/{title}";
    }
    
    public void SetColor(Material mat)
    {
        if (TryGetComponent<MeshRenderer>(out var meshRenderer))
        {
            if (!OriginalMaterials.ContainsKey(meshRenderer))
            {
                OriginalMaterials.Add(meshRenderer, meshRenderer.sharedMaterial);
            }
            meshRenderer.sharedMaterial = mat;
            return;
        }
        
        var comps = GetComponentsInChildren<MeshRenderer>();
        foreach (var partRenderer in comps)
        {
            if (!OriginalMaterials.ContainsKey(partRenderer))
            {
                OriginalMaterials.Add(partRenderer, partRenderer.sharedMaterial);
            }
            partRenderer.sharedMaterial = mat;
        }
    }
}
