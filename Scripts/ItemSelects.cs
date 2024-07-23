using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSelects : MonoBehaviour
{
    //[SerializeField] private FrogCpu _frogCpu;  
    private Animator _itemAnimator;
    private enum Item {
        Default,
        Water,
        Beard,
        Pridiction,
        Mucus
    }
    private Item _item = default;
    // Start is called before the first frame update
    void Start()
    {
        _itemAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchItem();
        //if (!_frogCpu._ishavingItem) {
        //    this.gameObject.SetActive(false);
        //}

    }
   public void ItemIcon(int item) {
        if (item == 0) 
        {
            _item = Item.Default;
        }
        if (item == 1) 
        {
            _item = Item.Water;
        }
        if (item == 2) 
        {
            _item = Item.Beard;
        }
        if (item == 3) 
        {
            _item = Item.Pridiction;
        }
        if (item == 4)
        {
            _item = Item.Mucus;
        }
    }
    private void SwitchItem() {
        switch (_item) {
            case Item.Default:
                _itemAnimator.SetBool("Random", true); 
                _itemAnimator.SetBool("WaterItem", false);
                _itemAnimator.SetBool("BeardItem", false);
                _itemAnimator.SetBool("PridictionItem", false);
                _itemAnimator.SetBool("MucusItem", false);
                break;
            case Item.Water:
                _itemAnimator.SetBool("Random", false);
                _itemAnimator.SetBool("WaterItem", true);
                _itemAnimator.SetBool("BeardItem", false);
                _itemAnimator.SetBool("PridictionItem", false);
                _itemAnimator.SetBool("MucusItem", false);
                break;
            case Item.Beard:
                _itemAnimator.SetBool("Random", false);
                _itemAnimator.SetBool("WaterItem", false);
                _itemAnimator.SetBool("BeardItem", true);
                _itemAnimator.SetBool("PridictionItem", false);
                _itemAnimator.SetBool("MucusItem", false);
                break;
            case Item.Pridiction:
                _itemAnimator.SetBool("Random", false);
                _itemAnimator.SetBool("WaterItem", false);
                _itemAnimator.SetBool("BeardItem", false);
                _itemAnimator.SetBool("PridictionItem", true);
                _itemAnimator.SetBool("MucusItem", false);
                break;
                case Item.Mucus:
                _itemAnimator.SetBool("Random", false);
                _itemAnimator.SetBool("WaterItem", false);
                _itemAnimator.SetBool("BeardItem", false);
                _itemAnimator.SetBool("PridictionItem", false);
                _itemAnimator.SetBool("MucusItem", true);
                break;

        }

    }

}
