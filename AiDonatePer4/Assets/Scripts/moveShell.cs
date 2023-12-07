using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShell : MonoBehaviour
{
    float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, (speed * Time.deltaTime)/2.0f, Time.deltaTime*speed);
    }
}
