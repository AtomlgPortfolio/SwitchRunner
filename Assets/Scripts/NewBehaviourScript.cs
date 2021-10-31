using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void Start()
    {
        Warehouse warehouse = new Warehouse();
        Shop shop = new Shop(warehouse);
        Good iPhone12 = new Good("IPhone 12");
        Good iPhone11 = new Good("IPhone 11");
        
        warehouse.Deliver(iPhone12, 10);
        warehouse.Deliver(iPhone11, 1);
        warehouse.ShowGoods();
        
        Cart cart = shop.Cart();
        cart.Add(iPhone12, 4);
        cart.Add(iPhone11, 3);  
        cart.ShowGoods();
    }
}

public class Shop
{
    private Warehouse _warehouse;

    public Shop(Warehouse warehouse) => 
        _warehouse = warehouse;

    public Cart Cart() => 
        new Cart(_warehouse);
}

public class Warehouse
{
    private List<Good> _goods = new List<Good>();

    public void Deliver(Good good, int count)
    {
        Good goodInList = GetGoodByName(good.Name);
        if (goodInList != null)
        {
            goodInList.AddCount(count);
        }
        else
        {
            good.AddCount(count);
            _goods.Add(good);
        }
    }

    public void ShowGoods()
    {
        for (int i = 0; i < _goods.Count; i++) 
            Debug.Log($"Имя: {_goods[i].Name}. Количество: {_goods[i].Count}");
    }

    public Good GetGoodByName(string name) => 
        _goods.FirstOrDefault(x => x.Name == name);
}
    
public class Good
{
    public string Name { get; private set; }
    public int Count { get; private set; }

    public Good(string name, int count = 0)
    {
        Name = name;
        Count = count;
    }

    public void AddCount(int count)
    {
        if (count > 0)
            Count += count;
        else
            throw new ArgumentException(nameof(count));
    }

    public void RemoveCount(int count)
    {
        if (Count >= count)
            Count -= count;
        else
            throw new ArgumentException(nameof(count));
    }
}
    
public class Cart
{
    private readonly Warehouse _warehouse;
    private List<Good> _goods = new List<Good>();

    public Cart(Warehouse warehouse) => 
        _warehouse = warehouse;

    public void Add(Good good, int count)
    {
        Good goodInList = _warehouse.GetGoodByName(good.Name);
        if(goodInList != null)
        {
            if (goodInList.Count >= count)
            {
                Good goodCopy = new Good(good.Name, good.Count);
                _goods.Add(goodCopy);
                goodInList.RemoveCount(count);
            }
            else
            {
                throw new ArgumentException(nameof(count));
            }
        }
    }

    public void ShowGoods()
    {
        for (int i = 0; i < _goods.Count; i++) 
            Debug.Log($"Имя: {_goods[i].Name}. Количество: {_goods[i].Count}");
    }
}
