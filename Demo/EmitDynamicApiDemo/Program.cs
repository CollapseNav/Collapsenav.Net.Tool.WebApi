using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.AutoInject;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutoInjectProviderFactory();
builder.Services.AddControllers().AddControllersAsServices();
builder.Services.AddDynamicWebApi().AddRepController().AddAppController();
builder.Services.AddCrudController<FirstEntity, FirstCreateDto, FirstGetDto>("testdy")
.AddGetAction<FirstJoinGetDto>("QueryNew")
.AddGetAction<FirstJoinGetDto2>("QueryTTTT")
;
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddSqlitePool<EntityContext>(new SqliteConn("./Data.db"));
builder.Services.AddDefaultDbContext<EntityContext>();
builder.Services.AddAutoInjectController();
var app = builder.Build();
app.UseAutoCommit();
if (app.Environment.IsDevelopment()) app.UseSwagger().UseSwaggerUI();
app.MapControllers();
app.Run();

[DynamicApi]
public class TestApi
{
    [AutoInject]
    public IRepository<FirstEntity> FirstRepo { get; set; }

    public string QueryBBBB()
    {
        Console.WriteLine(FirstRepo == null);
        return "123";
    }
}


#region dfoasdjf
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
    public string Name { get; set; }
    public string Description { get; set; }
}
/// <summary>
/// 实体2
/// </summary>
public class SecondEntity : BaseEntity<long?>
{
    public string Name { get; set; }
    public int? Age { get; set; }
    public string Description { get; set; }
}
public class FirstCreateDto : BaseCreate<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public override bool IsExist(IQueryable<FirstEntity> rep)
    {
        return rep.WhereIf(Name, item => item.Name == Name).Any();
    }
}
public class FirstGetDto : BaseGet<FirstEntity>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public override IQueryable<FirstEntity> GetQuery(IRepository<FirstEntity> query)
    {
        return query.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Name, item => item.Name == Name)
        .WhereIf(Description, item => item.Description.Contains(Description))
        ;
    }
}
public class FirstJoinGetDto : BaseJoinGet<FirstEntity, ReturnModel>
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public override IQueryable<ReturnModel> GetQuery(IRepository<FirstEntity> repo)
    {
        return repo
        .LeftJoin<SecondEntity>(i => i.Name, i => i.Description)
        .WhereIf(Id.HasValue, item => item.Data1.Id == Id)
        .WhereIf(Name, item => item.Data1.Name == Name)
        .SelectValue((f, s) => new ReturnModel
        {
            Id = f?.Id,
            Name = f?.Name,
            Age = s?.Age.ToString(),
        });
    }
}
public class FirstJoinGetDto2 : BaseJoinGet<FirstEntity, ReturnModel>
{
    public long? Id { get; set; }
    public override IQueryable<ReturnModel> GetQuery(IRepository<FirstEntity> repo)
    {
        return repo
        .LeftJoin<SecondEntity>(i => i.Name, i => i.Description)
        .WhereIf(Id.HasValue, item => item.Data1.Id == Id)
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

#endregion