using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class PositionObject
{
    public float latitude;
    public float longitude;

    public PositionObject()
    {
        
    }

    public PositionObject(float pointLatitude, float pointLongitude)
    {
        this.latitude = pointLatitude;
        this.longitude = pointLongitude;
    }

    public PositionObject(JToken jToken)
    {
        Debug.Log("[PositionObject] jToken : " + jToken.ToString());
        latitude = float.Parse(jToken[0].ToString());
        longitude = float.Parse(jToken[1].ToString());
    }

    public JObject Serialize()
    {
        JObject jsonObject = new JObject();
        jsonObject.Add(longitude);
        jsonObject.Add(latitude);

        return jsonObject;
    }

    public override string ToString()
    {
        return longitude + "," + latitude;
    }

    public float[] ToArray()
    {
        float[] array = new float[2];

        array[0] = longitude;
        array[1] = latitude;

        return array;
    }
}
