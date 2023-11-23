using TMPro;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] float capacity = 10f;
    [SerializeField] float speed = 1f;
    [SerializeField] TextMeshProUGUI fuelText, scaleText, capacityText;
    public bool Sink = false;
    public static Boat Instance;

    private BoatController _movement;
    private float fuel = 120f;
    private float _moveSpeed;
    private float _currentWeight = 0;
    private Vector3 _originPos;
    private float _sinkInterval = 0, _maxSinkInterval = 1.5f;
    private bool _sinking = false;
    private int _viewScreenSize = 20;

    void Start()
    {
        _moveSpeed = speed;
        _originPos = transform.position;
        _movement = GetComponent<BoatController>();
        fuelText.text = $"Fuel: {fuel:0.00} L";
        scaleText.text = $"Weight: {_currentWeight:0.00} Kg";
        capacityText.text = $"Capacity: {capacity:0.00} Kg";
        Instance = this;
    }

    void Update()
    {
        if (Sink)
        {
            SinkBoat();
            return;
        }
        if (_movement.IsMoving)
        {
            if (fuel > 0)
            {
                ClampPostion();
                transform.position = transform.position + _movement.Direction * _moveSpeed * Time.deltaTime * Vector3.right;
                fuel -= Time.deltaTime;
                fuelText.text = $"Fuel: {fuel:0.00} L";
            }
            if (fuel <= 0)
            {
                fuel = 0;
                Stop();
            }

        } else
        {
            if (fuel >= 20) return;
            fuel += .25f * Time.deltaTime;
            fuelText.text = $"Fuel: {fuel:0.00} L";
        }
    }

    private void ClampPostion()
    {
        if (transform.position.x + .5f * _movement.Direction < -_viewScreenSize || transform.position.x + .5f * _movement.Direction > _viewScreenSize)
        {

            Stop();
        }
        else
        {
            _moveSpeed = speed;
            CameraFollowBoat();
        }
    }

    private void Stop()
    {
        _moveSpeed = 0;
        _movement.Direction = 0;
        _movement.IsMoving = false;
    }

    private void CameraFollowBoat()
    {
        if (!_movement.IsMoving ||
            transform.position.x - 2f < -_viewScreenSize ||
             transform.position.x + 2f > _viewScreenSize) return;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + _moveSpeed * _movement.Direction * Vector3.right, _moveSpeed * .5f * Time.deltaTime);

    }

    public void AddFish(float weight)
    {
        _currentWeight += weight;

        if (_currentWeight > capacity)
        {
            _originPos = transform.position;
            _currentWeight = 0;
            Sink = true;
            Stop();
        }
        else
        {
            scaleText.text = $"Weight: {_currentWeight:0.00} Kg";
        }
    }

    private void SinkBoat()
    {
        if (_sinkInterval < _maxSinkInterval)
        {
            if (!_sinking)
            {
                _originPos = transform.position;
                _sinking = true;
                AudioManager.Instance.PlayClip(Clip.Sink);
            }
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(0, -1.5f, 0), 2f * Time.deltaTime);
            _sinkInterval += Time.deltaTime;
        }
        else
        {
            _sinkInterval = 0;
            Response();
        }
    }

    public void Response()
    {
        Sink = false;
        _sinking = false;
        fuel = 10f;
        _currentWeight = 0f;
        fuelText.text = $"Fuel: {fuel:0.00} L";
        scaleText.text = $"Weight: {_currentWeight:0.00} Kg";
        capacityText.text = $"Capacity: {capacity:0.00} Kg";
        transform.position = _originPos;
        GetComponentInChildren<FishingRod>().ResetDefault();
        Stop();
    }

    public void UpgradeBoatSpeed(float upSpeed) {
        speed += upSpeed;
    }

    public void UpgradeBoatCapacity(float upCapacity) {
        capacity += upCapacity;
        capacityText.text = $"Capacity: {capacity:0.00} Kg";
    }

    public void AddFuel(float upFuel)
    {
        fuel += upFuel;
        fuelText.text = $"Fuel: {fuel:0.00} L";
    }

    public float CurrentWeight()
    {
        return _currentWeight;
    }

    public void CutWeight(float weight)
    {
        _currentWeight -= weight;
        scaleText.text = $"Weight: {_currentWeight:0.00} Kg";
        AudioManager.Instance.PlayClip(Clip.Buy);
    }

    public float Capacity()
    {
        return capacity;
    }
}
