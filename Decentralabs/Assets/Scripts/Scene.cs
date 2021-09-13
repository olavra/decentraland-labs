using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene
{
    public GameObject asset;
    public Vector2 coords;

    public bool removable = false;

    public Scene(GameObject asset, int x, int y)
    {
        this.coords = new Vector2(x, y);
        this.asset = asset;
    }

    public void destroy()
    {

    }
}
