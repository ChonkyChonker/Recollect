using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
   public Player player; 
   public int health;
   public int numOfHearts;
   public int lifeForce;

   public Image[] hearts;
   public Sprite hs0;
   public Sprite hs1;
   public Sprite hs2;
   public Sprite hs3;
   public Sprite hs4;

   public Image[] lifeForceBars;
   public Sprite bars;

   void Update()
   {
        lifeForce = player.lifeForce;

        for(int i = lifeForce; i < player.maxLifeForce; i++) {
                lifeForceBars[i].enabled = false;
        }

        for(int i = 0; i < lifeForce; i++) {
                lifeForceBars[i].enabled = true;
        }

        health = player.health;

        for(int i = 0; i < hearts.Length; i++) {

            switch(health) {
                case 0: hearts[0].sprite = hs4;
                        hearts[1].sprite = hs4;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 1: hearts[0].sprite = hs3;
                        hearts[1].sprite = hs4;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 2: hearts[0].sprite = hs2;
                        hearts[1].sprite = hs4;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 3: hearts[0].sprite = hs1;
                        hearts[1].sprite = hs4;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 4: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs4;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 5: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs3;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 6: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs2;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 7: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs1;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 8: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs4;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 9: hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs3;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 10:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs2;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 11:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs1;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4; 
                        break;
                case 12:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs4;
                        hearts[4].sprite = hs4;
                        break;
                case 13:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs3;
                        hearts[4].sprite = hs4;
                        break;
                case 14:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs2;
                        hearts[4].sprite = hs4;
                        break;
                case 15:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs1;
                        hearts[4].sprite = hs4;
                        break;
                case 16:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs0;
                        hearts[4].sprite = hs4;
                        break;
                case 17:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs0;
                        hearts[4].sprite = hs3;
                        break;
                case 18:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs0;
                        hearts[4].sprite = hs2;
                        break;
                case 19:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs0;
                        hearts[4].sprite = hs1;
                        break;
                case 20:hearts[0].sprite = hs0;
                        hearts[1].sprite = hs0;
                        hearts[2].sprite = hs0;
                        hearts[3].sprite = hs0;
                        hearts[4].sprite = hs0;
                        break;
                default: break;
            }

            if(i < numOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
   }


}
