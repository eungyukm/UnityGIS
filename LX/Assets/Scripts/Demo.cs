using UnityEngine;

public class Demo : MonoBehaviour
{
    private LineRenderer lr;

    public Transform[] Positions;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = Positions.Length;
        lr.SetPosition(0, Positions[0].position);
        lr.SetPosition(1, Positions[1].position);
        lr.SetPosition(2, Positions[2].position);
    }
}
