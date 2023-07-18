using System.Linq.Expressions;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;

public class SecondGetDto : BaseGet<SecondEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }

    public override IQueryable<SecondEntity> GetQuery(IQueryable<SecondEntity> query)
    {
        return query
        .WhereIf(Name, item => item.Name.Contains(Name))
        ;
    }
}

public class SecondGetDto2 : BaseGet<SecondEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }

    public override IQueryable<SecondEntity> GetQuery(IQueryable<SecondEntity> query)
    {
        return query
        .WhereIf(Name, item => item.Name.Contains(Name))
        ;
    }
}