using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using System.Reflection;

public class PreBuilder : IPreprocessBuild
{
    public Texture2D m4399Icon = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Sprites/M4399/icon_4399.png");

    int IOrderedCallback.callbackOrder
    {
        get
        {
            return 0;
        }
    }

    void IPreprocessBuild.OnPreprocessBuild(BuildTarget target, string path)
    {

        if (Application.identifier == "com.tykj.jetpaper.m4399")
        {
            SetIcon(m4399Icon);
        }
        else
        {
            SetIcon(null);
        }
    }

    void SetIcon(Texture2D tex)
    {
        Texture2D[] icons = new Texture2D[1]
        {
            tex
        };
        PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Android, icons);
    }
}
