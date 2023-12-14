using BlazorTraining.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;

namespace BlazorTraining.Pages.BlogWithApi
{
    public partial class P_BlogWithApi
    {
        private BlogViewModel reqModel { get; set; } = new BlogViewModel();
        RadzenDataGrid<BlogViewModel> dataGrid;
        private List<BlogViewModel> dataList;
        private int rowCount = 0;
        private PageSettingModel pageSetting;
        private EnumFormType _formType = EnumFormType.List;
        [Inject]
        public ApiService _apiService { get; set; }

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
            var model = await _apiService.GetBlogs(new BlogRequestModel
            {
                PageSettng = new PageSettingModel(pageNo, pageSize)
            });
            dataList = model.BlogList;
            rowCount = model.PageSetting.RowCount;
            int pageCount = model.PageSetting.PageCount;
            pageSetting.PageNo = pageNo;
            pageSetting.PageSize = pageSize;
            pageSetting.PageCount = pageCount;
            dataGrid.CurrentPage = pageNo - 1;
            _formType = EnumFormType.List;
            StateHasChanged();
        }

        private void Cancel()
        {
            reqModel = new BlogViewModel();
        }

        private async Task Save()
        {
            var response = await _apiService.CreateBlog(new BlogRequestModel
            {
                Title = reqModel.Title,
                Author = reqModel.Author,
                Content = reqModel.Content,
            });

            _InjectService.ShowMessage(response.Response);
            reqModel = new();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }

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
            reqModel = new();
            _formType = EnumFormType.Edit;
        }

        private async Task Update()
        {
            reqModel = new();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }

        private async Task Delete(int blogId)
        {
            reqModel = new();

            pageSetting = new PageSettingModel(1, 10);
            await List();
        }
    }
}
