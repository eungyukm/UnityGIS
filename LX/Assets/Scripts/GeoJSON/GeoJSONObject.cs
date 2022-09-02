using UnityEngine;
using Newtonsoft.Json.Linq;

public class GeoJSONObject
{
    public string type;

    public GeoJSONObject()
    {
    }

    public GeoJSONObject(JObject jObject)
    {
        if (jObject != null)
        {
            type = jObject["type"].ToString();
        }
    }

    public static FeatureCollection Deserialize(string encodedString)
    {
        FeatureCollection collection = null;

        JObject jsonObject = JObject.Parse(encodedString);
        Debug.Log(jsonObject["type"].ToString());

        if (jsonObject["type"].ToString() == "FeatureCollection")
        {
            Debug.Log("FeatureCollection Call!!");
            collection = new FeatureCollection(jsonObject);
        }
        else
        {
            collection = new FeatureCollection();
            collection.features.Add( new FeatureObject(jsonObject));
        }

        return collection;
    }
}