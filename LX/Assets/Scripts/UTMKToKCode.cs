using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UTMK 코드를 국가지점번호로 변환하는 로직
/// </summary>
public class UTMKToKCode : MonoBehaviour
{
    private Dictionary<int, string> widthDictionary = new Dictionary<int, string>();
    private Dictionary<int, string> heightDictionary = new Dictionary<int, string>();

    private void Start()
    {
        widthDictionary = new Dictionary<int, string>()
        {
            {7, "가"},
            {8, "나"},
            {9, "다"},
            {10, "라"},
            {11, "마"},
            {12, "바"},
            {13, "사"},
        };

        heightDictionary = new Dictionary<int, string>()
        {
            {13, "가"},
            {14, "나"},
            {15, "다"},
            {16, "라"},
            {17, "마"},
            {18, "바"},
            {19, "사"},
            {20, "아"},
        };
        // 100km = 100000m
        ConvertToKcode(1387599.176629074, 1924687.6551367077);
    }
    
    // UTM-K width와 height를 입력하면, 국가 지정 좌표로 변환하여 출력하는 로직
    public void ConvertToKcode(double width, double height)
    {
        string firstKCode;
        string secondKcode;

        int firstNumber = (int)width / 100000;
        int secondNumber = (int) height / 100000;

        if (firstNumber >= 7 && firstNumber <= 13)
        {
            firstKCode = widthDictionary[firstNumber];
        }
        else
        {
            Debug.LogError("국가지점번호의 입력 범위가 아닙니다.");
            return;
        }

        if (secondNumber >= 13 && secondNumber <= 20)
        {
            secondKcode = heightDictionary[secondNumber];
        }
        else
        {
            Debug.LogError("국가지점번호의 입력 범위가 아닙니다.");
            return;
        }
        int wp = (int)((width % 100000) / 10);
        int hp = (int)((height % 100000) / 10);

        string kCode = firstKCode + " "+ secondKcode + " " + wp.ToString() + " " + hp.ToString();
        // Debug.Log(kCode);
    }
    
    // 사사 구역일 경우, 국가 지정 좌표로 변환
    public void ConvertToKcodeInSaSa(double width, double height)
    {
        int wp = (int)((width % 100000) / 10);
        int hp = (int)((height % 100000) / 10);

        string kCode = "사" + " "+ "사" + " " + wp.ToString() + " " + hp.ToString();
        // Debug.Log(kCode);
    }
}
