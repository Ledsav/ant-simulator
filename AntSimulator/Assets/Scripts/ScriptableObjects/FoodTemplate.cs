using UnityEngine;

namespace ScriptableObjects
{
    // Create a new menu (Assets/Create/Food) in the Unity Editor to create Food assets
    [CreateAssetMenu(fileName = "New Food", menuName = "Food", order = 0)]
    public class FoodTemplate : ScriptableObject
    {
        public float energy = 10f; // Energy content of the food
        public Color color = Color.green; // Color of the food
    }
}