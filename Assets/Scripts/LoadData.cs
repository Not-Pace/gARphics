using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadData : MonoBehaviour
{
    //public string inputfile;

    private List<Dictionary<string, object>> points;

    // Indices for columns to be assigned
    private int columnX = Globals.column1;
    private int columnY = Globals.column2;
    private int columnZ = Globals.column3;

    public bool renderPrefabsWithColor = true;

    // Full column names
    public string xName;
    public string yName;
    public string zName;

    private float xMax;
    private float yMax;
    private float zMax;
    private float xMin;
    private float yMin;
    private float zMin;

    public GameObject PointPrefab;

    public GameObject PointHolder;

    public float plotScale = 10;


    public GameObject objA;
    public GameObject objB;

    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = (LineRenderer)this.GetComponent(typeof(LineRenderer));
    }



    // Start is called before the first frame update
    void Start()
    {
        points = Globals.pointList;
        // Debug.Log(points.Count);

        lineRenderer = (LineRenderer)this.GetComponent(typeof(LineRenderer));


        List<string> columnList = new List<string>(points[1].Keys);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

        // Get maxes of each axis
        xMax = FindMaxValue(xName);
        yMax = FindMaxValue(yName);
        zMax = FindMaxValue(zName);

        // Get minimums of each axis
        xMin = FindMinValue(xName);
        yMin = FindMinValue(yName);
        zMin = FindMinValue(zName);

        // AssignLabels();

        for (var i =0; i< points.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name, normalize
            float x =
            (Convert.ToSingle(points[i][xName]) - xMin) / (xMax - xMin);

            float y =
            (Convert.ToSingle(points[i][yName]) - yMin) / (yMax - yMin);

            float z =
            (Convert.ToSingle(points[i][zName]) - zMin) / (zMax - zMin);

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3(x, y, z) * plotScale,
                    Quaternion.identity);

            // Make dataPoint child of PointHolder object
            dataPoint.transform.parent = PointHolder.transform;

            // Assigns original values to dataPointName
            string dataPointName =
            points[i][xName] + " "
            + points[i][yName] + " "
            + points[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;

            // Gets material color and sets it to a new RGBA color we define

            // dataPoint.GetComponent<Renderer>().material.color =
            // new Color(x, y, z, 1.0f);
            if (renderPrefabsWithColor)
            {
                // Sets color according to x/y/z value
                dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, z, 1.0f);

                // Activate emission color keyword so we can modify emission color
                dataPoint.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

                dataPoint.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(x, y, z, 1.0f));

            }

        }

    }

    // Finds labels named in scene, assigns values to their text meshes
    // WARNING: game objects need to be named within scene
    private void AssignLabels()
    {
        // Update point counter
        GameObject.Find("Point_Count").GetComponent<TextMesh>().text = points.Count.ToString("0");

        // Update title according to inputfile name
        // GameObject.Find("Dataset_Label").GetComponent<TextMesh>().text = inputfile;

        // Update axis titles to ColumnNames
        GameObject.Find("X_Title").GetComponent<TextMesh>().text = xName;
        GameObject.Find("Y_Title").GetComponent<TextMesh>().text = yName;
        GameObject.Find("Z_Title").GetComponent<TextMesh>().text = zName;

        // Set x Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        GameObject.Find("X_Min_Lab").GetComponent<TextMesh>().text = xMin.ToString("0.0");
        GameObject.Find("X_Mid_Lab").GetComponent<TextMesh>().text = (xMin + (xMax - xMin) / 2f).ToString("0.0");
        GameObject.Find("X_Max_Lab").GetComponent<TextMesh>().text = xMax.ToString("0.0");

        // Set y Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        GameObject.Find("Y_Min_Lab").GetComponent<TextMesh>().text = yMin.ToString("0.0");
        GameObject.Find("Y_Mid_Lab").GetComponent<TextMesh>().text = (yMin + (yMax - yMin) / 2f).ToString("0.0");
        GameObject.Find("Y_Max_Lab").GetComponent<TextMesh>().text = yMax.ToString("0.0");

        // Set z Labels by finding game objects and setting TextMesh and assigning value (need to convert to string)
        GameObject.Find("Z_Min_Lab").GetComponent<TextMesh>().text = zMin.ToString("0.0");
        GameObject.Find("Z_Mid_Lab").GetComponent<TextMesh>().text = (zMin + (zMax - zMin) / 2f).ToString("0.0");
        GameObject.Find("Z_Max_Lab").GetComponent<TextMesh>().text = zMax.ToString("0.0");

    }

    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(points[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < points.Count; i++)
        {
            if (maxValue < Convert.ToSingle(points[i][columnName]))
                maxValue = Convert.ToSingle(points[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {

        float minValue = Convert.ToSingle(points[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < points.Count; i++)
        {
            if (Convert.ToSingle(points[i][columnName]) < minValue)
                minValue = Convert.ToSingle(points[i][columnName]);
        }

        return minValue;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
