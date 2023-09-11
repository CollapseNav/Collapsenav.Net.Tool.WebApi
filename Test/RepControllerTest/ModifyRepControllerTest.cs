using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class ModifyRepControllerTest
{
    protected readonly IServiceProvider Provider;
    public ModifyRepControllerTest()
    {
        Provider = DIConfig.GetProvider();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }
    [Fact, Order(1)]
    public async Task AddTest()
    {
        using var RepController = GetService<IModifyController<TestModifyEntity, TestModifyEntityCreate>>();
        var entitys = new List<TestModifyEntityCreate>{
                new ("wait-to-delete",23334,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
            };
        await RepController.AddAsync(entitys.First());
        await RepController.AddRangeAsync(entitys.Skip(1));
        RepController.Dispose();
        var queryController = GetService<IQueryController<TestModifyEntity, TestModifyEntityGet>>();
        var data = await queryController.QueryAsync(new TestModifyEntityGet { Code = "wait-to-delete" });
        Assert.True(data.Count() == 10);
    }

    [Fact, Order(3)]
    public async Task UpdateTest()
    {
        var queryController = GetService<IQueryController<TestModifyEntity, TestModifyEntityGet>>();
        using var RepController = GetService<IModifyController<TestModifyEntity, TestModifyEntityCreate>>();
        var data = await queryController.QueryAsync(new TestModifyEntityGet { Code = "wait-to-delete", Number = 23333 });
        foreach (var item in data)
            await RepController.UpdateAsync(item.Id.ToString(), new(item.Code, item.Number + 1, !item.IsTest));
        RepController.Dispose();
        queryController = GetService<IQueryController<TestModifyEntity, TestModifyEntityGet>>();
        var pageData = await queryController.QueryPageAsync(new TestModifyEntityGet { Code = "wait-to-delete", Number = 23333 });
        Assert.True(pageData.Length == 1);
        Assert.True(pageData.Data.All(item => item.Number == 23335));
    }
    [Fact, Order(4)]
    public async Task DeleteTest()
    {
        var queryController = GetService<IQueryController<TestModifyEntity, TestModifyEntityGet>>();
        using var RepController = GetService<IModifyController<TestModifyEntity, TestModifyEntityCreate>>();
        var data = await queryController.QueryAsync(new TestModifyEntityGet { Code = "wait-to-delete" });
        await RepController.DeleteAsync(data.First().Id.ToString(), true);
        await RepController.DeleteRangeAsync(data.Skip(1).Select(item => item.Id.ToString()), true);
        RepController.Dispose();
        queryController = GetService<IQueryController<TestModifyEntity, TestModifyEntityGet>>();
        data = await queryController.QueryAsync(new TestModifyEntityGet { Code = "wait-to-delete" });
        Assert.True(data.IsEmpty());
    }
}
