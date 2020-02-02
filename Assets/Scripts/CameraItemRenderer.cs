using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItemRenderer : MonoBehaviour
{
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private Transform positionAnchor;

    public Dictionary<string, Texture2D> Images { get; } = new Dictionary<string, Texture2D>();

    public IEnumerator StartRendering(Phone phone)
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        // create textures instantly to use them already
        foreach (var part in phone.parts)
        {
            var tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            Images.Add(part.ToString(), tex);
        }

        foreach (var part in phone.parts)
        {
            var t = part.transform;
            var originalParent = t.parent;
            var originalLocalPosition = t.localPosition;
            var originalLocalRotation = t.localRotation;

            t.SetParent(positionAnchor, false);

            yield return waitForEndOfFrame;

            Images[part.ToString()] = ToTexture2D(renderTexture, Images[part.ToString()]);
            t.SetParent(originalParent, false);
            t.localPosition = originalLocalPosition;
            t.localRotation = originalLocalRotation;
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
