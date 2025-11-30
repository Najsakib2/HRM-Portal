public static class CacheDurations
{
    public static TimeSpan Default => TimeSpan.FromHours(1);
    public static TimeSpan Short => TimeSpan.FromMinutes(15);
    public static TimeSpan Long => TimeSpan.FromDays(1);
}
