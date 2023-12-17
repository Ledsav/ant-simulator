using ScriptableObjects;
using UnityEngine;

namespace Items
{
    public class Pheromone : MonoBehaviour
    {
        [SerializeField] private PheromoneTemplate pheromone;
        public float strength;
        public float decayRate;
        public PheromoneType type;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetColor();
        }

        private void SetColor()
        {
            _spriteRenderer.color = pheromone.color;
        }
    }
}