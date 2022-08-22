using System;
using System.Collections;
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

        ConvertToKcode(1387599.176629074, 1924687.6551367077);
    }
    
    // 
    private void ConvertToKcode(double width, double height)
    {
        string firstKCode;
        string secondKcode;

        int wp = (int)((width % 100000) / 10);
        int hp = (int)((height % 100000) / 10);

        int firstNumber = (int)width / 100000;
        int secondeNumber = (int) height / 100000;

        firstKCode = widthDictionary[firstNumber];
        secondKcode = heightDictionary[secondeNumber];

        string kCode = firstKCode + secondKcode;
        
        Debug.Log(kCode);
    }
}
