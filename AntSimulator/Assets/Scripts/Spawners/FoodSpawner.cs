using UnityEngine;

namespace Spawners
{
    public class FoodSpawner : MonoBehaviour
    {
        public GameObject foodPrefab; // Assign your food prefab in the inspector
        public int numberOfFoodItems = 10; // Number of food items to spawn

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
            SpawnFood();
        }

        private void SpawnFood()
        {
            for (var i = 0; i < numberOfFoodItems; i++)
            {
                var spawnPosition = GetRandomPositionInView();
                Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            }
        }

        private Vector3 GetRandomPositionInView()
        {
            var randomPosition = new Vector3(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                _mainCamera.nearClipPlane
            );

            var worldPosition = _mainCamera.ViewportToWorldPoint(randomPosition);
            return worldPosition;
        }
    }
}