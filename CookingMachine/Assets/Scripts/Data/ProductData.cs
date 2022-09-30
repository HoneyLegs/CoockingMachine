using UnityEngine;

namespace Data {

    [CreateAssetMenu(fileName = "newProductData", menuName = "ProductData")]
    public class ProductData : ScriptableObject {

        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField]
        private float _scale;
        public float Scale => _scale;

        [SerializeField]
        private float _weight;
        public float Weight => _weight;

        [SerializeField]
        private float _price;
        private float Price => _price;

    }
}

