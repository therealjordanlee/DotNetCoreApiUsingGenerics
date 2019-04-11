using System;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCoreApiUsingGenerics.Attributes;
using DotNetCoreApiUsingGenerics.Configurations;
using DotNetCoreApiUsingGenerics.Entities;
using DotNetCoreApiUsingGenerics.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace DotNetCoreApiUsingGenerics.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly ITableRepository<MessageEntity> _tableRepository;
        public HomeController(ITableRepository<MessageEntity> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetTableValue()
        {
            //var test = new MessageEntity();

            //var blah = typeof(MessageEntity).GetCustomAttribute<TableNameAttribute>();


            var result = await _tableRepository.FindRecord("123", "123");
            return Ok(result);
        }
    }
}
