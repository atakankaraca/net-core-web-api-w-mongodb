using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using POC.WebAPI.Helpers;
using POC.WebAPI.Models;
using POC.WebAPI.Models.Result;
using POC.WebAPI.Repository;
using NLog.Web;

namespace POC.WebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IRepository<Product> productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var product = await _productRepository.Get();
                if (product == null)
                {
                    return new NotFoundObjectResult(new ReturnModel()
                    {
                        Message = GenericHelper.NotFoundMessage,
                        Success = false,
                        StatusCode = NotFound().StatusCode
                    });
                }

                return Ok(new MultipleDataModel<Product>
                {
                    Message = GenericHelper.OkMessage,
                    Success = true,
                    StatusCode = Ok().StatusCode,
                    Data = product
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new ReturnModel
                {
                    Message = e.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ReturnModel
                {
                    Message = ModelState.ToString(),
                    Success = false,
                    StatusCode = BadRequest().StatusCode
                });

                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    return new NotFoundObjectResult(new ReturnModel()
                    {
                        Message = GenericHelper.NotFoundMessage,
                        Success = false,
                        StatusCode = NotFound().StatusCode
                    });
                }

                return Ok(new SingleDataModel<Product>
                {
                    Message = GenericHelper.OkMessage,
                    Success = true,
                    StatusCode = Ok().StatusCode,
                    Data = product
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new ReturnModel
                {
                    Message = e.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ReturnModel
                {
                    Message = ModelState.ToString(),
                    Success = false,
                    StatusCode = BadRequest().StatusCode
                });

                await _productRepository.Add(product);

                return Created("", new ReturnModel
                {
                    Message = GenericHelper.CreatedMessage,
                    Success = true,
                    StatusCode = 201
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new ReturnModel
                {
                    Message = e.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new ReturnModel
                    {
                        Message = ModelState.ToString(),
                        Success = false,
                        StatusCode = BadRequest().StatusCode
                    });

                var prod = await _productRepository.Update(id, product);

                return Ok(new SingleDataModel<Product>
                {
                    Message = GenericHelper.ModifiedMEssage,
                    Success = true,
                    StatusCode = Ok().StatusCode,
                    Data = prod
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new ReturnModel
                {
                    Message = e.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _productRepository.Remove(id);

                if (!result.IsAcknowledged)
                {
                    return BadRequest(new ReturnModel
                    {
                        Message = GenericHelper.BadRequestMessage,
                        Success = false,
                        StatusCode = BadRequest().StatusCode
                    });
                }

                return Ok(new ReturnModel()
                {
                    Message = GenericHelper.OkMessage,
                    Success = true,
                    StatusCode = Ok().StatusCode
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return Json(new ReturnModel
                {
                    Message = e.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }
    }
}
