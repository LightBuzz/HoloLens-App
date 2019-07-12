using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Logger : MonoBehaviour
{
    public static Logger instance;

    private static int id = 0;

    private Text uiText;

    private void Awake()
    {
        uiText = GetComponent<Text>();
        instance = this;
    }

    private void ShowLog(string text)
    {
        string newText = text + Environment.NewLine + uiText.text;

        if (uiText != null)
            uiText.text = newText;
    }

    public static void Log(string text)
    {
        if(instance != null)
        {
            instance.ShowLog(text);
        }
        
        id++;

        Debug.Log(text);
    }

    public static void Info(string text)
    {
        Log("<color=#ffffffff>" + id.ToString() + ":" + text + "</color>");
    }

    public static void Warning(string text)
    {
        Log("<color=#ffa500ff>" + id.ToString() + ":" + text + "</color>");
    }

    public static void Error(string text)
    {
        Log("<color=#a52a2aff>" + id.ToString() + ":" + text + "</color>");
    }
}