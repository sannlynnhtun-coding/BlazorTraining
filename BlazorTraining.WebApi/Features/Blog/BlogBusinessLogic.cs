namespace BlazorTraining.WebApi.Features.Blog
{
    public class BlogBusinessLogic
    {
        private readonly BlogDataAccess _blogDataAccess;

        public BlogBusinessLogic(BlogDataAccess blogDataAccess)
        {
            _blogDataAccess = blogDataAccess;
        }

        public async Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize)
        {
            BlogListResponseModel model = new BlogListResponseModel();
            if (pageNo == 0)
            {
                model.Response = new ResponseModel("999", "Invalid Page No.", EnumRespType.Warning);
                goto result;
                //return model;
            }
            if (pageSize == 0)
            {
                model.Response = new ResponseModel("999", "Invalid Page Size.", EnumRespType.Warning);
                goto result;
                //return model;
            }
            model = await _blogDataAccess.GetBlogs(pageNo, pageSize);
        result:
            return model;
        }

        public async Task<BlogResponseModel> CreateBlog(BlogRequestModel requestModel)
        {
            return await _blogDataAccess.CreateBlog(requestModel);
        }
    }
}
