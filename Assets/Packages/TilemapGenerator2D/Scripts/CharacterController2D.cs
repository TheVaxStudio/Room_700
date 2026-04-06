namespace DevelopersHub.ProceduralTilemapGenerator2D.Tools
{
    using UnityEngine;

    public class CharacterController2D : MonoBehaviour
    {

        [SerializeField] private Camera _camera = null;
        [SerializeField] private float _cameraSize = 5;
        [SerializeField] private float _moveSpeed = 2f;
        private Rigidbody2D _rigidbody = null;
    }
}