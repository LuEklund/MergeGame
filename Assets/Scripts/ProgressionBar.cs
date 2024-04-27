using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressionbar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void updateSlider(float current, float maximum)
    {
        slider.value = current / maximum;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
