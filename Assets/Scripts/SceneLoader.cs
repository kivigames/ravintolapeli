﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class SceneLoader : MonoBehaviour
{
    
  private void LoadLevel(int Level)
  { 
      SceneManager.LoadScene(Level);
  }
}
