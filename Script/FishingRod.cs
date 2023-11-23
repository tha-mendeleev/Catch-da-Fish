using System;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public static FishingRod Instance;

    [SerializeField] float fishingDept = 3f;
    [SerializeField] float fishingSpeed = 1f;
    [SerializeField] float maxPower = 1f;
    [SerializeField] Transform fishParentGO;

    private bool _fishing = false;
    private Vector3 _originPos;
    private bool up = false;
    private bool _caught = false;
    private Fish _fish;

    private void Start()
    {
        GetComponentInParent<BoatController>().Fishing += Fishing;
        _originPos = transform.localPosition;
        Instance = this;
    }

    private void Update()
    {
        if (_fishing || up)
        {
            if (transform.localPosition.y > _originPos.y - fishingDept && !up)
            {
                Down();
            }
            else
            {
                up = true;
                Up();
            }
        }
    }

    private void Down()
    {
        transform.localPosition = transform.localPosition + new Vector3(0, -fishingSpeed * Time.deltaTime);
    }

    private void Up()
    {
        if (transform.localPosition.y < _originPos.y)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, (fishingSpeed * 1.5f) * Time.deltaTime);
        }
        else Done();
    }

    private void Done()
    {
        _fishing = false;
        up = false;
        if (_caught && _fish)
        {
            GetComponentInParent<Boat>().AddFish(_fish.Weight);
            FishSpawner.RemoveFish();
            Destroy(_fish.gameObject);
            _caught = false;
            _fish = null;
            AudioManager.Instance.PlayClip(Clip.Caught);
        }
    }

    private void Fishing(object sender, EventArgs e)
    {
        if (!_fishing)
        {
            _fishing = true;
            AudioManager.Instance.PlayClip(Clip.Fishing);
        }
    }

    public void ResetDefault()
    {
        _fishing = false;
        transform.localPosition = _originPos;
        up = false;
        _caught = false;
        _fish = null;
    }

    public void FishEscape()
    {
        // fishing = false => not able to get another fish
        if (!_fish) return;
        _fishing = false;
        _caught = false;
        _fish.transform.SetParent(fishParentGO);
        _fish = null;
        AudioManager.Instance.PlayClip(Clip.Escaped);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ISeaItem item) && !_caught && _fishing)
        {
            _caught = true;
            up = true;
            collision.transform.SetParent(transform);
            item.Caught(maxPower);
            if (item is Fish)
            {
                _fish = item as Fish;
            }
        }
    }

    public void UpPower(float power)
    {
        maxPower += power;
    }

    public void UpDept(float upDept)
    {
        fishingDept += upDept;
    }

    public void UpSpeed(float speed)
    {
        fishingSpeed += speed;
    }
}
