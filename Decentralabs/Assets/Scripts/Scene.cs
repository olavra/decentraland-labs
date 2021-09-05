using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class SceneManager
{
    static List<Scene> Scenes = new List<Scene>();
    public static int TotalInstances;

    public static void Add(Scene newScene)
    {
        if (Scenes.Find(s => s == newScene) is null)
        {
            Scenes.Add(newScene);
        }
    }

    public static void Remove(Scene removeScene)
    {
        Scenes.Remove(removeScene);
    }

    public static Scene GetSceneByCoord(int x, int y)
    {
        Vector2 position = new Vector2(x, y);
        foreach (Scene s in Scenes)
        {
            if(s.position == position)
            {
                return s;
            }
        }
        return null; 
    }
}

public class Scene
{
    private GameObject asset;
    public Vector2 position;

    public Scene(GameObject asset, int x, int y)
    {
        this.position = new Vector2(x, y);
        this.asset = asset;
        SceneManager.Add(this);
    }

    public void destroy()
    {
        SceneManager.Remove(this);
    }
}
