using TMPro;
using UnityEngine;

public class TimeMng : MonoBehaviour
{

    private float mn, sec;
    [SerializeField] TextMeshProUGUI time;

    void Start()
    {
        mn = 0f; sec = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 60f) {
            mn++;
            sec = 0f;
        }
        time.text = $"{mn}:{(sec < 10f ? "0" : "")}{(int) sec}";
    }
}
