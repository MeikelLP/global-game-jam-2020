using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PhoneScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    public string[] scenesToLoad;

    private IEnumerator Start()
    {
        var ops = new List<AsyncOperation>();
        foreach (var sceneName in scenesToLoad)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (scene.isLoaded) continue;

            ops.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
        }

        yield return new WaitUntil(() => ops.All(x => x.isDone));

        var spawner = FindObjectOfType<PhoneSpawner>();
        spawner.Initialize();

        var renderer = FindObjectOfType<CameraItemRenderer>();
        StartCoroutine(renderer.StartRendering(spawner.ActivePhone));

        var shop = FindObjectOfType<ShopManagerBehaviour>();
        shop.Initialize(spawner.ActivePhone);
    }
}
