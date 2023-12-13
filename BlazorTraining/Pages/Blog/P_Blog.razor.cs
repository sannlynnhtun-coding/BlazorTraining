using BlazorTraining.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace BlazorTraining.Pages.Blog
{
    public partial class P_Blog
    {
        //[Inject]
        //public AppDbContext _dbContext2 { get; set; }

        private BlogDataModel reqModel { get; set; } = new BlogDataModel();
        RadzenDataGrid<BlogDataModel> dataGrid;
        private List<BlogDataModel> dataList;
        private int rowCount = 0;
        private PageSettingModel pageSetting;
        private EnumFormType _formType = EnumFormType.List;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //await List();
                pageSetting = new PageSettingModel(1, 10);
            }
        }

        private async Task List(int pageNo = 1, int pageSize = 10)
        {
            dataList = await _dbContext.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            rowCount = await _dbContext.Blogs.CountAsync();
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            pageSetting.PageNo = pageNo;
            pageSetting.PageSize = pageSize;
            pageSetting.PageCount = pageCount;
            dataGrid.CurrentPage = pageNo - 1;
            _formType = EnumFormType.List;
            StateHasChanged();
        }

        private void Cancel()
        {
            //reqModel.Blog_Title = string.Empty;
            reqModel = new BlogDataModel();
        }

        private async Task Save()
        {
            Console.WriteLine(reqModel.Blog_Title);
            Console.WriteLine(reqModel.Blog_Author);
            Console.WriteLine(reqModel.Blog_Content);

            await _dbContext.Blogs.AddAsync(reqModel);
            await _dbContext.SaveChangesAsync();

            //_InjectService.ShowMessage(new ResponseModel
            //{
            //    RespCode = "000",
            //    RespDesp = "Success",
            //    RespType = EnumRespType.Success
            //});

            _InjectService.ShowMessage(new ResponseModel("000", "Saving Successful.", EnumRespType.Success));
            reqModel = new BlogDataModel();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }

        //async void OnPage(PagerEventArgs args)
        //{
        //    await List(args.PageIndex + 1);
        //}

        private async Task LoadData(LoadDataArgs args)
        {
            pageSetting.PageNo = ((args.Skip ?? 0) / 10) + 1;
            await List(pageSetting.PageNo);
        }

        private void Create()
        {
            _formType = EnumFormType.Create;
        }

        private async Task Edit(int blogId)
        {
            var item = await _dbContext.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == blogId);
            if(item is null)
            {
                _InjectService.ShowMessage(new ResponseModel("999", "No data found.", EnumRespType.Warning));
                return;
            }

            reqModel = item;
            _formType = EnumFormType.Edit;
        }

        private async Task Update()
        {
            var item = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == reqModel.Blog_Id);
            if (item is null)
            {
                _InjectService.ShowMessage(new ResponseModel("999", "No data found.", EnumRespType.Warning));
                return;
            }

            item.Blog_Title = reqModel.Blog_Title;
            item.Blog_Author = reqModel.Blog_Author;
            item.Blog_Content = reqModel.Blog_Content;
            await _dbContext.SaveChangesAsync();

            _InjectService.ShowMessage(new ResponseModel("000", "Updating Successful.", EnumRespType.Success));
            reqModel = new BlogDataModel();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }

        private async Task Delete(int blogId)
        {
            if (!await _InjectService.ConfirmMessage(new ResponseModel("999", "Are you sure want to delete?", EnumRespType.Confirm)))
            {
                return;
            }

            var item = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == blogId);
            if (item is null)
            {
                _InjectService.ShowMessage(new ResponseModel("999", "No data found.", EnumRespType.Warning));
                return;
            }

            _dbContext.Blogs.Remove(item);
            await _dbContext.SaveChangesAsync();
            _InjectService.ShowMessage(new ResponseModel("000", "Deleting Successful.", EnumRespType.Success));
            reqModel = new BlogDataModel();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }
    }
}
