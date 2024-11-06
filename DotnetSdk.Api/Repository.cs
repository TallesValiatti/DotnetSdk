using DotnetSdk.Common.Models;

namespace DotnetSdk.Api;

public class Repository
{
    private IList<ReviewResult> _items = new List<ReviewResult>();

    public IList<ReviewResult> List()
    {
        return _items;
    }
    
    public void Add(ReviewResult item)
    {
        _items.Add(item);
    }
}