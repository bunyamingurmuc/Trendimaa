namespace Trendimaa.BLL.Helper
{
    public static class SearchExtensions
    {
        public static IQueryable<T> SearchByWord<T>(this IQueryable<T> source, string searchWord, params Func<T, string>[] selectors)
        {
            if (string.IsNullOrEmpty(searchWord) || searchWord.Length < 3)
                return source;

            searchWord = searchWord.ToLower().Trim();

            // Listeyi filtreliyoruz
            return source.Where(item =>
                selectors.Any(selector =>
                {
                    var value = selector(item)?.ToLower();
                    return value != null && value.Contains(searchWord);
                }));
        }
    }
}