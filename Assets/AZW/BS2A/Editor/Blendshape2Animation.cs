using UnityEngine;
using UnityEditor;

public class Blendshape2Animation : Editor
{
    const string DIR = "Assets/AZW/BS2A/GeneratedAnimations/";
    [UnityEditor.MenuItem("Tools/AZW/Blendshape2Animation")]
    public static void CreateAnim()
    {
        AssetDatabase.CreateFolder("Assets/AZW/BS2A", "GeneratedAnimations");

        var go = Selection.activeGameObject;
        var mesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        for (var i = 0; i <  mesh.blendShapeCount; i++)
        {
            var name = mesh.GetBlendShapeName(i);
            AnimationClip clip = new AnimationClip();
            AnimationUtility.SetEditorCurve(clip, EditorCurveBinding.FloatCurve(go.name, typeof(SkinnedMeshRenderer), "blendShape." + name), AnimationCurve.Linear(0, 0, 1, 1));
            AssetDatabase.CreateAsset(clip, DIR + name + ".anim");
        }
    }
}