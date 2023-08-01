using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmitDynamicApiDemo.Controllers;

[Route("testdy")]
public class FirstCrudController : ControllerBase
{
    public IModifyRepository<FirstEntity> firstRepo { get; set; }

    public FirstCrudController(IModifyRepository<FirstEntity> firstRepo, IModifyRepository<SecondEntity> secondRepo)
    {
        this.firstRepo = firstRepo;
        this.secondRepo = secondRepo;
    }

    public IModifyRepository<SecondEntity> secondRepo { get; set; }

    [HttpGet("wtf")]
    public async Task<int> GetDD()
    {

        Console.WriteLine(firstRepo == null || secondRepo == null);
        // await firstRepo.AddAsync(new FirstEntity { });
        // await secondRepo.AddAsync(new SecondEntity { });
        return 10;
    }
}
