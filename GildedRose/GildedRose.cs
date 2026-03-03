using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    public const string AgedBrieConst = "Aged Brie";
    public const string BackStageConst = "Backstage passes to a TAFKAL80ETC concert";
    public const string SulfurasConst = "Sulfuras, Hand of Ragnaros";
    public const string ConjuredConst = "Conjured Mana Cake";

    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    //Test case reduction qualité article normal
    //Cas 1 : article normal reduire sellin de 1 et qualité de 1 si sellin >= 0 (par contre qualité ne peut pas être négative)
    //Cas 2 : article normal reduire sellin de 1 et qualité de 2 si sellin < 0 (par contre qualité ne peut pas être négative)
    //Cas 3 : Aged Brie augmente de 1 la qualité si sellin >= 0 et de 2 si sellin < 0 (par contre qualité ne peut pas être supérieure à 50)
    //Cas 4 : Sulfuras ne change pas de qualité ni de sellin
    //Cas 5 : Backstage passes augmente de 1 la qualité si sellin > 10, de 2 si 6 <= sellin <= 10, de 3 si 1 <= sellin <= 5 et tombe à 0 si sellin <= 0
    //Cas 6 : Conjured diminue de 2 la qualité si sellin >= 0 et de 4 si sellin < 0 (par contre qualité ne peut pas être négative)
    //Cas 7 : tous les articles ne peuvent pas avoir une qualité négative ni supérieure à 50 (sauf Sulfuras qui a une qualité de 80)
    //Cas pour tous tester les limite (0 et 50) pour pas avoir comportement bizarre

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            switch (item.Name)
            {
                case SulfurasConst:
                    //Pas à traiter
                    //if (item.Quality != 80) item.Quality = 80;
                    break;
                case AgedBrieConst:
                    UpdateAgedBrie(item);
                    break;
                case BackStageConst:
                    UpdateBackStagePasses(item);
                    break;
                case ConjuredConst:
                    UpdateConjured(item);
                    break;
                default:
                    UpdateNormalItem(item);
                    break;
            }
        }
    }

    //Logique de mise à jour d'un article normal : on réduit le sellin de 1, puis on réduit la qualité de 1 si le sellin est supérieur ou égal à 0, sinon on réduit la qualité de 2. La qualité ne peut pas être négative.
    private void UpdateNormalItem(Item item)
    {
        item.SellIn--;

        item.Quality = Math.Max(item.Quality - 1, 0);

        if (item.SellIn < 0)
        {
            item.Quality = Math.Max(item.Quality - 1, 0);
        }
    }

    //Logique de mise à jour d'un article Aged Brie : on réduit le sellin de 1, puis on augmente la qualité de 1 si le sellin est supérieur ou égal à 0, sinon on augmente la qualité de 2. La qualité ne peut pas être supérieure à 50.
    private void UpdateAgedBrie(Item item)
    {
        item.SellIn--;

        item.Quality = Math.Min(item.Quality + 1, 50);

        if (item.SellIn < 0)
        {
            item.Quality = Math.Min(item.Quality + 1, 50);
        }
    }

    //Logique de mise à jour d'un article Backstage passes : on réduit le sellin de 1, puis on augmente la qualité de 1 si le sellin est supérieur à 10, de 2 si le sellin est compris entre 6 et 10, de 3 si le sellin est compris entre 1 et 5, et on met la qualité à 0 si le sellin est inférieur ou égal à 0. La qualité ne peut pas être supérieure à 50.
    private void UpdateBackStagePasses(Item item)
    {
        item.SellIn--;
        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
        else if (item.SellIn < 5)
        {
            item.Quality = Math.Min(item.Quality + 3, 50);
        }
        else if (item.SellIn < 10)
        {
            item.Quality = Math.Min(item.Quality + 2, 50);
        }
        else
        {
            item.Quality = Math.Min(item.Quality + 1, 50);
        }
    }

    //Logique de mise à jour d'un article Conjured : on réduit le sellin de 1, puis on réduit la qualité de 2 si le sellin est supérieur ou égal à 0, sinon on réduit la qualité de 4. La qualité ne peut pas être négative.
    private void UpdateConjured(Item item)
    {
        item.SellIn--;
        item.Quality = Math.Max(item.Quality - 2, 0);
        if (item.SellIn < 0)
        {
            item.Quality = Math.Max(item.Quality - 2, 0);
        }
    }


    [Obsolete("Ancien code à ne plus utiliser : logique inverser + trop de if imbriqué + difficile à maintenir")]
    public void UpdateQualityOld()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (Items[i].Quality > 0)
                {
                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 11)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < 6)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < 0)
            {
                if (Items[i].Name != "Aged Brie")
                {
                    if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].Quality > 0)
                        {
                            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                            {
                                Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }
                }
            }
        }
    }
}