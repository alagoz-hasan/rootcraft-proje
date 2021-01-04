using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour
{
    private void OnMouseDown()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.color = GameObject.FindWithTag("Canvas").GetComponent<Buttons>().color;
    }
}
