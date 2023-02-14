using UnityEngine;

namespace BeresnevTest.GameLogic
{
    public class MovementClamper
    {
        private readonly float _leftBoundary;
        private readonly float _rightBoundary;

        public MovementClamper(float leftBoundary, float rightBoundary)
        {
            _leftBoundary = leftBoundary;
            _rightBoundary = rightBoundary;
        }
        
        public Vector2 Clamp(Vector2 position, Vector3 extents)
        {
            return new Vector2(Mathf.Clamp(position.x, 
                _leftBoundary + extents.x, 
                _rightBoundary - extents.x), position.y);
        }
    }
}