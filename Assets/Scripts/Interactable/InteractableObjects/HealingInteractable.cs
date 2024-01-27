using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingInteractable : MonoBehaviour, Interactable
{
    private Player player;
    public GameController game;
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        player = game.currentPlayer;
        player.HealCharacter(player.maxHealth);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));   
    }
}
