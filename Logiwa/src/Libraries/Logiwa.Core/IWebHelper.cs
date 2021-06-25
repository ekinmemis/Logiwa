namespace Logiwa.Core
{
    public interface IWebHelper
    {
        string ModifyQueryString(string url, string queryStringModification, string anchor);
    }
}
