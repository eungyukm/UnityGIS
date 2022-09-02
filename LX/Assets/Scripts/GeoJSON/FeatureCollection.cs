using System.Collections.Generic;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class FeatureCollection : GeoJSONObject
{
    public List<FeatureObject> features;

    public FeatureCollection(string encodedString)
    {
        features = new List<FeatureObject>();
        JObject jObject = JObject.Parse(encodedString);
        ParseFeatures(jObject["features"]);
        type = "FeatureCollection";
    }

    public FeatureCollection(JObject jObject) : base(jObject)
    {
        features = new List<FeatureObject>();
        
        ParseFeatures(jObject["features"]);
    }

    public FeatureCollection()
    {
        features = new List<FeatureObject>();
        type = "Feature";
    }

    protected void ParseFeatures(JToken jToken)
    {
        foreach (var token in jToken)
        {
            string json = token.ToString();
            features.Add(new FeatureObject(JObject.Parse(json)));
        }
    }
}
