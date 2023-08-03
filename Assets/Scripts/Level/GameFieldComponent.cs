using UnityEngine;

namespace TestAssignment.Level
{
    public class GameFieldComponent : MonoBehaviour
    {
        public Transform plane;

        public Renderer planeRenderer;

        public Transform rightWall;
        public Transform leftWall;
        public Transform topWall;
        public Transform bottomWall;

        public void SetSize(int x, int y)
        {
            plane.localScale = new Vector3(x + 1, 1, y + 1);
            planeRenderer.material.mainTextureScale = new Vector2(x / 2 + 1, y / 2 + 1);

            rightWall.localPosition = Vector3.right * (x / 2 + 1);
            rightWall.localScale = new Vector3(1, 1, y + 3);

            leftWall.localPosition = Vector3.left * (x / 2 + 1);
            leftWall.localScale = new Vector3(1, 1, y + 3);

            topWall.localPosition = Vector3.forward * (y / 2 + 1);
            topWall.localScale = new Vector3(x + 3, 1, 1);

            bottomWall.localPosition = Vector3.back * (y / 2 + 1);
            bottomWall.localScale = new Vector3(x + 3, 1, 1);
        }
    }
}