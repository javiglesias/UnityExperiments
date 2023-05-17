using System.Collections.Generic;
using System;
using UnityEditor;

class BuildEditorScript
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "OnlyWantedBeer";
    static string TARGET_DIR = "Win64";

    [MenuItem("Custom/CI/Build Win64")]
    static void Build()
    {
        string target_dir = APP_NAME + ".exe";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        var res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        // test
    }
}