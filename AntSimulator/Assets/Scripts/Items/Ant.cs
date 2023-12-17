using System.Collections;
using Items;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [Header("Movement Settings")] [SerializeField]
    private float maxSpeed = 2;

    [SerializeField] private float defaultRotationSpeed = 120;
    [SerializeField] private float foodRotationSpeed = 100;
    [SerializeField] private float steerStrength = 0.1f;
    [SerializeField] private float wanderStrength = 0.2f;

    [Header("Food Settings")] [SerializeField]
    private LayerMask foodLayer; // Layer for the food objects

    [SerializeField] private float detectionRadius = 1f; // Radius in which food is detected
    [SerializeField] private float foodEnergyStored;

    [Header("Pheromone Settings")] [SerializeField]
    private GameObject pheromoneFoodFound;

    [SerializeField] private GameObject pheromoneHome;

    private Vector2 _currentDirection;

    private Vector2 _desiredDirection;
    private bool _isCarryingFood;
    private float _rotationSpeed;

    private void Start()
    {
        _desiredDirection = Random.insideUnitCircle.normalized;
        _currentDirection = _desiredDirection;
        _rotationSpeed = defaultRotationSpeed;
        StartCoroutine(ChangeDirectionRoutine());
        StartCoroutine(ProducePheromone());
    }

    private void Update()
    {
        SenseFood();
        RotateTowardsDesiredDirection();
        Move();
    }

    // Optional: Visualize the detection area in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            var food = other.GetComponent<Food>();
            foodEnergyStored += food.energy;
            _isCarryingFood = true;
            Destroy(other.gameObject);
        }
    }

    private void SenseFood()
    {
        var food = Physics2D.OverlapCircle(transform.position, detectionRadius, foodLayer);
        if (!food)
        {
            _rotationSpeed = defaultRotationSpeed;
            return;
        }

        _rotationSpeed = foodRotationSpeed;
        Vector2 directionToFood = food.transform.position - transform.position;
        directionToFood += Random.insideUnitCircle * 0.1f;
        _desiredDirection = directionToFood.normalized;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f)); // Change direction at random intervals
            if (!Physics2D.OverlapCircle(transform.position, detectionRadius, foodLayer))
                // Only change direction if no food is detected
                _desiredDirection = Random.insideUnitCircle.normalized * wanderStrength;
        }
    }

    private void RotateTowardsDesiredDirection()
    {
        _currentDirection = Vector2.Lerp(_currentDirection, _desiredDirection, Time.deltaTime * steerStrength);
        _currentDirection.Normalize();

        var angle = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg - 90f;
        var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Rotate at a constant rate
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        var transform1 = transform;
        transform1.position += transform1.up * (maxSpeed * Time.deltaTime);
    }

    private IEnumerator ProducePheromone()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            Instantiate(_isCarryingFood ? pheromoneFoodFound : pheromoneHome, transform.position, Quaternion.identity);
        }
    }
}