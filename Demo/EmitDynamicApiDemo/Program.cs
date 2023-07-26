using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;
using SimpleWebApiDemo;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDynamicWebApi().AddRepController().AddAppController();
builder.Services.AddController<CrudAppController<long?, SecondEntity, SecondCreateDto, SecondGetDto>>("testdy");
// ApplicationServiceConvention.GetTypes.Add(typeof(SecondGetDto2));
builder.Services.AddDefaultSwaggerGen();
builder.Services.AddSqlitePool<EntityContext>(new SqliteConn("./Data.db"));
builder.Services.AddDefaultDbContext<EntityContext>();
var app = builder.Build();
app.UseAutoCommit();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();


