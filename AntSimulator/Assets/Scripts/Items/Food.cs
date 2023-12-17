using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private FoodTemplate foodTemplate;
        public float energy; // Energy content of the food

        private void Start()
        {
            GetComponent<SpriteRenderer>().color = foodTemplate.color;
            energy = foodTemplate.energy;
        }
    }
}