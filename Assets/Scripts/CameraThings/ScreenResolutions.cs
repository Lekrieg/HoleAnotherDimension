using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolutions : MonoBehaviour
{
    public bool maintainWidth = true;

    [Range(-1, 1)]
    public int adaptPosition;

    float defaultWidth;
    float defaultHeight;

    Vector3 cameraPos;

    private void Start()
    {
        cameraPos = Camera.main.transform.position;

        defaultHeight = Camera.main.orthographicSize;
        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    private void Update()
    {
        if(maintainWidth)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            Camera.main.transform.position = new Vector3(cameraPos.x, (defaultHeight - Camera.main.orthographicSize) * adaptPosition, cameraPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3((defaultWidth - Camera.main.orthographicSize * Camera.main.aspect) * adaptPosition, cameraPos.y, cameraPos.z);
        }
    }
}
