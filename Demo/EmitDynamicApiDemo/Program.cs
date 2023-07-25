using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.DynamicApi;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;
using SimpleWebApiDemo;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDynamicWebApi();
// builder.Services.AddDynamicController();
var t = typeof(CrudRepController<,,>);
var tt = t.MakeGenericType(typeof(FirstEntity), typeof(FirstCreateDto), typeof(FirstGetDto));
ApplicationServiceConvention.Types.Add(tt);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// ApplicationServiceConvention.Types.Add(typeof(CrudRepController<FirstEntity, FirstCreateDto, FirstGetDto>));
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDefaultSwaggerGen();
// builder.Services.AddDynamicController();
builder.Services.AddRepository();
builder.Services.AddMap();
builder.Services.AddSqlitePool<EntityContext>(new SqliteConn("./Data.db"));
builder.Services.AddDefaultDbContext<EntityContext>();
// builder.Services.AddScoped<CrudRepController<FirstEntity, FirstCreateDto, FirstGetDto>>();
// 如果业务足够简单, 就不需要实现 Controller, 偶尔会有用
// builder.Services.AddCrudApi<FirstEntity, FirstCreateDto, FirstGetDto>("FirstAAAAA");
// // builder.Services.AddModifyApi<long?, SecondEntity, SecondCreateDto>("WTFFFFF");
// builder.Services.AddCrudApi<long?, SecondEntity, SecondCreateDto, SecondGetDto>("WTFFFFF");
var app = builder.Build();

app.UseAutoCommit();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
