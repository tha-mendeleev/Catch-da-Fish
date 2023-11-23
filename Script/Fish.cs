using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour, ISeaItem
{

    [SerializeField] float speed = 1f;
    public float Weight;
    private Vector3 _target;
    private bool _caught = false;
    private readonly float _leftMostX = -20f, _rightMostX = 20f;
    private readonly float _bottomMostY = -5.7f, _upMostY = 1.3f;

    private void Start()
    {
        transform.position = InstantiateRandomPoint();
        float minWid, maxWid;
        float midDept = (_bottomMostY + _upMostY) * .5f;
        minWid = transform.position.y <= midDept ? .75f : .5f;
        maxWid = transform.position.y <= midDept ? 2f : 1f;

        Weight = Random.Range(minWid, maxWid);
        transform.localScale = .5f * Weight * Vector3.one; // .5f => sprite smaller x 2
        GetRandomPosition();
    }

    private void Update()
    {
        if (_caught)
        {
            transform.Rotate(360f * Time.deltaTime * Vector3.up);
        }
        else
        {
            var dir = (_target - transform.position).normalized;
            transform.position += speed * Time.deltaTime * dir;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(angle, Vector3.up), 3f * Time.deltaTime);
            if (Vector3.Distance(transform.position, _target) <= .1f)
            {
                GetRandomPosition();
            }
        }
    }

    public void Caught(float rodPower)
    {
        _caught = true;
        AudioManager.Instance.PlayClip(Clip.FishTouch);
        if (rodPower < Weight)
        {
            StartCoroutine(Escape());
        }
    }

    IEnumerator Escape()
    {
        yield return new WaitForSeconds(1f);
        _caught = false;
        GetComponentInParent<FishingRod>().FishEscape();
    }

    private void GetRandomPosition()
    {

        _target = ClampAndGetRandomPoint();
    }
    
    private Vector3 ClampAndGetRandomPoint()
    {
        var x = Random.Range(transform.position.x - 5f, transform.position.x + 5f);
        var y = Random.Range(transform.position.y - 2f, transform.position.y + 2f);
        if (x <= _leftMostX)
        {
            x = Random.Range(_leftMostX, x + 5f);
        } else if (x >= _rightMostX)
        {
            x = Random.Range(x - 4, _rightMostX);
        }
        if (y <= _bottomMostY)
        {
            y = Random.Range(_bottomMostY, y + 3);
        }
        else if (y >= _upMostY)
        {
            y = Random.Range(y - 3, _upMostY);
        }

        return new Vector3(x, y);
    }

    private Vector3 InstantiateRandomPoint()
    {
        float x, y;
        if (Boat.Instance.transform.position.x < 0) // move left -> spawn right
        {
            x = Random.Range(Boat.Instance.transform.position.x + 10, _rightMostX);
        }
        else // move right -> spawn left
        {
            x = Random.Range(_leftMostX, Boat.Instance.transform.position.x - 10);
        }
        y = Random.Range(_bottomMostY + 1, _upMostY - 1);

        return new Vector3(x, y);
    }
}
