using Collapsenav.Module;
using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;
using Collapsenav.WebApi.Module;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args).LoadModules();
var app = builder.Build();
app.UseModules();
app.MapControllers();
app.Run();
public class EntityContext : DbContext
{
    public DbSet<FirstEntity> FirstEntity { get; set; }
    public DbSet<SecondEntity> SecondEntity { get; set; }
    public EntityContext(DbContextOptions<EntityContext> options) : base(options) { }
}
/// <summary>
/// 实体1
/// </summary>
public class FirstEntity : Entity<long>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
/// <summary>
/// 实体2
/// </summary>
public class SecondEntity : BaseEntity<long?>
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Description { get; set; }
}
[MapController("testdy")]
public class FirstCreateDto : BaseCreate<FirstEntity>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public override bool IsExist(IQueryable<FirstEntity>? rep) => rep.WhereIf(Name, item => item.Name == Name).Any();
}
[MapController("testdy")]
public class FirstGetDto : BaseGet<FirstEntity>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public override IQueryable<FirstEntity> GetQuery(IRepository<FirstEntity> query) => query.Query().WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Name, item => item.Name == Name)
        .WhereIf(Description, item => item.Description.Contains(Description))
        ;
}
[MapGet("testdy", "New", true)]
public class FirstJoinGetDto : BaseJoinGet<FirstEntity, ReturnModel>
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public override IQueryable<ReturnModel?> GetQuery(IRepository<FirstEntity> repo) => repo.LeftJoin<SecondEntity>(i => i.Name, i => i.Description)
        .WhereIf(Id.HasValue, item => item.Data1.Id == Id)
        .WhereIf(Name, item => item.Data1.Name.Contains(Name))
        .SelectValue((f, s) => new ReturnModel
        {
            Id = f?.Id,
            Name = f?.Name,
            Age = s?.Age.ToString(),
        });
}
public class FirstJoinGetDto2 : BaseJoinGet<FirstEntity, ReturnModel>
{
    public long? Id { get; set; }
    public override IQueryable<ReturnModel> GetQuery(IRepository<FirstEntity> repo) => repo.LeftJoin<SecondEntity>(i => i.Name, i => i.Description)
        .WhereIf(Id.HasValue, item => item.Data1.Id == Id)
        .Select(item => new ReturnModel
        {
            Id = item.Data1.Id,
            Name = item.Data1.Name,
            Age = item.Data2.Age.ToString(),
        });
}
public class ReturnModel
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Age { get; set; }
}