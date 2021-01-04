using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class GridManager : MonoBehaviour
{
    public List<List<GameObject>> circles = new List<List<GameObject>>();
    public int columnLength = 3, rowLength = 3;
    public float x_Space, y_Space;
    public GameObject prefab;
    private float maxLeft = -3f;
    private float maxRight = 3f;

    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnLength; i++)
        {
            circles.Add(new List<GameObject>());
            for (int j = 0; j < rowLength; j++)
            {
                GameObject circle = Instantiate(prefab, new Vector3(maxLeft + 1 + (x_Space * j), maxRight - 1 + (-y_Space * i)), Quaternion.identity);
                circle.GetComponent<SpriteRenderer>().color = Color.gray;
                circles[i].Add(circle);
            }
            
        }
    }
    private void Update()
    {
        int rows = circles.Count;
        int columns = circles[0].Count;
        if (up)
        {
            up = false;
            int a = circles[circles.Count - 1].Count;
            for (int i = 0; i < a; i++)
            {
                GameObject circle = circles[circles.Count - 1][i];
                Destroy(circle);
                
            }
            circles.RemoveAt(circles.Count - 1);

            Vector3 scaleFactor = circles[0][0].transform.localScale + new Vector3(0.08f, 0.08f, 0.08f);
            Vector3 position = new Vector3(0f, -1f, 0f);
            Vector3 aa = new Vector3(0f, -0.2f, 0f);
            ScaleGridY(scaleFactor, position, aa);
        }
        if (down)
        {
            down = false;
            circles.Add(new List<GameObject>());
            Vector3 pos = circles[rows - 2][0].transform.position - circles[rows - 1][0].transform.position;
            pos.x = 0f;
            pos.z = 0f;
            for (int i = 0; i < columns; i++)
            {
                GameObject circle = Instantiate(prefab, circles[rows - 1][i].transform.position - pos, Quaternion.identity);
                circle.GetComponent<SpriteRenderer>().color = Color.gray;
                circles[rows].Add(circle);
            }
            Vector3 scaleFactor = circles[0][0].transform.localScale - new Vector3(0.08f, 0.08f, 0.08f);
            Vector3 position = new Vector3(0f, 1f, 0f);
           
            Vector3 a = new Vector3(0f, 0.2f, 0f);
            ScaleGridY(scaleFactor,position,a);
        }
        if (left)
        {
            left = false;
            int count = circles.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject circle = circles[i][columns - 1];
                Destroy(circle);
                circles[i].RemoveAt(columns-1);
            }

            Vector3 scaleFactor = circles[0][0].transform.localScale + new Vector3(0.08f, 0.08f, 0.08f);
            Vector3 position = new Vector3(1f, 0f, 0f);
            Vector3 a = new Vector3(-0.4f, 0f, 0f);
            ScaleGridX(scaleFactor, position,a);
        }
        if (right)
        {
            right = false;
            int count = circles.Count;
            Vector3 pos = circles[0][columns-2].transform.position - circles[0][columns-1].transform.position;
            pos.y = 0f;
            pos.z = 0f;
            for (int i = 0; i < count; i++)
            { 
                GameObject circle = Instantiate(prefab, circles[i][columns-1].transform.position - pos, Quaternion.identity);
                circle.GetComponent<SpriteRenderer>().color = Color.gray;
                circles[i].Add(circle);
            }
            Vector3 scaleFactor = circles[0][0].transform.localScale - new Vector3(0.08f, 0.08f, 0.08f);
            Vector3 position = new Vector3(-1f, 0f, 0f);
            Vector3 a = new Vector3(0.4f, 0f, 0f);
            ScaleGridX(scaleFactor, position, a);
        }
    }
    
    public void ScaleGridY(Vector3 scaleFactor, Vector3 position, Vector3 pos)
    {
        for(int i = 0; i < circles.Count; i++)
        {
            for(int j = 0; j< circles[0].Count; j++)
            {
                circles[i][j].transform.localScale = scaleFactor;
                circles[i][j].transform.position += position;
            }
        }
        for (int i = 0; i < circles.Count; i++)
        {
            for (int j = 0; j < circles[0].Count; j++)
            {
                circles[i][j].transform.position += pos;
            }
        }

    }
    public void ScaleGridX(Vector3 scaleFactor, Vector3 position, Vector3 pos)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            for (int j = 0; j < circles[0].Count; j++)
            {
                circles[i][j].transform.localScale = scaleFactor;
                circles[i][j].transform.position += position;
            }
        }
        int column = circles[0].Count;
        for (int i = 0; i < circles.Count; i++)
        {
            for (int j = 0; j < column; j++)
            {
                circles[i][j].transform.position += pos;
            }

        }
        for (int i = 0; i < circles.Count; i++)
        {
            for (int j = 1; j < circles[0].Count; j++)
            {
                circles[i][j].transform.position -= new Vector3(0.15f,0f,0f) * j;
            }
        }
    }
    private class GridData
    {
        public Vector3 position;
        public Color color;
    }
    public void SaveData()
    {
        string json="";
        List<GridData> datas = new List<GridData>();
        for(int i = 0; i < circles.Count; i++)
        {
            for(int j = 0; j < circles[0].Count; j++)
            {
                GridData data = new GridData();
                data.position = circles[i][j].transform.position;
                data.color = circles[i][j].GetComponent<SpriteRenderer>().color;
                json += JsonUtility.ToJson(data);
            }
        }
        File.WriteAllText(Application.dataPath + "/gridData.json", json);

    }
}
