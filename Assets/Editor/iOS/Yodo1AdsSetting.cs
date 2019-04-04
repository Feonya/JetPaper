using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

#endif

using System.IO;

public static class Yodo1AdsSetting
{
    [PostProcessBuildAttribute(100)]
    private static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target != BuildTarget.iOS)
        {
            Debug.LogWarning("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        string _projPath = PBXProject.GetPBXProjectPath(pathToBuildProject);
        PBXProject _pbxProj = new PBXProject();
        Debug.Log("_projPath:" + _projPath);
        _pbxProj.ReadFromString(File.ReadAllText(_projPath));
        string _targetGuid = _pbxProj.TargetGuidByName("Unity-iPhone");

        //*******************************设置buildsetting*******************************//
        _pbxProj.AddBuildProperty(_targetGuid, "OTHER_LDFLAGS", "-ObjC");

        File.WriteAllText(_projPath, _pbxProj.WriteToString());
    }
}