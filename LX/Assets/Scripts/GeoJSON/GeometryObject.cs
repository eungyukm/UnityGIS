using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

[System.Serializable]
public class GeometryObject : GeoJSONObject
{
    public GeometryObject() : base()
    {
        
    }

    public GeometryObject(JObject jObject) : base(jObject)
    {
        
    }

    public virtual List<PositionObject> AllPositions()
    {
        return null;
    }

    public virtual PositionObject FirstPosition()
    {
        return null;
    }

    public virtual int PositionCount()
    {
        return 0;
    }

    // protected override void SerialzieConent(JObject rootObjet)
    // {
    //     
    // }

    protected virtual JObject SerializeGeometry()
    {
        return null;
    }
}

[System.Serializable]
public class SingleGeometryObject : GeometryObject
{
    public PositionObject coordinates;

    public SingleGeometryObject() : base()
    {
        type = "Point";
        coordinates = new PositionObject();
    }

    public SingleGeometryObject(float longitude, float latitude) : base()
    {
        type = "Point";
        coordinates = new PositionObject(longitude, latitude);
    }

    public SingleGeometryObject(JObject jObject) : base(jObject)
    {
        coordinates = new PositionObject(jObject["coordinates"]);
    }

    // public override List<PositionObject> AllPositons()
    // {
    //     List<PositionObject> list = new List<PositionObject>();
    //     list.Add(coordinates);
    //     return list;
    // }

    public override PositionObject FirstPosition()
    {
        return coordinates;
    }
    
}

[System.Serializable]
public class ArrayGeometryObject : GeometryObject {
	public List<PositionObject> coordinates;

	public ArrayGeometryObject(JObject jObject) : base(jObject) {
		coordinates = new List<PositionObject>();
		foreach(JObject j in jObject["coordinates"]){
			coordinates.Add(new PositionObject (j));
		}
	}

		override public List<PositionObject> AllPositions() {
			return coordinates;
		}

		override public PositionObject FirstPosition() {
			if(coordinates.Count > 0)
				return coordinates[0];

			return null;
		}

		override public int PositionCount() {
			return coordinates.Count;
		}

		override protected JObject SerializeGeometry() {

			JObject coordinateArray = new JObject ();
			foreach (PositionObject position in coordinates) {
				coordinateArray.Add (position.Serialize());
			}

			return coordinateArray;
		}
	}
	[System.Serializable]
	public class ArrayArrayGeometryObject : GeometryObject {
		public List<List<PositionObject>> coordinates;

		public ArrayArrayGeometryObject(JObject jObject) : base(jObject) {
			Debug.Log("Token : " + jObject.ToString());
			Debug.Log("Type : " + jObject["type"].ToString());
			
			coordinates = new List<List<PositionObject>> ();

			if (jObject["type"].ToString() == "MultiPolygon")
			{
				foreach (var l in jObject["coordinates"])
				{
					foreach (var l2 in l)
					{
						List<PositionObject> polygon = new List<PositionObject>();
						coordinates.Add (polygon);
						foreach (var l3 in l2)
						{
							polygon.Add (new PositionObject (l3));
						}
					}
				}
			}
			else
			{
				foreach (JToken l in jObject["coordinates"]) {
					List<PositionObject> polygon = new List<PositionObject>();
					Debug.Log("l : " + l.ToString());
					coordinates.Add (polygon);
					foreach (JToken l2 in l) {
						Debug.Log("l2 : " + l2.ToString());
						polygon.Add (new PositionObject (l2));
					}
				}
			}
		}

		public override List<PositionObject> AllPositions() {
			List<PositionObject> list = new List<PositionObject> ();
			foreach (List<PositionObject> l in coordinates) {
				foreach (PositionObject pos in l) {
					list.Add (pos);
				}
			}
			return list;
		}

		public override PositionObject FirstPosition() {
			if(coordinates.Count > 0 && coordinates[0].Count > 0)
				return coordinates[0][0];

			return null;
		}

		public override int PositionCount() {
			int totalPositions = 0;

			foreach (List<PositionObject> l in coordinates) {
				// Debug.Log("l.Count : " + l.Count);
				totalPositions += l.Count;
			}

			return totalPositions;
		}


		protected override JObject SerializeGeometry() {

			JObject coordinateArrayArray = new JObject ();

			foreach (List<PositionObject> l in coordinates) {
				JObject coordinateArray = new JObject ();
				foreach (PositionObject pos in l) {
					coordinateArray.Add (pos.Serialize());
				}
				coordinateArrayArray.Add (coordinateArray);
			}

			return coordinateArrayArray;
		}
	}

[System.Serializable]
public class PointGeometryObject : SingleGeometryObject {
    public PointGeometryObject(JObject jObject) : base(jObject) {
    }
    public PointGeometryObject(float longitude, float latitude) : base(longitude, latitude) {
    }
}
[System.Serializable]
public class MultiPointGeometryObject : ArrayGeometryObject {
    public MultiPointGeometryObject(JObject jObject) : base(jObject) {
    }
}

[System.Serializable]
public class LineStringGeometryObject : ArrayGeometryObject {
    public LineStringGeometryObject(JObject jObject) : base(jObject) {
    }
}
[System.Serializable]
public class MultiLineStringGeometryObject : ArrayArrayGeometryObject {
    public MultiLineStringGeometryObject(JObject jObject) : base(jObject) {
    }
}

[System.Serializable]
public class PolygonGeometryObject : ArrayArrayGeometryObject {
    public PolygonGeometryObject(JObject jObject) : base(jObject) {

    }
}
[System.Serializable]
public class MultiPolygonGeometryObject : ArrayArrayGeometryObject {
    public MultiPolygonGeometryObject(JObject jObject) : base(jObject) {
    }
}
