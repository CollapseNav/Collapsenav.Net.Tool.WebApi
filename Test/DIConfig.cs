using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class DIConfig
{
    public static ServiceProvider GetProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>(new SqliteConn("Test.db").ToString())
        .AddDefaultDbContext<TestDbContext>()
        .AddRepController()
        .AddAutoMapper()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestNotBaseDbContext>(new SqliteConn("Test.db").ToString())
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddRepController()
        .AddMap<MappingProfile>()
        .BuildServiceProvider();
    }

    public static ServiceProvider GetAppProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestDbContext>(new SqliteConn("TestApp.db").ToString())
        .AddDefaultDbContext<TestDbContext>()
        .AddAppController()
        .BuildServiceProvider();
    }
    public static ServiceProvider GetNotBaseAppProvider()
    {
        return new ServiceCollection()
        .AddSqlite<TestNotBaseDbContext>(new SqliteConn("TestApp.db").ToString())
        .AddMap(typeof(MappingProfile))
        .AddDefaultDbContext<TestNotBaseDbContext>()
        .AddAppController()
        .BuildServiceProvider();
    }

    // public static (IServiceCollection collection, ServiceProvider provider) GetDynamicApiProvider()
    // {
    //     var coll = new ServiceCollection()
    //     // .AddQueryApi<TestEntity, TestEntityGet>("DTestQuery")
    //     // .AddQueryApi<int, TestEntity, TestEntityGet>("DTestIntQuery")
    //     .AddModifyApi<TestEntity, TestEntityCreate>("DTestModify")
    //     .AddModifyApi<int, TestEntity, TestEntityCreate>("DTestIntModify")
    //     .AddCrudApi<TestEntity, TestEntityCreate, TestEntityGet>("DTestCrud")
    //     .AddCrudApi<int, TestEntity, TestEntityCreate, TestEntityGet>("DTestIntCrud")
    //     ;
    //     return (coll, coll.BuildServiceProvider());
    // }
}
