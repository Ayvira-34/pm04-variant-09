namespace Shop;

public static class RepairLogic
{
public static TUser? Authenticate<TUser>(IEnumerable<TUser> users, string login)
where TUser : class, IRegisteredUser
{
if (users == null || string.IsNullOrWhiteSpace(login))
return null;

    var trimmedLogin = login.Trim();
    return users.FirstOrDefault(u =>
        string.Equals(u.Login, trimmedLogin, StringComparison.OrdinalIgnoreCase));
}

public static void Back<TLine>(ICollection<TLine> draft)
{
    // При возврате неподтверждённая заявка сохраняется без очистки.
}

public static List<TItem> Catalog<TItem>(IEnumerable<TItem> items, string query,
    string category, bool descending) where TItem : ICatalogItem
{
    if (items == null)
        return new List<TItem>();

    var trimmedQuery = (query ?? string.Empty).Trim();

    var filtered = items.Where(p =>
        (string.IsNullOrEmpty(category) || category == "Все категории" || p.Category == category) &&
        (string.IsNullOrEmpty(trimmedQuery) ||
         (p.Name != null && p.Name.Contains(trimmedQuery, StringComparison.OrdinalIgnoreCase)) ||
         (p.Description != null && p.Description.Contains(trimmedQuery, StringComparison.OrdinalIgnoreCase))));

    return descending
        ? filtered.OrderByDescending(p => p.Price).ThenBy(p => p.Id).ToList()
        : filtered.OrderBy(p => p.Price).ThenBy(p => p.Id).ToList();
}
}