using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Text warningText;
    public float restartDelay = 5f;

    private Animator _anim;
    private float _restartTimer;
    private static readonly int GameOver = Animator.StringToHash("GameOver");
    private static readonly int Warning = Animator.StringToHash("Warning");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            _anim.SetTrigger(GameOver);

            _restartTimer += Time.deltaTime;

            if (_restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = $"{Mathf.RoundToInt(enemyDistance)} M!";
        _anim.SetTrigger(Warning);
    }
}