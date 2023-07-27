// using Collapsenav.Net.Tool.Data;
// using DataDemo.EntityLib;
// using Microsoft.AspNetCore.Mvc;

// namespace EmitDynamicApiDemo.Controllers;

// [Route("testdy")]
// public class FirstCrudController : ControllerBase
// {
//     private readonly IModifyRepository<FirstEntity> firstRepo;
//     private readonly IModifyRepository<SecondEntity> secondRepo;

//     public FirstCrudController(IModifyRepository<FirstEntity> firstRepo, IModifyRepository<SecondEntity> secondRepo)
//     {
//         this.firstRepo = firstRepo;
//         this.secondRepo = secondRepo;
//     }
//     [HttpGet("wtf")]
//     public async Task<int> GetDD()
//     {
//         await firstRepo.AddAsync(new FirstEntity { });
//         throw new Exception();
//         await secondRepo.AddAsync(new SecondEntity { });
//         return 10;
//     }
// }
