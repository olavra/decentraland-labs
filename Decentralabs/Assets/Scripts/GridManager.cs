using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static GridManager _instance;

    public GameObject[] sceneTemplates;
    
    public GameObject player;

    public int gridRadius = 5;
    
    private int sceneSize = 16;
    
    public Vector2 currentParcel = Vector2.zero;

    private GameObject _sceneHolder;

    private List<Scene> _scenes;

    public static GridManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else _instance = this;
    }

    void Start()
    {
        _scenes = new List<Scene>();

        _sceneHolder = new GameObject("Scenes");
        _sceneHolder.transform.position = Vector3.zero;
        _sceneHolder.transform.rotation = Quaternion.identity;

        buildParcels();
    }

    public Vector3 getPositionFromCoords(int x, int z)
    {
        return new Vector3(x*this.sceneSize, 0f, z*this.sceneSize);
    }

    public void buildParcels()
    {
        foreach(Scene s in this._scenes)
        {
            s.removable = true;
        }

        for(int z = -gridRadius; z <= gridRadius; z++)
        {
            for(int x = -gridRadius; x <= gridRadius; x++)
            {
                int parcelX = (int)this.currentParcel.x - x;
                int parcelZ = (int)this.currentParcel.y - z;

                if(parcelX == 0 && parcelZ == 2) continue;

                if (!checkSceneExists(new Vector2(parcelX, parcelZ)))
                {
                    Vector3 position = getPositionFromCoords(parcelX, parcelZ);
                    Quaternion rotation = new Quaternion(0f, 90f*UnityEngine.Random.Range(0,3), 0f, 0f);
                    GameObject scene = Instantiate(this.sceneTemplates[UnityEngine.Random.Range(0,sceneTemplates.Length)], position, rotation);
                    scene.transform.SetParent(_sceneHolder.transform);
                    Scene s = new Scene(scene, parcelX, parcelZ);
                    this._scenes.Add(s);
                }
            }
        }
    }

    public void destroyParcels()
    {
        for (int i = this._scenes.Count - 1; i >= 0; i--)
        {
            if(_scenes[i].removable)
            {
                Destroy(_scenes[i].asset);
                this._scenes.RemoveAt(i);
            }
        }
    }

    public bool checkSceneExists(Vector2 coords)
    {
        foreach(Scene s in this._scenes)
        {
            if(s.coords == coords)
            {
                s.removable = false;
                return true;
            }
        }
        return false;
    }

    public void calculatePlayerParcel()
    {
        Vector2 newCoords = new Vector2((int) (player.transform.position.x / this.sceneSize), (int) (player.transform.position.z / this.sceneSize));

        if(newCoords != currentParcel)
        {
            this.currentParcel = newCoords;
            buildParcels();
            destroyParcels();
        }

        
    }

    public void drawGridlines()
    {
        int numberLines = 2;
        int lineSize = (this.sceneSize/2) * (2*2+1);
        
        Vector2 startingGridPos = this.currentParcel*16;

        //Draw z lines
        for (int i = -numberLines; i <= numberLines; i++)
        {
            float mposx = startingGridPos.x + i*this.sceneSize;
            Debug.DrawLine(new Vector3(mposx, 0, startingGridPos.y-lineSize), new Vector3(mposx,0, startingGridPos.y+lineSize), Color.blue);
        }

        //Draw x lines
        for (int j = -numberLines; j <= numberLines; j++)
        {
            float mposz = startingGridPos.y + j*this.sceneSize;

            Debug.DrawLine(new Vector3(startingGridPos.x-lineSize, 0, mposz), new Vector3(startingGridPos.x+lineSize,0,mposz), Color.red);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.calculatePlayerParcel();
        this.drawGridlines();
    }
}
