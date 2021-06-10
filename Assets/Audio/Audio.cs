using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{


    [RequireComponent(typeof(Slider))]
    public class Audio : MonoBehaviour
    {
        // Creating a private Audiomixer for your audio
        [SerializeField]
        private AudioMixer mixer;
        // Serializing the Volume Parameters that have been exposed creating a private string
        [SerializeField]
        private string volumeParam;

        private Slider slider;

        // Setting up the max and minimum values that the slider can go up to for the audios dB

        // Start is called before the first frame update
        void Start()
        {
            slider = gameObject.GetComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = 1;
            slider.value = PlayerPrefs.GetFloat(volumeParam, 1);
            slider.onValueChanged.AddListener(_value => mixer.SetFloat(volumeParam, Remap(_value, 0, 1, -80, 0)));

        }
        private void OnDestroy()
        {
            PlayerPrefs.SetFloat(volumeParam, slider.value);
            PlayerPrefs.Save();
        }

        // Remaps the values of the volume new max and min, with old min and max.
        private float Remap(float _value, float _oldMin, float _oldMax, float _newMin, float _newMax)
        {
            return (_value - _oldMin) / (_oldMax - _oldMin) * (_newMax - _newMin) + _newMin;
        }
    }
}
