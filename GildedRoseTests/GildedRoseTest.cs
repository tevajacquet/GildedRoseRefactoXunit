using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void foo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
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


}