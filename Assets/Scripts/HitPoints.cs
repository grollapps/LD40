using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoints : MonoBehaviour {

    public int maxHp = 3;
    public int hp = 2;
    public List<Image> hpImages;

	// Use this for initialization
	void Start () {
        updateUi();
	}

    /// <summary>
    /// Decreases HP and returns true if this object is out of HPs
    /// </summary>
    /// <param name="amt"></param>
    /// <returns></returns>
    public bool decreaseHp(int amt) {
        setHp(hp - amt);
        if (hp <= 0) {
            return true;
        }
        return false;
    }

    public void increaseHp(int amt) {
        setHp(hp + amt);
    }

    public int getHp() {
        return hp;
    }

    public void setHp(int hp) {
        this.hp = hp;
        this.hp = Mathf.Clamp(hp, 0, maxHp);
        updateUi();
    }

    private void updateUi() {
        if (hpImages != null) {
            for (int i = 0; i < hpImages.Count; i++) {
                    Color c = hpImages[i].color;
                if (i < hp) {
                    hpImages[i].color = new Color(c.r, c.g, c.b, 1); //visible
                } else {
                    hpImages[i].color = new Color(c.r, c.g, c.b, 0);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
