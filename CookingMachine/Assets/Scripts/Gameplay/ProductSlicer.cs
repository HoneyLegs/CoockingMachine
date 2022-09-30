using Data;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay {

    public class ProductSlicer : GameScreen {

        [SerializeField]
        private SlicedProduct _slicedProduct;

        [SerializeField]
        private SlicedProduct _pieceOfProduct;

        private Vector2 _startPosition;

        [SerializeField]
        private Text _weightsLabel;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private GameObject _knife;

        [SerializeField]
        private float knifeSpeed;

        private bool _stopKnife;

        private Vector2 _knifeDirection = Vector2.right;

        private float _leftmostKnifePositionX;
        private float _rightmostKnifePositionX;
        private float _fullKnifeDistance;

        private void Awake() {
            _startPosition = _slicedProduct.transform.position;
        }

        public void Init(ProductData productData) {
            _slicedProduct.Init(productData);
            _pieceOfProduct.Init(productData);
            Init();
        }

        [Button]
        private void Init() {
            _slicedProduct.transform.position = _startPosition;
            _pieceOfProduct.transform.position = _startPosition;
           // _weightsLabel.text = _slicedProduct.GetWeight().ToString();
            _leftmostKnifePositionX = _slicedProduct.transform.position.x - _slicedProduct.transform.localScale.x / 2;
            _rightmostKnifePositionX = _slicedProduct.transform.position.x + _slicedProduct.transform.localScale.x / 2;
            _fullKnifeDistance = (_rightmostKnifePositionX - _leftmostKnifePositionX) ;
            _pieceOfProduct.gameObject.SetActive(false);
            _stopKnife = false;
        }

        private void OnEnable() {
            _updateEventListener.ActionsToDo += BehaviourUpdate;
        }

        private void OnDisable() {
            _updateEventListener.ActionsToDo -= BehaviourUpdate;
        }

        private float PositionToPercent(float position) {
            return (position - _leftmostKnifePositionX) * 100 / _fullKnifeDistance;
        }

        private void BehaviourUpdate() {
            MoveKnife();
        }

        private void MoveKnife() {
            if (_stopKnife) {
                return;
            }
            TryToChangeDirection();
            _knife.transform.Translate(_knifeDirection * Time.deltaTime);
        }

        private void TryToChangeDirection() {
            if ((_knifeDirection == Vector2.right && _knife.transform.position.x >= _rightmostKnifePositionX) ||
                (_knifeDirection == Vector2.left && _knife.transform.position.x <= _leftmostKnifePositionX)) {
                _knifeDirection *= -1;
            }
        }

        [Button]
        private void Cut() {
            var knifePositionX = _knife.transform.position.x;
            var cutProcent = PositionToPercent(knifePositionX);
            if(cutProcent > _slicedProduct.ProductPercent) {
                return;
            }
            _slicedProduct.ProductPercent = cutProcent;
          //  _pieceOfProduct.gameObject.SetActive(true);
            _pieceOfProduct.ProductPercent = 100 - cutProcent;
            _rightmostKnifePositionX = knifePositionX;
            _stopKnife = true;
        }
    }
}
