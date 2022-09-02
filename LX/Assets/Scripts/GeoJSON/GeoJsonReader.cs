using UnityEngine;

public class GeoJsonReader : MonoBehaviour
{
    public LineRenderer lr;
    
    private void Start()
    {
        var geojson = Resources.Load("Datas/MapSample.geojsonl") as TextAsset;
        FeatureCollection featureCollection = GeoJSONObject.Deserialize(geojson.text);
        
        double[] geometryXY = new double[featureCollection.features[0].geometry.PositionCount() * 2];
        int geometryCount = 0;
        foreach (var positionObject in featureCollection.features[0].geometry.AllPositions())
        {
            geometryXY[geometryCount] = positionObject.latitude;
            geometryXY[geometryCount + 1] = positionObject.longitude;
            geometryCount += 2;
        }
        
        double[] z = {0};
        
        string grs80 = "+proj=tmerc +lat_0=38 +lon_0=127.5 +k=0.9996 +x_0=1000000 +y_0=2000000 +ellps=GRS80 +units=m +no_defs";
        string wgs84 = "+title=WGS 84 (long/lat) +proj=longlat +ellps=WGS84 +datum=WGS84 +units=degrees";

        DotSpatial.Projections.ProjectionInfo src = DotSpatial.Projections.ProjectionInfo.FromProj4String(wgs84);
        DotSpatial.Projections.ProjectionInfo trg = DotSpatial.Projections.ProjectionInfo.FromProj4String(grs80);
        
        DotSpatial.Projections.Reproject.ReprojectPoints(geometryXY, z, src, trg, 0, geometryXY.Length / 2);
        Debug.LogFormat("output RD New p{0} = {1} {2}", 0, geometryXY[0], geometryXY[1]);
        utmkToWorldPostion(geometryXY);
    }

    // utm-k xy좌표에서 Unity World 좌표로 변환
    private void utmkToWorldPostion(double[] utmkXY)
    {
        int count = utmkXY.Length;

        Vector3[] worldPosition = new Vector3[count / 2];
        int worldCount = 0;
        for (int i = 0; i < count; i++)
        {
            worldPosition[worldCount].y = 1000f;
            if (i % 2 == 0)
            {
                worldPosition[worldCount].x = (float)(utmkXY[i]) % 100000;
            }
            else
            {
                worldPosition[worldCount].z = (float)(utmkXY[i]) % 100000;
                worldCount++;
            }
        }
        SelectGroundRay(worldPosition);
    }

    // 하늘에서 쏘는 RayCast
    private void SelectGroundRay(Vector3[] Positions)
    {
        RaycastHit hit;
        for (int i = 0; i < Positions.Length; i++)
        {
            if (Physics.Raycast(Positions[i], Vector3.down, out hit, Mathf.Infinity))
            {
                Positions[i].y = hit.point.y + 0.05f;
            }           
        }

        DrawLine(Positions);
    }
    
    // Line Renderer로 Line 그리기
    private void DrawLine(Vector3[] Positions)
    {
        lr.positionCount = Positions.Length;

        for (int i = 0; i < Positions.Length; i++)
        {
            lr.SetPosition(i, Positions[i]);
        }
    }
}
