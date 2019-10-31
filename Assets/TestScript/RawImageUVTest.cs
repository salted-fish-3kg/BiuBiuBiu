using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawImageUVTest : MonoBehaviour
{
    Camera _camera;
    Transform[] transforms;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        transforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            transforms[i] = transform.GetChild(i);
        }
        Debug.Log(transforms.Length);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 11; i++)
        {
            Debug.DrawLine(_camera.ViewportToWorldPoint(Vector3.up * (i / 10f)), _camera.ViewportToWorldPoint(Vector3.right + Vector3.up * (i / 10f)), Color.red);
            Debug.DrawLine(_camera.ViewportToWorldPoint(Vector3.right * (i / 10f)), _camera.ViewportToWorldPoint(Vector3.up + Vector3.right * (i / 10f)), Color.red);
        }
        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector3 _pos = _camera.ViewportToWorldPoint(Vector3.up * (i / 5f+0.1f) + Vector3.right * (j / 5f+0.1f));
                if (count < transforms.Length)
                transforms[count].position = _pos;
                count++;
            }
        }
        
    }
}
