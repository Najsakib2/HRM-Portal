public static class CacheKeyHelper<T>
{
    private const string AppPrefix = "HRM_v1_";

    public static string GetById(int id) =>
        $"{AppPrefix}{typeof(T).Name}_Single_{id}";

    public static string GetAll() =>
        $"{AppPrefix}{typeof(T).Name}_All";

    public static string GetAllByCompany(int companyId) =>
        $"{AppPrefix}{typeof(T).Name}_Company_{companyId}";

    public static string Custom(string suffix) =>
        $"{AppPrefix}{typeof(T).Name}_{suffix}";
}
