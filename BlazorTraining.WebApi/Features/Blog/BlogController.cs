using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTraining.WebApi.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogBusinessLogic _blogBusinessLogic;

        public BlogController(BlogBusinessLogic blogBusinessLogic)
        {
            _blogBusinessLogic = blogBusinessLogic;
        }

        [HttpPost]
        [Route("List")]
        public async Task<IActionResult> GetBlogs(BlogRequestModel requestModel)
        {
            try
            {
                return Ok(await _blogBusinessLogic.GetBlogs(
                    requestModel.PageSettng.PageNo,
                    requestModel.PageSettng.PageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogListResponseModel
                {
                    Response = new ResponseModel("999", ex.ToString(), EnumRespType.Error)
                });
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBlog(BlogRequestModel requestModel)
        {
            try
            {
                return Ok(await _blogBusinessLogic.CreateBlog(requestModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BlogListResponseModel
                {
                    Response = new ResponseModel("999", ex.ToString(), EnumRespType.Error)
                });
            }
        }
    }
}
