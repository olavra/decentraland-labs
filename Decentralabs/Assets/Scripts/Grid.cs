using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{

    public GameObject[] scenes;
    public GameObject player;

    public GameObject UIplayerCoords;
    public GameObject compass;

    public int gridSize = 50;
    private GameObject[][] grid;
    private int sceneSize = 16;

    private GameObject sceneHolder;

    public Vector2 currentParcel = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {

        sceneHolder = new GameObject("Scenes");
        sceneHolder.transform.position = Vector3.zero;
        sceneHolder.transform.rotation = Quaternion.identity;

        for(int z = 0; z < this.gridSize; z++)
        {
            for (int x = 0; x < this.gridSize; x++)
            {

                int posx = x*sceneSize;
                int posy = z*sceneSize;
                Vector3 position = new Vector3(x*sceneSize, 0, z*sceneSize);
                Quaternion rotation = new Quaternion(0f, 90f*UnityEngine.Random.Range(0,3), 0f, 0f);
                GameObject scene = Instantiate(this.scenes[UnityEngine.Random.Range(0,scenes.Length)], position, rotation);
                scene.transform.SetParent(sceneHolder.transform);

                Scene s = new Scene(scene, posx, posy);
            }
            
        }
    }

    public void GetPlayerParcel()
    {
        int x = (int) (player.transform.position.x / this.gridSize);
        int z = (int) (player.transform.position.z / this.gridSize);
        this.currentParcel.x = x;
        this.currentParcel.y = z;

        this.UIplayerCoords.GetComponent<Text>().text = x + ", " + z;
    }
    
    public void CalculateCompass()
    {
        this.compass.transform.rotation = Quaternion.Euler(0, 0, -player.transform.rotation.eulerAngles.y);

        //this.compass.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * 5.0f);
        //this.compass.transform.rotation = Quaternion.Euler(new Vector3(0,0,));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetPlayerParcel();
        this.CalculateCompass();
        /*
        for (int i = 0; i <= 100; i++)
        {
            Debug.DrawLine(new Vector3(i*16, 0, 0), new Vector3(i*16, 0, 100), Color.white);
        }
        */
    }
}
