using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
   public AudioMixer AudioMixer;
   public TMP_Dropdown resolutionDropdown;

   private Resolution[] _resolutions;

   private void Start()
   {
      _resolutions = Screen.resolutions;
      resolutionDropdown.ClearOptions();
      List<string> options = new List<string>();
      int currentRessolutionIndex = 0;
      for (int i = 0; i < _resolutions.Length; i++)
      {
         string opstion = _resolutions[i].width + " x " + _resolutions[i].height;
         options.Add(opstion);

         if (_resolutions[i].width == Screen.currentResolution.width &&
             _resolutions[i].height == Screen.currentResolution.height)
         {
            currentRessolutionIndex = i;
         }
      }
      resolutionDropdown.AddOptions(options);
      resolutionDropdown.value = currentRessolutionIndex;
      resolutionDropdown.RefreshShownValue();
   }

   public void SetVolume(float volume)
   {
      AudioMixer.SetFloat("volume", volume);
   }

   public void SetFullscreen(bool isFullscreen)
   {
      Screen.fullScreen = isFullscreen;
   }

   public void SetResolution(int resolutionIndex)
   {
      Resolution resolution = _resolutions[resolutionIndex];
      Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
   }
}
