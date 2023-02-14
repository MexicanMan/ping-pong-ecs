using UnityEngine;
using UnityEngine.SceneManagement;

namespace BeresnevTest.Scenes
{
    [CreateAssetMenu(menuName = "Configuration/" + nameof(SceneReference))]
    public class SceneReference : ScriptableObject
    {
        [SerializeField] 
        [Tooltip("The path to the scene from the root of the project. Example: Assets/Scenes/Game.unity")]
        private string _scenePath;

        private Scene _scene;

        public string Path => _scenePath;

        public Scene Scene
        {
            get
            {
                if (!_scene.IsValid())
                    _scene = SceneManager.GetSceneByPath(_scenePath);

                return _scene;
            }
        }
    }
}