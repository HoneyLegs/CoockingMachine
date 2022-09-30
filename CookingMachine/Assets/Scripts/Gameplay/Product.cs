using Data;
using UnityEngine;

namespace Gameplay {

    public class Product : MonoBehaviour {

        [SerializeField]
        private ProductData _productData;
        public ProductData ProductData => _productData; 

        public void Init(ProductData productData) {

        }

        public virtual float GetWeight() {
            return _productData.Weight;
        }
    }
}
