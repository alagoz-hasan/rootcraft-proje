using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Color color;
    private Color[] colors = { Color.white, Color.red, Color.blue, Color.green };
    public int colorIndex;

    private void Start()
    {
        colorIndex = 0;
        color = colors[colorIndex];
        GameObject.FindGameObjectWithTag("ColorButton").GetComponent<Image>().color = color;
        GameObject.FindGameObjectWithTag("NextColor").GetComponent<Image>().color = colors[colorIndex+1];
    }
    public void Up()
    {
        if (GameObject.FindWithTag("GridManager").GetComponent<GridManager>().rowLength != 3)
        {
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().rowLength -= 1;
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().up = true;
        }
    }
    public void Down()
    {
        if (GameObject.FindWithTag("GridManager").GetComponent<GridManager>().rowLength != 7)
        {
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().rowLength += 1;
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().down = true;
        }
    }
    public void Left()
    {
        if (GameObject.FindWithTag("GridManager").GetComponent<GridManager>().columnLength != 3)
        {
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().columnLength -= 1;
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().left = true;
        }
    }
    public void Right()
    {
        if (GameObject.FindWithTag("GridManager").GetComponent<GridManager>().columnLength != 6)
        {
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().columnLength += 1;
            GameObject.FindWithTag("GridManager").GetComponent<GridManager>().right = true;
        }
    }

    public void ColorButton()
    {
        if (colorIndex == colors.Length - 1)
        {
            colorIndex = 0;
            color = colors[colorIndex];
            GameObject.FindGameObjectWithTag("ColorButton").GetComponent<Image>().color = color;
            GameObject.FindGameObjectWithTag("NextColor").GetComponent<Image>().color = colors[1];
        }
        else
        {
            colorIndex += 1;
            color = colors[colorIndex];
            GameObject.FindGameObjectWithTag("ColorButton").GetComponent<Image>().color = color;
            if(colorIndex == colors.Length-1)
            {
                GameObject.FindGameObjectWithTag("NextColor").GetComponent<Image>().color = colors[0];
            }
            else
            {
                GameObject.FindGameObjectWithTag("NextColor").GetComponent<Image>().color = colors[colorIndex+1];
            }
        }
    }
    public void Save()
    {
        GameObject.FindWithTag("GridManager").GetComponent<GridManager>().SaveData();
    }
}
