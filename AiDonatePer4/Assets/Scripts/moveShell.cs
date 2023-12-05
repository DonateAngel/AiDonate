using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveShell : MonoBehaviour
{
    float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, Time.deltaTime*speed);
    }
}
