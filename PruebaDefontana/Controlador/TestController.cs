using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaDefontana.Interfaz;

namespace PruebaDefontana.Controlador
{
    [ApiController]
    [AllowAnonymous]
    public class TestController : Controller
    {

        private readonly ITest _test;

        public TestController(ITest test)
        {
            _test = test;
        }

        [HttpGet]
        [Route("api/consultaDetalleDeVentas/{days}")]
        public async Task<object> consultaDetalleDeVentas([FromRoute] int days)
        {
            var service = await _test.consultaDetalleDeVentas(days);

            return service;
        }
    }
}
