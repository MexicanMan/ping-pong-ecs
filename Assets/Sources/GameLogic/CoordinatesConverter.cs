using UnityEngine;

namespace BeresnevTest.GameLogic
{
    public class CoordinatesConverter
    {
        private Camera _camera;

        public CoordinatesConverter(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 ConvertFromScreenToWorldPlane(Vector2 screenCoords)
        {
            return _camera.ScreenToWorldPoint(
                new Vector3(screenCoords.x, screenCoords.y, -_camera.transform.position.y));
        }
    }
}