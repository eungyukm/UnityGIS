using UnityEngine;

public class UTMKReader : MonoBehaviour
{
    public UTMKToKCode UtmkToKCode;

    public Vector3 hitPos = new Vector3();

    // 지면 좌표를 얻어 냅니다. 
    public void GetUTMK()
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if(Physics.Raycast(ray, out hitData))
        {
            // Debug.Log(hitData.point);
            hitPos = hitData.point;
        }
    }
    
    // UTM-K의 좌표를 입력을 받아 국가 지점 좌표로 변환
    public void UTMKToLocationCode()
    {
        UtmkToKCode.ConvertToKcodeInSaSa(hitPos.x, hitPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        GetUTMK();
        UTMKToLocationCode();
    }
}
