using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pheromone", menuName = "Pheromone", order = 0)]
    public class PheromoneTemplate : ScriptableObject
    {
        public Color color = Color.blue;
        public float strength = 1f;
        public float decayRate = 0.1f;
        public PheromoneType type;
    }

    public enum PheromoneType
    {
        Home,
        FoodFound
    }
}