using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;


public class SecondCreateDto : BaseCreate<SecondEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
    public override bool IsExist(IQueryable<SecondEntity> rep)
    {
        return rep.WhereIf(true, item => item.Name == Name && item.Id != Id)
        .Any();
    }
}