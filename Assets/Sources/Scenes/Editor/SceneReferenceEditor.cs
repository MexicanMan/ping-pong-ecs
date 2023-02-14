using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeresnevTest.Scenes.Editor
{
    public static class SceneReferenceEditorExtensions
    {
        public static string EscapeSlashes(this string path)
        {
            return path.Replace("/", "\u2215");
        }
    }

    [CustomEditor(typeof(SceneReference), true)]
    public class SceneReferenceEditor : UnityEditor.Editor
    {
        private const string NoScenesWarning =
            "There is no scene on the current path, or it is not added to Build Settings";

        private const string EditorMessage =
            "You can select only those scenes that are added in Build Settings";

        private static readonly string[] ExcludedProperties = { "m_Script", "_scenePath" };
        
        private GUIStyle _headerLabelStyle;

        private string[] _sceneList;
        private SceneReference _gameSceneInspected;

        private void OnEnable()
        {
            _gameSceneInspected = target as SceneReference;
            PopulateScenePicker();
            InitializeGuiStyles();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Scene information", _headerLabelStyle);
            EditorGUILayout.Space();
            DrawScenePicker();
            DrawPropertiesExcluding(serializedObject, ExcludedProperties);
        }

        private void DrawScenePicker()
        {
            var sceneName = _gameSceneInspected.Path;
            EditorGUI.BeginChangeCheck();
            var selectedScene = _sceneList.ToList().IndexOf(sceneName);

            EditorGUILayout.HelpBox(EditorMessage, MessageType.Info);

            if (selectedScene < 0)
            {
                EditorGUILayout.HelpBox(NoScenesWarning, MessageType.Warning);
            }

            selectedScene = EditorGUILayout.Popup("Scene", selectedScene,
                _sceneList.Select(s => s.EscapeSlashes()).ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed selected scene");
                var pathProperty = serializedObject.FindProperty("_scenePath");
                pathProperty.stringValue = _sceneList[selectedScene];
                serializedObject.ApplyModifiedProperties();
                Repaint();
            }
        }

        private void InitializeGuiStyles()
        {
            _headerLabelStyle = new GUIStyle(EditorStyles.largeLabel)
            {
                fontStyle = FontStyle.Bold,
                fontSize = 18,
                fixedHeight = 70.0f
            };
        }

        private void PopulateScenePicker()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            _sceneList = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                _sceneList[i] = SceneUtility.GetScenePathByBuildIndex(i);
            }
        }

    }
}