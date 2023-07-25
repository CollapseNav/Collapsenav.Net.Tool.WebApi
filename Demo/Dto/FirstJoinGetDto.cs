using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo;

public class FirstJoinGetDto : BaseJoinGet<FirstEntity, ReturnModel>
{
    public long? Id { get; set; }
    public override IQueryable<ReturnModel> GetQuery(IRepository<FirstEntity> repo)
    {
        return repo.CreateJoin()
        .LeftJoin<SecondEntity>(i => i.Name, i => i.Description)
        .Query
        .Select(item => new ReturnModel
        {
            Id = item.Data1.Id,
            Name = item.Data1.Name,
            Age = item.Data2.Age.ToString(),
        });
    }
}

public class ReturnModel
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
}