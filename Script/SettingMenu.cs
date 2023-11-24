using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Toggle themeToggler, effectToggler;

    public void SetMasterVolume()
    {
        AudioManager.Instance.SetMasterVolume(masterVolumeSlider.value);
    }

    public void ToggleSoundEffect()
    {
        AudioManager.Instance.ToggleSoundEffect(effectToggler.isOn);
    }

    public void ToggleThemeMusic()
    {
        AudioManager.Instance.ToggleThemeMusic(themeToggler.isOn);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
