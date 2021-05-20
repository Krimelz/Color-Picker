using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public ColorPicker colorPicker;

    private Ray ray;
    private RaycastHit hit;

    private GameObject cube;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GetCubeColor();
        }

        if (cube != null) {
            SetCubeColor();
        }
    }

    private void GetCubeColor() {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Cube")) {
                cube = hit.collider.gameObject;

                var color = cube.GetComponent<MeshRenderer>().material.color;
                Debug.Log(color);
                colorPicker.SetColor(color);
            }
        }
    }

    private void SetCubeColor() {
        cube.GetComponent<MeshRenderer>().material.color = colorPicker.GetColor();
    }
}
