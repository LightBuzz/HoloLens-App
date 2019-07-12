using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private Text debugText;

    public static DebugCanvas instance;

    private void Awake()
    {
        instance = this;
    }

    public void Log(string str)
    {
        string previousStr = debugText.text;

        debugText.text = str + Environment.NewLine + previousStr;

        Debug.Log(str);
    }
}
