using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay {

    public class SlicedProduct : Product {

        [SerializeField]
        [Range(0, 100)]
        [OnValueChanged(nameof(ChangeVisiblePartOfProduct))]
        private float _productPercent = 100;

        public Action onProductPercentChanged = delegate { };

        [SerializeField]
        private GameObject _mask;

        public float ProductPercent {
            get {
                return _productPercent;
            }

            set {
                if (value > 100) {
                    _productPercent = 100;
                } else if (value < 0) {
                    _productPercent = 0;
                } else {
                    _productPercent = value;
                }
                onProductPercentChanged?.Invoke();
            }
        }

        private void Awake() {
            onProductPercentChanged += ChangeVisiblePartOfProduct;
        }

        private void ChangeVisiblePartOfProduct() {
            _mask.transform.localScale = new Vector3((100 - ProductPercent) / 100, _mask.transform.localScale.y, _mask.transform.localScale.z);
        }

        public override float GetWeight() {
            return base.GetWeight() * ProductPercent / 100;
        }
    }
}

