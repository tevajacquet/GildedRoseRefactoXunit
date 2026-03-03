using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    public const string AgedBrieConst = "Aged Brie";
    public const string BackStageConst = "Backstage passes to a TAFKAL80ETC concert";
    public const string SulfurasConst = "Sulfuras, Hand of Ragnaros";
    public const string ConjuredConst = "Conjured Mana Cake";

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

    private static (GildedRose app, Item item) InitAppTestAvecUnItem(string name, int sellIn, int quality)
    {
        var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
        var app = new GildedRose(new List<Item> { item });
        return (app, item);
    }

    //Article normal : sellin = 1, quality = 10 => après update : sellin = 0, quality = 9
    [Fact]
    public void ArticleNormalSellinPositif()
    {
        var (app, item) = InitAppTestAvecUnItem("foo", 1, 10);

        app.UpdateQuality();

        Assert.Equal(0, item.SellIn);
        Assert.Equal(9, item.Quality);
    }

    [Fact]
    public void ArticleNormalSellInNegatif()
    {
        var (app, item) = InitAppTestAvecUnItem("foo", 0, 10);


        app.UpdateQuality();

        Assert.Equal(-1, item.SellIn);
        Assert.Equal(8, item.Quality);
    }

    [Fact]
    public void ArticleNormalQualiteZero()
    {
        var (app, item) = InitAppTestAvecUnItem("foo", 1, 0);

        app.UpdateQuality();

        Assert.Equal(0, item.SellIn);
        Assert.Equal(0, item.Quality);
    }

    //Aged Brie : sellin = 0, quality = 10 => après update : sellin = -1, quality = 12 si expiré sinon quality = 11
    //Aged Brie : sellin = 5, quality = 49 => après update : sellin = 4, quality = 50
    //Aged Brie : sellin = 5, quality = 50 => après update : sellin = 4, quality = 50
    [Theory]
    [InlineData(0, 10, -1, 12)]
    [InlineData(-1, 10, -2, 12)]
    [InlineData(5, 49, 4, 50)]
    [InlineData(5, 50, 4, 50)]
    [InlineData(0, 50, -1, 50)]
    public void AgedBrie(int sellIn, int quality, int sellInAttendu, int qualityAttendu)
    {
        var (app, item) = InitAppTestAvecUnItem(AgedBrieConst, sellIn, quality);

        app.UpdateQuality();

        Assert.Equal(sellInAttendu, item.SellIn);
        Assert.Equal(qualityAttendu, item.Quality);
        Assert.InRange(item.Quality, 0, 50);
    }

    [Theory]
    [InlineData(0, 80)]
    [InlineData(-5, 80)]
    [InlineData(10, 80)]
    public void Sulfuras(int sellIn, int quality)
    {
        //IList<Item> Items = new List<Item> { new Item { Name = SulfurasConst, SellIn = sellIn, Quality = quality } };
        var (app, item) = InitAppTestAvecUnItem(SulfurasConst, sellIn, quality);

        app.UpdateQuality();

        Assert.Equal(sellIn, item.SellIn);
        Assert.Equal(quality, item.Quality);
        Assert.Equal(80, item.Quality);
    }


    //Cas 5 : Backstage passes augmente de 1 la qualité si sellin > 10, de 2 si 6 <= sellin <= 10, de 3 si 1 <= sellin <= 5 et tombe à 0 si sellin <= 0
    [Theory]
    [InlineData(11, 20, 10, 21)]
    [InlineData(15, 49, 14, 50)] // max 50
    [InlineData(15, 50, 14, 50)] // max 50

    [InlineData(10, 20, 9, 22)]
    [InlineData(6, 20, 5, 22)]

    [InlineData(5, 20, 4, 23)]
    [InlineData(1, 20, 0, 23)]

    [InlineData(0, 20, -1, 0)]
    public void BackstagePasses(int sellIn, int quality, int sellInAttendu, int qualityAttendu)
    {
        var (app, item) = InitAppTestAvecUnItem(BackStageConst, sellIn, quality);

        app.UpdateQuality();

        Assert.Equal(sellInAttendu, item.SellIn);
        Assert.Equal(qualityAttendu, item.Quality);
        Assert.InRange(item.Quality, 0, 50);
    }

    public void Conjured()
    {
        //TODO
    }






}























