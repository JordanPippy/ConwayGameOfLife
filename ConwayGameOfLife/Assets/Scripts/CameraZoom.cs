using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update

    private float ortho;
    void Start()
    {
        Camera.main.orthographicSize = 50.0f;
        ortho = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll != 0.0f)
        {
            ortho -= scroll * 10.0f;
        }

        //Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, ortho, Time.deltaTime * 10f);
        Camera.main.orthographicSize = ortho;
    }
}
