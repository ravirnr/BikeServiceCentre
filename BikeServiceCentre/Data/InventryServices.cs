using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikeServiceCentre.Data
{
    internal class InventryServices
    {
        private static void SaveAll(Guid userId, List<InventryModel> items)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string itemsFilePath = Utils.GetAppInventryFilePath(userId);

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(itemsFilePath, json);
        }

        public static List<InventryModel> View(Guid userId)
        {
            string todosFilePath = Utils.GetAppInventryFilePath(userId);
            if (!File.Exists(todosFilePath))
            {
                return new List<InventryModel>();
            }

            var json = File.ReadAllText(todosFilePath);

            return JsonSerializer.Deserialize<List<InventryModel>>(json);
        }

        public static List<InventryModel> Insert(Guid userId, string itemName, string quantity, string takenby, DateTime date)
        {
            List<InventryModel> items = View(userId);
            items.Add(new InventryModel
            {
                ItemName = itemName,
                Quantity = quantity,
                TakenBy = takenby,
                Date = date,
                ApproveBy = userId
            });
            SaveAll(userId, items);
            return items;
        }

    }
}
