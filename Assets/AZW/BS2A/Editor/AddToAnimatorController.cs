using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.Animations;
using System.Text.RegularExpressions;

namespace AZW.VRCUnityTools
{
    public class AddToAnimatorController : EditorWindow
    {
        [SerializeField]
        string prefix = "facial_";

        [SerializeField]
        AnimatorController animator;
        
        [SerializeField]
        AnimationClip[] animations;

        [SerializeField]
        AnimatorLayerBlendingMode blendingMode = AnimatorLayerBlendingMode.Additive;

        [SerializeField]
        bool writeDefault = false;

        Vector2 scrollPos = Vector2.zero;

        [MenuItem("Tools/AZW/Add animations to an animation layer")]
        public static void Init()
        {
            GetWindow(typeof(AddToAnimatorController)).Show();
        }

        public static void AddAnimatorController()
        {

        }
        void OnGUI()
        {
            using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
            {
                scrollPos = scrollView.scrollPosition;

                var so = new SerializedObject(this);
                so.Update();
                GUILayout.Label("Target AnimatorController", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(so.FindProperty("animator"), GUIContent.none, true);
                GUILayout.Space(8);
                GUILayout.Label("Animations to be added", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(so.FindProperty("animations"), GUIContent.none, true);

                GUILayout.Space(17);
                GUILayout.Label("Congfigurations", EditorStyles.boldLabel);

                GUILayout.Label("Prefix of the parameters and the layers");
                EditorGUILayout.PropertyField(so.FindProperty("prefix"), GUIContent.none, true);
                GUILayout.Space(8);
                GUILayout.Label("Blending mode of animation layers");
                EditorGUILayout.PropertyField(so.FindProperty("blendingMode"), GUIContent.none, true);
                GUILayout.Space(8);
                EditorGUILayout.PropertyField(so.FindProperty("writeDefault"), new GUIContent("Enable write default"), true);
                so.ApplyModifiedProperties();


                if (GUILayout.Button("Run"))
                {
                    foreach (var anim in animations)
                    {
                        var name = prefix + anim.name;

                        animator.AddParameter(name, AnimatorControllerParameterType.Float);

                        var layer = new AnimatorControllerLayer();
                        layer.name = name;
                        layer.blendingMode = blendingMode;
                        layer.defaultWeight = 1;

                        var stateMachine = new AnimatorStateMachine();

                        var blendshapeState = stateMachine.AddState(name.Replace(".", "")); // '.' is not allowed in State name
                        blendshapeState.writeDefaultValues = writeDefault;
                        blendshapeState.motion = anim;
                        blendshapeState.timeParameter = name;
                        blendshapeState.timeParameterActive = true;


                        layer.stateMachine = stateMachine;

                        animator.AddLayer(layer);
                    }
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}