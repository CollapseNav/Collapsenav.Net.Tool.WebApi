using Collapsenav.Net.Tool.AutoInject;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmitDynamicApiDemo.Controllers;

[Route("testdy")]
public class FirstCrudController : ControllerBase
{
    [AutoInject]
    public IModifyRepository<FirstEntity> firstRepo { get; set; }
    [AutoInject]
    public IModifyRepository<SecondEntity> secondRepo { get; set; }

    [HttpGet("wtf")]
    public async Task<int> GetDD()
    {
        await TestTrans();
        return 10;
    }

    private async Task TestTrans()
    {
        await firstRepo.UpdateWithoutTransactionAsync(item => item.Name == "123", item => new FirstEntity { Description = "111" });
        throw new Exception();
        await secondRepo.UpdateAsync(item => item.Name == "123", item => new SecondEntity { Description = "111" });
    }
}
