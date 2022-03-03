#region

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Web.Models;
using Store.Web.Services.IServices;

#endregion

namespace Store.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    /// <inheritdoc />
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _productService.GetAsync<ResponseDto>();

        if (!response.IsSuccess)
            return NotFound();

        var products = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

        return View(products);
    }

    public Task<IActionResult> Create()
    {
        return Task.FromResult<IActionResult>(View(new ProductDto()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDto product)
    {
        var response = await _productService.CreateAsync<ResponseDto>(product);

        if (!response.IsSuccess)
            return View(product);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _productService.GetByIdAsync<ResponseDto>(id);

        if (!response.IsSuccess)
            return NotFound();

        var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductDto product)
    {
        var response = await _productService.UpdateAsync<ResponseDto>(product);

        if (!response.IsSuccess)
            return View(product);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _productService.GetByIdAsync<ResponseDto>(id);

        if (!response.IsSuccess)
            return NotFound();

        var product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(ProductDto product)
    {
        if (product.Id == 0)
            return View(product);

        var response = await _productService.DeleteAsync<ResponseDto>(product.Id);

        if (!response.IsSuccess)
            return BadRequest();

        return RedirectToAction(nameof(Index));
    }
}