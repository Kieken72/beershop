using System;
using System.Collections.Generic;
using Catalog.API.Model;

namespace Catalog.API.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<Beer> _beerList;

        public CatalogRepository()
        {
            _beerList =  SeedRepositoryItems();
        }

        public IEnumerable<Beer> All()
        {
            return _beerList;
        }

        private List<Beer> SeedRepositoryItems()
        {
            var beers = new List<Beer>();
            beers.Add(new Beer { Id = 1, Name = "Hocus Pocus", Description = "Our take on a classic summer ale.  A toast to weeds, rays, and summer haze.  A light, crisp ale for mowing lawns, hitting lazy fly balls, and communing with nature, Hocus Pocus is offered up as a summer sacrifice to clodless days.\r\n\r\nIts malty sweetness finishes tart and crisp and is best apprediated with a wedge of orange.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 2, Name = "Grimbergen Blonde", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 3, Name = "Widdershins Barleywine", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 4, Name = "Lucifer", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 5, Name = "Bitter", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 6, Name = "Winter Warmer", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 7, Name = "Winter Welcome 2007-2008", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 8, Name = "Oatmeal Stout", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 9, Name = "Espresso Porter", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 10, Name = "Chocolate Stout", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 11, Name = "Hitachino Nest Real Ginger Brew", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 12, Name = "JuJu Ginger", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 13, Name = "The Kidd Lager", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 14, Name = "Imperial Stout", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 15, Name = "Oak-Aged Belgian Tripel", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 16, Name = "Ultrablonde", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 17, Name = "Wiesen Edel Weisse", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 18, Name = "Old Foghorn 2001", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 19, Name = "Framboise", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 20, Name = "Cow Palace Scotch Ale 1998", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 21, Name = "Bigfoot 2001", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 22, Name = "Bigfoot 2002", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 23, Name = "Bigfoot 2003", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 24, Name = "Bigfoot 2004", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 25, Name = "Bigfoot 2005", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 26, Name = "Winter Ale", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 27, Name = "Full Moon Winter Ale", Description = "This full-bodied ale is brewed with roasted malts and a hint of Dark Belgian sugar for a perfectly balanced taste.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 28, Name = "'Odell IPA", Description = "We took the traditional IPA, originally shipped from England to India in the 1700\'s, and made it bolder and more flavorful - American Style. We\'ve added new varieties of highly aromatic American hops to create a distinctive bitterness profile and an incredible hop character.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 29, Name = "Ornery Amber Lager", Description = "Ornery Amber is brewed with a blend of the finest European hops and gently roasted malts.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 30, Name = "Cutthroat Porter", Description = "Not quite a stout but definitely no lightweight, Cutthroat Porter is smooth and robust. Inspired by the classic London porters, we use dark roasted malts to create a deep, rich color and flavor that hint at chocolate and coffee. We named it Cutthroat Porter as our tribute to the Colorado state fish - with its own rich heritage and unmistakable dark coloring. And while we\'re big fans of small batches, here\'s to the currently threatened Cutthroat population reaching mass quantities.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 31, Name = "Maple Nut Brown Ale Ale", Description = "Maple syrup is added to each barrel of Maple Nut Brown Ale to impart roasted sweetness balancing the nut flavor produced by chocolate malts.", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            beers.Add(new Beer { Id = 32, Name = "Cocoa Porter", Description = "", LastModified = DateTime.Parse("2010-07-22 20:00:20") });
            return beers;
        }

        public void Remove(Beer beer)
        {
            _beerList.Remove(beer);
        }
    }
}
