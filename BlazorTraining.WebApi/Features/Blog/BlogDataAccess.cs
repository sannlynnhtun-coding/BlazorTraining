using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace BlazorTraining.WebApi.Features.Blog
{
	public class BlogDataAccess
	{
		private readonly AppDbContext _appDbContext;

		public BlogDataAccess(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize)
		{
			BlogListResponseModel model = new BlogListResponseModel();
			PageSettingModel pageSetting = new PageSettingModel();
			var lst = await _appDbContext.Blogs
				.AsNoTracking()
				.OrderByDescending(x => x.Blog_Id)
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			int rowCount = await _appDbContext.Blogs.CountAsync();
			int pageCount = rowCount / pageSize;
			if (rowCount % pageSize > 0)
			{
				pageCount++;
			}
			pageSetting.PageNo = pageNo;
			pageSetting.PageSize = pageSize;
			pageSetting.PageCount = pageCount;
			pageSetting.RowCount = rowCount;
			model.PageSetting = pageSetting;
			model.BlogList = lst.Select(x => new BlogViewModel
			{
				Id = x.Blog_Id,
				Author = x.Blog_Author,
				Content = x.Blog_Content,
				Title = x.Blog_Title
			}).ToList();
			model.Response = new ResponseModel("000", "Success", EnumRespType.Success);

			//List<BlogViewModel> blogViewModels = new List<BlogViewModel>();
			//foreach (var item in lst)
			//{
			//    BlogViewModel viewModel = new BlogViewModel
			//    {
			//        Id = item.Blog_Id,
			//        Author = item.Blog_Author,
			//        Content = item.Blog_Content,
			//        Title = item.Blog_Title
			//    };
			//    blogViewModels.Add(viewModel);
			//}
			//model.BlogList = blogViewModels;
			return model;
		}

		public async Task<BlogResponseModel> CreateBlog(BlogRequestModel requestModel)
		{
			BlogResponseModel model = new BlogResponseModel();
			BlogDataModel blog = new BlogDataModel
			{
				Blog_Title = requestModel.Title!,
				Blog_Author = requestModel.Author!,
				Blog_Content = requestModel.Content!,
			};

			await _appDbContext.Blogs.AddAsync(blog);
			var result = await _appDbContext.SaveChangesAsync();

			string respCode = result > 0 ? "000" : "999";
			string respDesp = result > 0 ? "Saving Successful." : "Saving Failed.";
			EnumRespType respType = result > 0 ? EnumRespType.Success : EnumRespType.Error;
			model.Response = new ResponseModel(respCode, respDesp, respType);
			return model;
		}

		public async Task<BlogResponseModel> UpdateBlog(int blogId, BlogRequestModel requestModel)
		{
			BlogResponseModel model = new BlogResponseModel();

			var existingBlog = await _appDbContext.Blogs.FindAsync(blogId);

			if (existingBlog == null)
			{
				model.Response = new ResponseModel("999", "Blog not found for update.", EnumRespType.Error);

			}
			else
			{
				existingBlog.Blog_Title = requestModel.Title!;
				existingBlog.Blog_Author = requestModel.Author!;
				existingBlog.Blog_Content = requestModel.Content!;

				var result = await _appDbContext.SaveChangesAsync();

				string respCode = result > 0 ? "000" : "999";
				string respDesp = result > 0 ? "Updating Successful." : "Updating Failed.";
				EnumRespType respType = result > 0 ? EnumRespType.Success : EnumRespType.Error;
				model.Blog = new BlogViewModel
				{
					Id = existingBlog.Blog_Id,
					Author = existingBlog.Blog_Author,
					Content = existingBlog.Blog_Content,
					Title = existingBlog.Blog_Title
				};

				model.Response = new ResponseModel(respCode, respDesp, respType);
			}

			return model;
		}

	}
}
