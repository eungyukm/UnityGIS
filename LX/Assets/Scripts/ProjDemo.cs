using UnityEngine;

public class ProjDemo : MonoBehaviour
{
    void Start()
    {
        double[] x1 = {1387403.7918629837};
        double[] y1 = {1924997.6595098642};
        
        double[] x2 = {37.243260725805115};
        double[] y2 = {131.86686355139034};
        
        double[] xy1 = new double[2] {x1[0], y1[0]};
        double[] xy2 = new double[2] {y2[0], x2[0]};
        double[] z = {0};
        
        string grs80 =
            "+proj=tmerc +lat_0=38 +lon_0=127.5 +k=0.9996 +x_0=1000000 +y_0=2000000 +ellps=GRS80 +units=m +no_defs";
        string wgs84 = "+title=WGS 84 (long/lat) +proj=longlat +ellps=WGS84 +datum=WGS84 +units=degrees";

        DotSpatial.Projections.ProjectionInfo src = 
            DotSpatial.Projections.ProjectionInfo.FromProj4String(wgs84);
        DotSpatial.Projections.ProjectionInfo trg = 
            DotSpatial.Projections.ProjectionInfo.FromProj4String(grs80);
        
        Debug.Log("xy 0 : " + xy1[0]);
        Debug.Log("xy 1 : " + xy1[1]);
        DotSpatial.Projections.Reproject.ReprojectPoints(xy1, z, trg, src, 0, x1.Length);
        Debug.LogFormat("output RD New p{0} = {1} {2}", 0, xy1[0], xy1[0 + 1]);
        
        Debug.Log("xy 0 : " + xy2[0]);
        Debug.Log("xy 1 : " + xy2[1]);
        DotSpatial.Projections.Reproject.ReprojectPoints(xy2, z, src, trg, 0, x1.Length);
        Debug.LogFormat("output RD New p{0} = {1} {2}", 0, xy2[0], xy2[1]);
    }
}