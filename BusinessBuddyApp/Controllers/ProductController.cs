using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBuddyApp.Controllers
{
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
           var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get([FromRoute]int id) 
        {
            var product = await _productService.Get(id);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update([FromBody] Product product, [FromRoute] int id)
        {
            var updatedProduct = await _productService.Update(product, id);
            return Ok(updatedProduct);
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product product)
        {
            var isCreated = _productService.Create(product);
            return Ok(isCreated);
        }


    }
}
