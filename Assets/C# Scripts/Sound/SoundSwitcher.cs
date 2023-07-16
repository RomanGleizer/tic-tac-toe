using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _volumeText;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Sprite _moreThanZeroSoundIcon;
    [SerializeField] private Sprite _zeroSoundIcon;

    private float _volumeValue;

    public float CurrentVolume => _volumeValue;

    private void Awake() 
    {
        if (!PlayerPrefs.HasKey(nameof(_volumeValue)))
        {
            SaveVolumeValue(0);
            LoadVolumeValue();
        }
        else LoadVolumeValue();
    }

    public void ChageSoundValue() 
    {
        _volumeValue = _slider.value;
        _audioSource.volume = _volumeValue;
        _volumeText.text = $"{Mathf.Round(_audioSource.volume * 100)} %";
        _soundImage.sprite = _slider.value == 0 ? _zeroSoundIcon : _moreThanZeroSoundIcon;

        SaveVolumeValue(_slider.value);
    }

    private void LoadVolumeValue()
        => _slider.value = PlayerPrefs.GetFloat(nameof(_volumeValue));

    private void SaveVolumeValue(float value)
        => PlayerPrefs.SetFloat(nameof(_volumeValue), value);
}
