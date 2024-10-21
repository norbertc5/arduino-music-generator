using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScrolling : MonoBehaviour
{
    public static Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.isPlaying)
        {
            transform.localPosition -= new Vector3(Manager.tempo, 0) * Time.deltaTime;
        }
    }
}
