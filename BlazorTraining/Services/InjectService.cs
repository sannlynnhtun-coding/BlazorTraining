using MudBlazor;

namespace BlazorTraining.Services
{
    public class InjectService
    {
        private readonly IDialogService _dialogService;

        public InjectService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void ShowMessage(ResponseModel reqModel)
        {
            var parameters = new DialogParameters<MessageBox>();
            parameters.Add(x => x.Response, reqModel);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            _dialogService.Show<MessageBox>(reqModel.RespType.ToString(), parameters, options);
        }

        public async Task<bool> ConfirmMessage(ResponseModel reqModel)
        {
            var parameters = new DialogParameters<ConfirmMessageBox>();
            parameters.Add(x => x.Response, reqModel);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await _dialogService.ShowAsync<ConfirmMessageBox>(reqModel.RespType.ToString(), parameters, options);
            var result = await dialog.Result;
            return !result.Canceled;
        }
    }
}
