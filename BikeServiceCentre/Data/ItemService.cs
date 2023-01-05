using System.Text.Json;

namespace BikeServiceCentre.Data;
public static class ItemService
{
    private static void SaveAll(Guid userId, List<ItemModel> items)
    {
        string appDataDirectoryPath = Utils.GetAppDirectoryPath();
        string itemsFilePath = Utils.GetAppItemsFilePath(userId);

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(items);
        File.WriteAllText(itemsFilePath, json);
    }

    public static List<ItemModel> View(Guid userId)
    {
        string todosFilePath = Utils.GetAppItemsFilePath(userId);
        if (!File.Exists(todosFilePath))
        {
            return new List<ItemModel>();
        }

        var json = File.ReadAllText(todosFilePath);

        return JsonSerializer.Deserialize<List<ItemModel>>(json);
    }

    public static List<ItemModel> Insert(Guid userId, string itemName, string quantity, DateTime date)
    {
        List<ItemModel> items = View(userId);
        items.Add(new ItemModel
        {
            ItemName = itemName,
            Quantity= quantity,
            Date = date,
            CreatedBy = userId
        });
        SaveAll(userId, items);
        return items;
    }

    public static List<ItemModel> Delete(Guid userId, Guid id)
    {
        List<ItemModel> items = View(userId);
        ItemModel item = items.FirstOrDefault(x => x.Id == id);

        if (item == null)
        {
            throw new Exception("Todo not found.");
        }

        items.Remove(item);
        SaveAll(userId, items);
        return items;
    }

    public static void DeleteByUserId(Guid userId)
    {
        string todosFilePath = Utils.GetAppItemsFilePath(userId);
        if (File.Exists(todosFilePath))
        {
            File.Delete(todosFilePath);
        }
    }

    public static List<ItemModel> Update(Guid userId, Guid id, string itemName, string quantity, DateTime date)
    {
        List<ItemModel> items = View(userId);
        ItemModel itemToUpdate = items.FirstOrDefault(x => x.Id == id);

        if (itemToUpdate == null)
        {
            throw new Exception("Item not found.");
        }

        itemToUpdate.ItemName = itemName;
        itemToUpdate.Quantity = quantity;
        itemToUpdate.Date = date;
        SaveAll(userId, items);
        return items;
    }
    //public static Dictionary<> chart()
    //{
    //    return View(Guid userId)
    //    //_items1 = ItemService.View(_globalState.CurrentUser.Id);
    //}

}
