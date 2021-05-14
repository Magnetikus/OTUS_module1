using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

internal sealed class GameController : MonoBehaviour
{
    public Button attackButton;
    public CanvasGroup buttonPanel;
    public CanvasGroup menuPanel;
    public CanvasGroup endGame;
    public GameObject objectForName;
    public Text textEG;
    public Image buttonMenuPanel;
    
        
    public Character[] playerCharacter;
    public Character[] enemyCharacter;
    Character currentTarget;
    bool waitingForInput;

    private PlaySound playSound;

    Character FirstAliveCharacter(Character[] characters)
    {
        return characters.FirstOrDefault(character => !character.IsDead());
    }

    public void Pause()
    {
        if (playSound) playSound.Play("Click");
        Utility.SetCanvasGroupEnabled(menuPanel, true);
        Animation animation = buttonMenuPanel.GetComponent<Animation>();
        animation.Play();
        
    }

    public void Reload()
    {
        if (playSound) playSound.Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Return()
    {
        if (playSound) playSound.Play("Click");
        Utility.SetCanvasGroupEnabled(menuPanel, false);
        
    }

    public void ExitGame()
    {
        if (playSound) playSound.Play("Click");
        SceneManager.LoadScene("MainMenu");
        
    }
    
    public void PlayerWon()
    {
        Debug.Log("Player won.");
        if (playSound) playSound.Play("Win");
        Utility.SetCanvasGroupEnabled(buttonPanel, false);
        Utility.SetCanvasGroupEnabled(menuPanel, false);
        Utility.SetCanvasGroupEnabled(endGame, true);
        Text textEndGame = textEG.GetComponent<Text>();
        Animation animation = textEG.GetComponent<Animation>();
        animation.Play();
        textEndGame.text = "YOU WIN";
        textEndGame.color = Color.red;
        
    }

    public void PlayerLost()
    {
        Debug.Log("Player lost.");
        if (playSound) playSound.Play("Lose");
        Utility.SetCanvasGroupEnabled(buttonPanel, false);
        Utility.SetCanvasGroupEnabled(menuPanel, false);
        Utility.SetCanvasGroupEnabled(endGame, true);
        Text textEndGame = textEG.GetComponent<Text>();
        Animation animation = textEG.GetComponent<Animation>();
        animation.Play();
        textEndGame.text = "YOU LUS";
        textEndGame.color = Color.blue;
        
    }

    bool CheckEndGame()
    {
        if (FirstAliveCharacter(playerCharacter) == null) {
            PlayerLost();
            return true;
        }

        if (FirstAliveCharacter(enemyCharacter) == null) {
            PlayerWon();
            return true;
        }

        return false;
    }

    //[ContextMenu("Player Attack")]
    public void PlayerAttack()
    {
        if (playSound) playSound.Play("Click");
        waitingForInput = false;
    }

    //[ContextMenu("Next Target")]
    public void NextTarget()
    {
        if (playSound) playSound.Play("Click");
        int index = Array.IndexOf(enemyCharacter, currentTarget);
        for (int i = 1; i < enemyCharacter.Length; i++)
        {
            int next = (index + i) % enemyCharacter.Length;
            if (!enemyCharacter[next].IsDead()) {
                currentTarget.targetIndicator.gameObject.SetActive(false);
                currentTarget = enemyCharacter[next];
                currentTarget.targetIndicator.gameObject.SetActive(true);
                return;
            }
        }
    }

    IEnumerator GameLoop()
    {
        yield return null;
        while (!CheckEndGame()) {
            foreach (var player in playerCharacter)
            {
                if (player.IsDead())
                {
                    continue;
                }
                currentTarget = FirstAliveCharacter(enemyCharacter);
                if (currentTarget == null)
                    break;

                currentTarget.targetIndicator.gameObject.SetActive(true);
                Utility.SetCanvasGroupEnabled(buttonPanel, true);

                waitingForInput = true;
                while (waitingForInput)
                    yield return null;

                Utility.SetCanvasGroupEnabled(buttonPanel, false);
                currentTarget.targetIndicator.gameObject.SetActive(false);

                player.target = currentTarget.transform;
                player.AttackEnemy();

                while (!player.IsIdle())
                    yield return null;

                break;
            }

            foreach (var enemy in enemyCharacter)
            {
                if (enemy.IsDead())
                {
                    continue;
                }
                Character target = FirstAliveCharacter(playerCharacter);
                if (target == null)
                    break;

                enemy.target = target.transform;
                enemy.AttackEnemy();

                while (!enemy.IsIdle())
                    yield return null;

                break;
            }
        }
    }
    
    void Start()
    {
        attackButton.onClick.AddListener(PlayerAttack);
        playSound = GetComponent<PlaySound>();
        Utility.SetCanvasGroupEnabled(buttonPanel, false);
        Utility.SetCanvasGroupEnabled(menuPanel, false);
        Utility.SetCanvasGroupEnabled(endGame, false);
        StartCoroutine(GameLoop());
        
        
        
    }
}