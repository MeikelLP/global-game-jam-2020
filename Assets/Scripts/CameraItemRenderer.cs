using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItemRenderer : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private Transform positionAnchor;

    [Header("Debug")] public bool renderOnStart;
    public Phone phone;
    public Texture2D[] textures;
    public Dictionary<string, Texture2D> Images { get; } = new Dictionary<string, Texture2D>();

    private void Start()
    {
        if (renderOnStart)
        {
            StartCoroutine(StartRendering(phone));
        }
    }

    public IEnumerator StartRendering(Phone phone)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        // create textures instantly to use them already
        foreach (var part in phone.parts)
        {
            var tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Images.Add(part.ToString(), tex);
        }

        textures = new Texture2D[phone.parts.Length];

        for (var i = 0; i < phone.parts.Length; i++)
        {
            var part = phone.parts[i];
            var t = part.transform;
            var originalParent = t.parent;
            var originalLocalPosition = t.localPosition;
            var originalLocalRotation = t.localRotation;
            var originalLayer = t.gameObject.layer;

            t.SetParent(positionAnchor, false);
            t.gameObject.layer = LayerMask.NameToLayer("Render");

            yield return waitForEndOfFrame;

            var texture = ToTexture2D(renderTexture, Images[part.ToString()]);
            Images[part.ToString()] = texture;
            textures[i] = texture;
            t.SetParent(originalParent, false);
            t.localPosition = originalLocalPosition;
            t.localRotation = originalLocalRotation;
            t.gameObject.layer = originalLayer;
        }
    }

    private static Texture2D ToTexture2D(RenderTexture rTex, Texture2D tex)
    {
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
