using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapesPool: MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameObject[] shapes;

    Dictionary<string, GameObject> shapesDic;


    private void Start()
    {
        InitializeShapes();
    }

    //instantiate all the shapes into the scene and disable them
    public void InitializeShapes()
    {
        shapesDic = new Dictionary<string, GameObject>();

        for (int i = 0; i < shapes.Length; i++)
        {
            //instantiate shape
            var _shape = Instantiate(shapes[i], this.transform);

            //deactivate instatiated shape
            _shape.SetActive(false);

            //store instantiated shape in a dictionary
            shapesDic.Add(XMLManager.xMLManager.shapeDB.shapes[i].Name, _shape);
        }
    }

    //when player choose a shape from drop menu
    public void DisplayShape()
    {
        DeactivateAllShapes();

        switch (dropdown.value)
        {
            case 0:
                var cube = shapesDic[XMLManager.xMLManager.shapeDB.shapes[0].Name];
                SpawnShape(cube, 0);
                break;

            case 1:
                var cylindar = shapesDic[XMLManager.xMLManager.shapeDB.shapes[1].Name];
                SpawnShape(cylindar, 1);
                break;

            case 2:
                var sphere = shapesDic[XMLManager.xMLManager.shapeDB.shapes[2].Name];
                SpawnShape(sphere, 2);
                break;
        }
    }

    //before new shape is displayed in the screen, the previous shown shapes should be hidden
    private void DeactivateAllShapes()
    {
        foreach (string key in shapesDic.Keys)
        {
            shapesDic[key].SetActive(false);
        }
    }

    private void SpawnShape(GameObject _shape, int index)
    {
        //activate shape
        _shape.SetActive(true);

        //update its properties from Database
        _shape.transform.position = XMLManager.xMLManager.shapeDB.shapes[index].Position;
        _shape.transform.rotation = XMLManager.xMLManager.shapeDB.shapes[index].Rotation;
        _shape.transform.localScale = XMLManager.xMLManager.shapeDB.shapes[index].Size;
        _shape.GetComponent<MeshRenderer>().material.color = XMLManager.xMLManager.shapeDB.shapes[index].Color;
    }
}
