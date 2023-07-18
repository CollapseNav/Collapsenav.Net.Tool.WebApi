using System.Linq.Expressions;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;

public class FirstGetDto : BaseGet<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public override IQueryable<FirstEntity> GetQuery(IQueryable<FirstEntity> query)
    {
        query = query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Name, item => item.Name == Name)
        .WhereIf(Description, item => item.Description.Contains(Description))
        ;
        return query;
    }
}
public class FirstGetDto2 : BaseGet<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public override IQueryable<FirstEntity> GetQuery(IQueryable<FirstEntity> query)
    {
        query = query
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Name, item => item.Name == Name)
        .WhereIf(Description, item => item.Description.Contains(Description))
        ;
        return query;
    }
}