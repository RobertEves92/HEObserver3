using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HertfordshireMercury.Models;
using HertfordshireMercury.Services;

using CodeHollow.FeedReader;

[assembly: Xamarin.Forms.Dependency(typeof(HertfordshireMercury.Services.DataStore))]
namespace HertfordshireMercury.Services
{
    public class DataStore : IDataStore<Item>
    {
        List<Item> items;

        public DataStore()
        {
            items = new List<Item>();

#if DEBUG   //use static feed saved in gist for testing
            string feedUrl = "https://gist.githubusercontent.com/RobertEves92/85e22fbe847fc4fb08e1aa28851e3bdd/raw/ba25f9d2a9ef44a17071f2507ca20726f3832f74/gistfile1.txt";
#else       //use live feed from mercury for releases
            string feedUrl = "https://www.hertfordshiremercury.co.uk/news/?service=rss";
#endif
            string feedSrc = NetServices.GetWebpageFromUrl(feedUrl);
            var feed = FeedReader.ReadFromString(feedSrc);

            Storage.SaveTextDoc(feedSrc, "feed.txt");            

            foreach (var item in feed.Items)
            {
                items.Add(new Item { Id = Guid.NewGuid().ToString(), Text = Unescape.UnescapeHtml(item.Description), Description = Unescape.UnescapeHtml(item.Title), DateTime = (DateTime)item.PublishingDate, Author = Unescape.UnescapeHtml(item.Author), Link = item.Link });
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}