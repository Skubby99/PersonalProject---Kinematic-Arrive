using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            GameObject Inmovable = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if(Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                Inmovable.transform.position = hit.point;
        }
    }
}
