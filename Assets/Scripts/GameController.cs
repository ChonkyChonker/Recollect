using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {FreeRoam, Dialog, Battle}

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject SwordsMan;
    [SerializeField] GameObject GunMan;
    [SerializeField] GameObject Sword;
    public PlayerController currentController;
    public Player currentPlayer;
    private Animator currentAnimator;
    private Animator swordAnimator;

    GameState state;

    public void Start() {
        
        currentController = SwordsMan.GetComponent<PlayerController>();
        currentPlayer = SwordsMan.GetComponent<Player>();
        currentAnimator = SwordsMan.GetComponent<Animator>();
        swordAnimator = Sword.GetComponent<Animator>();

        DialogManager.Instance.OnShowDialog += () => {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnHideDialog += () => {
            if(state == GameState.Dialog) {
                state = GameState.FreeRoam;
            }
        }; 
        
    }

    public void Update(){
        if(state == GameState.FreeRoam) {
            currentController.FixedUpdate();
            SwappingCharacters();
        } else if (state == GameState.Dialog) {
            DialogManager.Instance.HandleUpdate();
        } else if (state == GameState.Battle) {

        }
    }

    public void SwappingCharacters()
    {
        if(!currentAnimator.GetBool("isWalking") && !swordAnimator.GetBool("IsSwinging")) {
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                Vector2 pos;
                Rigidbody2D prevRb = currentController.GetComponent<Rigidbody2D>();
                pos = prevRb.position;
                SwordsMan.SetActive(true);
                GunMan.SetActive(false);
                currentController = SwordsMan.GetComponent<PlayerController>();
                currentPlayer = SwordsMan.GetComponent<Player>();
                currentAnimator = SwordsMan.GetComponent<Animator>();
                Rigidbody2D currentRb = currentController.GetComponent<Rigidbody2D>();
                currentRb.MovePosition(pos);
            } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
                Vector2 pos;
                Rigidbody2D prevRb = currentController.GetComponent<Rigidbody2D>();
                pos = prevRb.position;
                SwordsMan.SetActive(false);
                GunMan.SetActive(true);
                currentController = GunMan.GetComponent<PlayerController>();
                currentPlayer = GunMan.GetComponent<Player>();
                currentAnimator = GunMan.GetComponent<Animator>();
                Rigidbody2D currentRb = currentController.GetComponent<Rigidbody2D>();
                currentRb.MovePosition(pos);
            }
        }
    }
}