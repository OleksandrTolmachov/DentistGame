using TMPro;
using UnityEngine;

public class RegistrationForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private GameObject _panel;
    private GameClient _gameClient;

    public ServerOptions options;

    private void Start()
    {
        _gameClient = new GameClient(options);
        if (PlayerPrefs.HasKey("token")) _panel.SetActive(false);
    }


    public async void TryRegister()
    {
        if (string.IsNullOrWhiteSpace(_username.text) || string.IsNullOrWhiteSpace(_password.text))
            return;

        await _gameClient.Register(_username.text, _password.text);
        _panel.SetActive(false);
    }
}
