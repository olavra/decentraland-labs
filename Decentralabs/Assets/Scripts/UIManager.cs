using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject compass;

    public GameObject UICoordsIndicator;

    public GameObject playerCamera;

    private GridManager gridManager;


    public Text FPSIndicator;

    public float deltaTime;


    // Start is called before the first frame update
    void Start()
    {
        gridManager = GridManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        this.compass.transform.rotation = Quaternion.Euler(0, 0, -playerCamera.transform.rotation.eulerAngles.y);

        this.UICoordsIndicator.GetComponent<Text>().text = gridManager.currentParcel.x + ", " + gridManager.currentParcel.y;

        //update fps
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        if(fps >= 60f)
        {
            FPSIndicator.color = Color.green;
        }
        else if( fps >= 30f)
        {
            FPSIndicator.color = Color.yellow;
        }
        else
        {
            FPSIndicator.color = Color.red;
        }
        FPSIndicator.text = Mathf.Ceil(fps).ToString() + " FPS";
     }
}
