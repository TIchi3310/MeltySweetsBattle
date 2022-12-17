using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private GameObject[] _character;
    private int _selected = 0;
    void Start()
    {
        _selected = PlayerPrefs.GetInt("choco", 0);
        _character[_selected].SetActive(true);
    }
    public void Right()
    {
        _character[_selected].SetActive(false);
        _selected++;
        if (_selected >= _character.Length)
            _selected = 0;
        _character[_selected].SetActive(true);
    }
    public void Left()
    {
        _character[_selected].SetActive(false);
        _selected--;
        if (_selected < 0)
            _selected = _character.Length - 1;
        _character[_selected].SetActive(true);
    }
    public void SelectCharacter()
    {
        PlayerPrefs.SetInt("CharacterSelect", _selected);
        SceneManager.LoadScene("SampleScene");
    }
}