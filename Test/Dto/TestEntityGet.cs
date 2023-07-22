using System;
using System.Linq;
using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class TestEntityGet : BaseGet<TestEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestEntity> GetQuery(IRepository<TestEntity> repository)
    {
        return repository.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
public class TestQueryEntityGet : BaseGet<TestQueryEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestQueryEntity> GetQuery(IRepository<TestQueryEntity> repository)
    {
        return repository.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}

public class TestModifyEntityGet : BaseGet<TestModifyEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestModifyEntity> GetQuery(IRepository<TestModifyEntity> query)
    {
        return query.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}


public class TestNotBaseEntityGet : BaseGet<TestNotBaseEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestNotBaseEntity> GetQuery(IRepository<TestNotBaseEntity> query)
    {
        return query.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
public class TestNotBaseQueryEntityGet : BaseGet<TestNotBaseQueryEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }

    public override IQueryable<TestNotBaseQueryEntity> GetQuery(IRepository<TestNotBaseQueryEntity> query)
    {
        return query.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}

public class TestNotBaseModifyEntityGet : BaseGet<TestNotBaseModifyEntity>
{
    public int? Id { get; set; }
    public string Code { get; set; }
    public int? Number { get; set; }
    public bool? IsTest { get; set; }
    public override IQueryable<TestNotBaseModifyEntity> GetQuery(IRepository<TestNotBaseModifyEntity> query)
    {
        return query.Query()
        .WhereIf(Id.HasValue, item => item.Id == Id)
        .WhereIf(Code, item => item.Code.Contains(Code))
        .WhereIf(Number.HasValue, item => item.Number > Number)
        .WhereIf(IsTest.HasValue, item => item.IsTest == IsTest)
        ;
    }
}
