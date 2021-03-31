using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[Serializable]
public class Shape
{
    public string Name;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Size;
    public Color Color;
}

[Serializable]
public class ShapeDatabase
{
    [XmlArray("Shapes")]
    public List<Shape> shapes;
}


public class XMLManager : MonoBehaviour
{
    public ShapeDatabase shapeDB;

    #region Singlton
    public static XMLManager xMLManager;
    private void Awake()
    {
        xMLManager = this;
    }
    #endregion

    public void Save()
    {
        //create new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ShapeDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/PrimitiveShape.xml", FileMode.Create);

        //serialize data to xml file
        serializer.Serialize(stream, shapeDB);

        //close stream
        stream.Close();
    }

    public void Load()
    {
        //open xml file
        XmlSerializer serializer = new XmlSerializer(typeof(ShapeDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/XML/PrimitiveShape.xml", FileMode.Open);

        //deserialize data from xml file to ShapeDataBase object
        shapeDB = serializer.Deserialize(stream) as ShapeDatabase;

        //close stream
        stream.Close();
    }

}
