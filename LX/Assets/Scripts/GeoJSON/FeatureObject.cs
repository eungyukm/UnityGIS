using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class FeatureObject
{
    public string type;
    public GeometryObject geometry;
    public Dictionary<string, string> properties;

    public FeatureObject(JObject jsonObject)
    {
        type = jsonObject["type"].ToString();
        string geometryStr = jsonObject["geometry"].ToString();
        geometry = parseGeometry(JObject.Parse(geometryStr));

        properties = new Dictionary<string, string>();
        string propertiesStr = jsonObject["properties"].ToString();
        parseProperties (JObject.Parse(propertiesStr));
    }
    
    protected void parseProperties(JObject jsonObject) {
        foreach (var jProperty in jsonObject.Properties())
        {
            string key = jProperty.Name;
            string value = jsonObject[key].ToString();
            properties.Add (key, value);
        }
    }

    protected GeometryObject parseGeometry(JObject jObject)
    {
        Debug.Log("JObject : " + jObject.ToString());
        switch (jObject["type"].ToString())
        {
            // case "Point":
            //     return new PointGeometryObject (jsonObject);
            // case "MultiPoint":
            //     return new MultiPointGeometryObject (jsonObject);
            // case "LineString":
            //     return new LineStringGeometryObject (jsonObject);
            // case "MultiLineString":
            //     return new MultiLineStringGeometryObject (jsonObject);
            case "Polygon":
                return new PolygonGeometryObject (jObject);
            case "MultiPolygon":
                return new MultiPolygonGeometryObject (jObject);
            default:
                break;
        }
        return null;
    }
}
